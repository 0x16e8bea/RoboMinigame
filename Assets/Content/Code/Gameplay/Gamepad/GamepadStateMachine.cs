using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;
using UnityHFSM;

public class GamepadStateMachine : IGamepadStateMachine, PlayerInputActions.IDefaultActions
{
    private readonly IGamepadController _gamepadController;
    StateMachine<State, Trigger> _fsm;

    enum State
    {
        Idle,
        PressingRight,
        PressingLeft,
        PressingAButton,
        PressingBButton,
    }
    
    enum Trigger
    {
        LeftButton,
        RightButton,
        AButton,
        BButton
    }
    
    public GamepadStateMachine(
        IGamepadController gamepadController,
        PlayerInputActions playerInputActions)
    {
        _gamepadController = gamepadController;
        playerInputActions.Default.AddCallbacks(this);
        
        _fsm = new StateMachine<State, Trigger>();
        
        _fsm.SetStartState(State.Idle);
        
        _fsm.AddState(State.Idle);
        _fsm.AddState(State.PressingLeft, OnEnterPressingLeft);
        _fsm.AddState(State.PressingRight, OnEnterPressingRight);
        
        _fsm.AddTriggerTransition(Trigger.LeftButton, State.Idle, State.PressingLeft);
        _fsm.AddTriggerTransition(Trigger.RightButton, State.Idle, State.PressingRight);
        _fsm.AddTriggerTransition(Trigger.LeftButton, State.PressingRight, State.PressingLeft);
        _fsm.AddTriggerTransition(Trigger.RightButton, State.PressingLeft, State.PressingRight);
        
        _fsm.Init();
        
    }

    private void OnEnterPressingLeft(State<State,Trigger> state)
    {
        _gamepadController.MoveLeft();
        _gamepadController.ApplyForceToDPad();
        _fsm.RequestStateChange(State.Idle);
    }
    
    private void OnEnterPressingRight(State<State,Trigger> state)
    {
        _gamepadController.MoveRight();
        _gamepadController.ApplyForceToDPad();
        _fsm.RequestStateChange(State.Idle);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        
        switch (context.ReadValue<float>())
        {
            case 1:
                _fsm.Trigger(Trigger.RightButton);
                break;
            case -1:
                _fsm.Trigger(Trigger.LeftButton);
                break;
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        
    }
}