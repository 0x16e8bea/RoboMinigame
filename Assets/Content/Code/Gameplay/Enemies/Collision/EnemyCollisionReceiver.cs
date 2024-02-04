using Content.Code.Common.Collision;
using Content.Code.Gameplay.Enemies.FX;
using Content.Code.Gameplay.Enemies.Instance;
using Content.Code.Gameplay.Enemies.Repository;
using Content.Code.Gameplay.Lanes;
using Content.Code.Gameplay.Robot.Projectiles;
using UnityEngine;

namespace Content.Code.Gameplay.Enemies.Collision
{
    public class EnemyCollisionReceiver : ICollisionReceiver
    {
        private readonly IEnemyRepository _enemyRepository;
        private readonly ILaneManager _laneManager;
        private readonly IEnemyDeathFXController _enemyDeathFXController;

        public EnemyCollisionReceiver(
            IEnemyRepository enemyRepository,
            ILaneManager laneManager,
            IEnemyDeathFXController enemyDeathFXController)
        {
            _enemyRepository = enemyRepository;
            _laneManager = laneManager;
            _enemyDeathFXController = enemyDeathFXController;
        }
    
        public void RegisterCollisions(IParticleCollisionNotifier particleCollisionNotifier)
        {
            particleCollisionNotifier.AddListener(OnParticleCollision);
        }
    
        public void UnregisterCollisions(IParticleCollisionNotifier particleCollisionNotifier)
        {
            particleCollisionNotifier.RemoveListener(OnParticleCollision);
        }

        private void OnParticleCollision(GameObject other)
        {
            Debug.Log("Collision with " + other.name + " instance ID: " + other.GetInstanceID());

            // Try to get the enemy from the repository using the GameObject instance ID
            if (_enemyRepository.TryGetEnemy(other.GetInstanceID(), out IEnemyInstance instance))
            {
                _laneManager.GetLane(instance.LaneIndex).IsEnemySpotOccupied = false;
                instance.Destroy();
                _enemyRepository.RemoveEnemy(instance);
                _enemyDeathFXController.PlayDeathParticlesAtLocation(instance.GameObject.transform.position);
            }
        }
    
    }
}