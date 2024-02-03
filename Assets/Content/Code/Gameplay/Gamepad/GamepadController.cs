using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepadController : IGamepadController
{
    private readonly IMonoHookManager _monoHookManager;
    private readonly IGamepadDefinition _gamepadDefinition;
    private Rigidbody _hinge1Rigidbody;
    private Rigidbody _hinge2Rigidbody;
    private Animator _animator;

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

    public void MoveLeft()
    {
        _animator.SetTrigger("Left");
    }
    
    public void MoveRight()
    {
        _animator.SetTrigger("Right");
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