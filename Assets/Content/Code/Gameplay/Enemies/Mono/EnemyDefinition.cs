using UnityEngine;

namespace Content.Code.Gameplay.Enemies.Mono
{
    public class EnemyDefinition : MonoBehaviour, IEnemyDefinition
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private ParticleSystem projectileParticleSystem;
        [SerializeField] private ParticleSystem destroyedParticleSystem;

        public MeshRenderer MeshRenderer => meshRenderer;
        public ParticleSystem ProjectileParticleSystem => projectileParticleSystem;
        public ParticleSystem DestroyedParticleSystem => destroyedParticleSystem;
        

    }
}
