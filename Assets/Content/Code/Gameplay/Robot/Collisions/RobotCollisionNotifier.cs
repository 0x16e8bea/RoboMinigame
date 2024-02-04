using Content.Code.Gameplay.Gamepad;
using Content.Code.Gameplay.Robot.Controller;
using Content.Code.Gameplay.Robot.Projectiles;
using Content.Code.Gameplay.Robot.State;
using UnityEngine;

public class RobotCollisionNotifier : ICollisionReceiver
{
    private readonly IRobotController _robotController;
    private readonly IHealthIndicatorController _healthIndicatorController;

    public RobotCollisionNotifier(
        IRobotController robotController
    )
    {
        _robotController = robotController;
    }

    public void RegisterCollisions(IParticleCollisionNotifier robotDefinition)
    {
        robotDefinition.AddListener(TakeDamage);
    }

    public void UnregisterCollisions(IParticleCollisionNotifier robotDefinition)
    {
        robotDefinition.RemoveListener(TakeDamage);
    }
    
    private void TakeDamage(GameObject other)
    {
        Debug.Log("Collision with " + other.name + " instance ID: " + other.GetInstanceID());
        _robotController.TakeDamage();
    }
}