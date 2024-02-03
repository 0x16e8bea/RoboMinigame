using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityHFSM;

public class RobotStateMachine : IRobotStateMachine, PlayerInputActions.IDefaultActions
{
    enum RobotState
    {
        Idle,
        MovingLeft,
        MovingRight,
        Shooting,
        Jumping,
    }

    enum Triggers
    {
        Jump,
        Left,
        Right,
        LaneChangeEnded,
        Shoot,
    }

    private readonly IRobotController _robotController;
    StateMachine<RobotState, Triggers> _fsm;

    public RobotStateMachine(
        PlayerInputActions playerInputActions,
        IRobotController robotController)
    {
        _robotController = robotController;
        playerInputActions.Default.AddCallbacks(this);

        _fsm = new StateMachine<RobotState, Triggers>();

        _fsm.AddState(RobotState.Idle);
        _fsm.AddState(RobotState.MovingLeft, state => OnEnterChangeLane(state, true).Forget());
        _fsm.AddState(RobotState.MovingRight, state => OnEnterChangeLane(state, false).Forget());
        _fsm.AddState(RobotState.Shooting, OnEnterShoot);
        _fsm.AddState(RobotState.Jumping, OnEnterJumpState);
        
        
        _fsm.AddTriggerTransition(Triggers.Left, RobotState.Idle, RobotState.MovingLeft, _ => _robotController.Data.IsGrounded);
        _fsm.AddTriggerTransition(Triggers.Right, RobotState.Idle, RobotState.MovingRight, _ => _robotController.Data.IsGrounded);
        _fsm.AddTriggerTransition(Triggers.LaneChangeEnded, RobotState.MovingLeft, RobotState.Idle);
        _fsm.AddTriggerTransition(Triggers.LaneChangeEnded, RobotState.MovingRight, RobotState.Idle);
        _fsm.AddTriggerTransition(Triggers.Shoot, RobotState.Idle, RobotState.Shooting);
        _fsm.AddTriggerTransition(Triggers.Jump, RobotState.Idle, RobotState.Jumping, _ => _robotController.Data.IsGrounded);

    }

    private void OnEnterJumpState(State<RobotState, Triggers> obj)
    {
        _robotController.Jump();
        _fsm.RequestStateChange(RobotState.Idle);
    }

    private void OnEnterShoot(State<RobotState,Triggers> state)
    {
        if (_robotController.Data.CanShoot)
        {
            EnterShootCooldown().Forget();
            _robotController.Shoot();
        }
        else
        {
            Debug.Log("Can't shoot yet");
        }
        
        _fsm.RequestStateChange(RobotState.Idle);
    }
    
    private async UniTask EnterShootCooldown()
    {
        _robotController.Data.CanShoot = false;
        await UniTask.Delay(TimeSpan.FromSeconds(_robotController.Settings.ShootCooldown));
        _robotController.Data.CanShoot = true;
    }

    public void Start()
    {
        _fsm.Init();
    }

    private async UniTaskVoid OnEnterChangeLane(State<RobotState, Triggers> state, bool isMovingLeft)
    {
        _robotController.Data.IsChangingLanes = true;
        
        if (isMovingLeft)
        {
            await _robotController.Move(IRobotController.MovementDirection.Left);
        }
        else
        {
            await _robotController.Move(IRobotController.MovementDirection.Right);
        }
        
        _robotController.Data.IsChangingLanes = false;
        _fsm.Trigger(Triggers.LaneChangeEnded);
    }
    

    #region Default Actions

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        
        switch (context.ReadValue<float>())
        {
            case 1:
                _fsm.Trigger(Triggers.Right);
                break;
            case -1:
                _fsm.Trigger(Triggers.Left);
                break;
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        
        _fsm.Trigger(Triggers.Shoot);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        
        _fsm.Trigger(Triggers.Jump);
    }

    #endregion
}