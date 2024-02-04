using Content.Code.Gameplay.Robot.Projectiles;

public interface ICollisionReceiver
{
    void RegisterCollisions(IParticleCollisionNotifier robotDefinition);
    void UnregisterCollisions(IParticleCollisionNotifier robotDefinition);
}