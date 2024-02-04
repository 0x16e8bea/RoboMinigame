using System;
using Content.Code.Gameplay.Enemies.Mono;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Content.Code.Gameplay.Enemies
{
    /// <summary>
    /// Enemy that shoots and does not move
    /// </summary>
    public class SimpleEnemyController : IEnemyController
    {
        private readonly IEnemyDefinition _enemyDefinition;
        public Action<IEnemyController> OnDeath { get; set; }
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

        public void Move()
        {
        }

        public void Kill()
        {
            Object.Destroy(GameObject);
            OnDeath?.Invoke(this);
        }
    }
}