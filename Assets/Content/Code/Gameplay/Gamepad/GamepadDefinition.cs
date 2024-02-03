using UnityEngine;

public class GamepadDefinition : MonoBehaviour, IGamepadDefinition
{
    [SerializeField] private Transform _leftButton;
    [SerializeField] private Transform _rightButton;
    [SerializeField] private Transform _jumpButton;
    [SerializeField] private Transform _shootButton;
    [SerializeField] private Transform _dPad;
    [SerializeField] private Rigidbody _hinge1Rigidbody;
    [SerializeField] private Rigidbody _hinge2Rigidbody;
    [SerializeField] private Animator _animator;
    
    
    public Transform LeftButton => _leftButton;
    public Transform RightButton => _rightButton;
    public Transform JumpButton => _jumpButton;
    public Transform ShootButton => _shootButton;
    public Transform DPad => _dPad;
    public Rigidbody Hinge1RigidBody => _hinge1Rigidbody;
    public Rigidbody Hinge2RigidBody => _hinge2Rigidbody;
    
    public Animator Animator => _animator;
    
}