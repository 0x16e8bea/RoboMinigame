using UnityEngine;

namespace Content.Code.Gameplay.Enemies.FX
{
    public class EnemyDeathFXController : MonoBehaviour, IEnemyDeathFXController
    {
        [SerializeField] private ParticleSystem _deathParticleSystem;
    
        public void PlayDeathParticlesAtLocation(Vector3 location)
        {
            _deathParticleSystem.transform.position = location;
            _deathParticleSystem.Play();
        }
    }
}