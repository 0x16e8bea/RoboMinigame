using Content.Code.Gameplay.Enemies.Mono;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Content.Code.Gameplay.Enemies.Controller
{
    /// <summary>
    /// Enemy that shoots and does not move
    /// </summary>
    public class SimpleEnemyController : IEnemyController
    {
        private readonly IEnemyDefinition _enemyDefinition;
        public GameObject GameObject { get; }

        public SimpleEnemyController(GameObject gameObject)
        {
            _enemyDefinition = gameObject.GetComponent<IEnemyDefinition>();
            GameObject = gameObject;
        }

        public void Attack()
        {
            _enemyDefinition.ProjectileParticleSystem.Play();
        }
        
        public void PlayDeathAnimation()
        {
            _enemyDefinition.DestroyedParticleSystem.Play();
        }

        public void Move()
        {
            
        }
        
    }
}