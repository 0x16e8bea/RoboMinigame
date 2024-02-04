using Content.Code.Gameplay.Gamepad.Health;
using UnityEngine;

namespace Content.Code.Gameplay.Gamepad.Mono
{
    public interface IGamepadDefinition
    {
        Transform LeftButton { get; }
        Transform RightButton { get; }
        Transform JumpButton { get; }
        Transform ShootButton { get; }
        Transform DPad { get; }
        Rigidbody Hinge1RigidBody { get; }
        Rigidbody Hinge2RigidBody { get; }
        Animator Animator { get; }
        IHealthIndicatorController HealthIndicatorController { get; }
    }
}