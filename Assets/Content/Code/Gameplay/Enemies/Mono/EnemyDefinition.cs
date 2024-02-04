using UnityEngine;

namespace Content.Code.Gameplay.Enemies.Mono
{
    public class EnemyDefinition : MonoBehaviour, IEnemyDefinition
    {
        [SerializeField] private ParticleSystem projectileParticleSystem;
        public ParticleSystem ProjectileParticleSystem => projectileParticleSystem;
    }
}
