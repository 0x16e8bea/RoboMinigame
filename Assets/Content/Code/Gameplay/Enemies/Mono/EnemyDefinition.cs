using Content.Code.Gameplay.Robot.Projectiles;
using UnityEngine;

namespace Content.Code.Gameplay.Enemies.Mono
{
    public class EnemyDefinition : MonoBehaviour, IEnemyDefinition
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private ParticleSystem projectileParticleSystem;
        [SerializeField] private ParticleSystem destroyedParticleSystem;
        [SerializeField] private ParticleCollisionNotifier particleCollisionNotifier;
        
        public MeshRenderer MeshRenderer => meshRenderer;
        public ParticleSystem ProjectileParticleSystem => projectileParticleSystem;
        public ParticleSystem DestroyedParticleSystem => destroyedParticleSystem;
        public IParticleCollisionNotifier ParticleCollisionNotifier => particleCollisionNotifier;
        

    }
}
