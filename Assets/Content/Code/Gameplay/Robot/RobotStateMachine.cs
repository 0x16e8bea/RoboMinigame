using System;
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
    }

    enum Triggers
    {
        Jump,
        Left,
        Right,
        LaneChangeEnded
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
        _fsm.AddState(RobotState.MovingLeft, state => OnEnterChangeLane(state, true));
        _fsm.AddState(RobotState.MovingRight, state => OnEnterChangeLane(state, false));
        
        _fsm.AddTriggerTransition(Triggers.Left, RobotState.Idle, RobotState.MovingLeft);
        _fsm.AddTriggerTransition(Triggers.Right, RobotState.Idle, RobotState.MovingRight);
        _fsm.AddTriggerTransition(Triggers.LaneChangeEnded, RobotState.MovingLeft, RobotState.Idle);
        _fsm.AddTriggerTransition(Triggers.LaneChangeEnded, RobotState.MovingRight, RobotState.Idle);
    }
    
    public void Start()
    {
        _fsm.Init();
    }

    private void OnEnterChangeLane(State<RobotState, Triggers> state, bool isMovingLeft)
    {
        if (isMovingLeft)
        {
            _robotController.Move(IRobotController.MovementDirection.Left);
        }
        else
        {
            _robotController.Move(IRobotController.MovementDirection.Right);
        }

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
    }

    public void OnJump(InputAction.CallbackContext context)
    {
    }

    #endregion
}