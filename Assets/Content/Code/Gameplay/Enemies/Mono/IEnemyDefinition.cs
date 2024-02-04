using Content.Code.Gameplay.Robot.Projectiles;
using UnityEngine;

namespace Content.Code.Gameplay.Enemies.Mono
{
    public interface IEnemyDefinition
    {
        ParticleSystem ProjectileParticleSystem { get; }
        ParticleSystem DestroyedParticleSystem { get; }
        MeshRenderer MeshRenderer { get; }
        IParticleCollisionNotifier ParticleCollisionNotifier { get; }
    }
}