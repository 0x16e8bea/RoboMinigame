using Content.Code.Gameplay.Robot.Projectiles;

namespace Content.Code.Common.Collision
{
    public interface ICollisionReceiver
    {
        void RegisterCollisions(IParticleCollisionNotifier robotDefinition);
        void UnregisterCollisions(IParticleCollisionNotifier robotDefinition);
    }
}