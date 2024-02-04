using UnityEngine;

namespace Content.Code.Gameplay.Enemies.Mono
{
    public interface IEnemyDefinition
    {
        ParticleSystem ProjectileParticleSystem { get; }
    }
}