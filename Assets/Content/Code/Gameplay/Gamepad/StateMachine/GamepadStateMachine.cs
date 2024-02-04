using UnityEngine.InputSystem;
using UnityHFSM;

namespace Content.Code.Gameplay.Gamepad
{
    public class GamepadStateMachine : IGamepadStateMachine, PlayerInputActions.IDefaultActions
    {
        private readonly IGamepadController _gamepadController;
        private readonly StateMachine<State, Trigger> _fsm;

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
            _fsm.AddState(State.PressingAButton, OnEnterPressA);
            _fsm.AddState(State.PressingBButton, OnEnterPressB);

            _fsm.AddTriggerTransitionFromAny(Trigger.LeftButton, State.PressingLeft);
            _fsm.AddTriggerTransitionFromAny(Trigger.RightButton, State.PressingRight);
            _fsm.AddTriggerTransitionFromAny(Trigger.AButton, State.PressingAButton);
            _fsm.AddTriggerTransitionFromAny(Trigger.BButton, State.PressingBButton);

            _fsm.Init();
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
            if (!context.started) return;

            _fsm.Trigger(Trigger.BButton);
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (!context.started) return;

            _fsm.Trigger(Trigger.AButton);
        }

        private void OnEnterPressA(State<State, Trigger> obj)
        {
            _gamepadController.PressAButton();
            _gamepadController.ApplyForceToAButton();
            _fsm.RequestStateChange(State.Idle);
        }

        private void OnEnterPressB(State<State, Trigger> obj)
        {
            _gamepadController.PressBButton();
            _gamepadController.ApplyForceToBButton();
            _fsm.RequestStateChange(State.Idle);
        }

        private void OnEnterPressingLeft(State<State, Trigger> state)
        {
            _gamepadController.PressLeftButton();
            _gamepadController.ApplyForceToDPad();
            _fsm.RequestStateChange(State.Idle);
        }

        private void OnEnterPressingRight(State<State, Trigger> state)
        {
            _gamepadController.PressRightButton();
            _gamepadController.ApplyForceToDPad();
            _fsm.RequestStateChange(State.Idle);
        }

        private enum State
        {
            Idle,
            PressingRight,
            PressingLeft,
            PressingAButton,
            PressingBButton
        }

        private enum Trigger
        {
            LeftButton,
            RightButton,
            AButton,
            BButton
        }
    }
}