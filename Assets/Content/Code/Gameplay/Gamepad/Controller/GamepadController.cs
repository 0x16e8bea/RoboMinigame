using UnityEngine;

namespace Content.Code.Gameplay.Gamepad
{
    public class GamepadController : IGamepadController
    {
        private readonly IGamepadDefinition _gamepadDefinition;
        private readonly IMonoHookManager _monoHookManager;
        private readonly Animator _animator;
        private readonly Rigidbody _hinge1Rigidbody;
        private readonly Rigidbody _hinge2Rigidbody;

        public GamepadController(
            GamepadControllerSettings gamepadControllerSettings)
        {
            _gamepadDefinition = gamepadControllerSettings.GamepadPrefab.GetComponent<IGamepadDefinition>();
            _hinge1Rigidbody = _gamepadDefinition.Hinge1RigidBody;
            _hinge2Rigidbody = _gamepadDefinition.Hinge2RigidBody;
            _animator = _gamepadDefinition.Animator;
        }

        public void ApplyForceToDPad()
        {
            _hinge1Rigidbody.AddForceAtPosition(Vector3.down * 25, _gamepadDefinition.DPad.position);
            _hinge2Rigidbody.AddForceAtPosition(Vector3.down * 25, _gamepadDefinition.DPad.position);
        }

        public void PressLeftButton()
        {
            _animator.SetTrigger("Left");
        }

        public void PressRightButton()
        {
            _animator.SetTrigger("Right");
        }

        public void PressAButton()
        {
            _animator.SetTrigger("A");
        }

        public void PressBButton()
        {
            _animator.SetTrigger("B");
        }

        public void ApplyForceToAButton()
        {
            _hinge1Rigidbody.AddForceAtPosition(Vector3.down * 25, _gamepadDefinition.JumpButton.position);
            _hinge2Rigidbody.AddForceAtPosition(Vector3.down * 25, _gamepadDefinition.JumpButton.position);
        }

        public void ApplyForceToBButton()
        {
            _hinge1Rigidbody.AddForceAtPosition(Vector3.down * 25, _gamepadDefinition.ShootButton.position);
            _hinge2Rigidbody.AddForceAtPosition(Vector3.down * 25, _gamepadDefinition.ShootButton.position);
        }
    }
}