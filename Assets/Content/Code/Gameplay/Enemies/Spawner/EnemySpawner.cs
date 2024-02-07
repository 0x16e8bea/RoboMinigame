using Content.Code.Gameplay.Enemies.Factory;
using Content.Code.Gameplay.Enemies.Factory.Recipes;
using Content.Code.Gameplay.Enemies.Instance;
using Content.Code.Gameplay.Enemies.Repository;
using Content.Code.Gameplay.Lanes;
using UnityEngine;

namespace Content.Code.Gameplay.Enemies.Spawner
{
    public class EnemySpawner : IEnemySpawner
    {
        private readonly ILaneManager _laneManager;
        private readonly IEnemyFactory _enemyFactory;
        private readonly IEnemyRepository _enemyRepository;

        public EnemySpawner(
            ILaneManager laneManager,
            IEnemyFactory enemyFactory,
            IEnemyRepository enemyRepository)
        {
            _laneManager = laneManager;
            _enemyFactory = enemyFactory;
            _enemyRepository = enemyRepository;
        }
    
        public IEnemyInstance? SpawnEnemy<T>(int laneIndex) where T : IEnemyRecipe
        {
            var requestedLane = _laneManager.GetLane(laneIndex);
            
            if (requestedLane.IsEnemySpotOccupied)
            {
                Debug.LogWarning("Lane is occupied");
                return null;
            }
            
            requestedLane.IsEnemySpotOccupied = true;

            // Check if lane is valid
            if (laneIndex < 0 || laneIndex >= _laneManager.LaneCount)
            {
                throw new System.ArgumentOutOfRangeException("laneIndex", "Lane is out of range");
            }

            // Spawn the enemy
            IEnemyInstance instance = _enemyFactory.CreateEnemy<T>();
            instance.LaneIndex = laneIndex;
            
            _laneManager.GetLane(laneIndex).IsEnemySpotOccupied = true;
            instance.OnDestroyed += enemyInstance =>
            {
                _laneManager.GetLane(enemyInstance.LaneIndex).IsEnemySpotOccupied = false;
            };
            
            _enemyRepository.AddEnemy(instance);
        
            // Set the enemy's position
            Vector3 position = _laneManager.GetLanePos(laneIndex);
            instance.GameObject.transform.position = position + _laneManager.LaneSetup.EnemySpawnOffset * Vector3.forward;
            instance.GameObject.transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
            
            return instance;
        }
    }
}