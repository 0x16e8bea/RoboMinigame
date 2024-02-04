
using Content.Code.Gameplay.Enemies.Instance;
using Content.Code.Gameplay.Level;
using UnityEngine;

namespace Content.Code.Gameplay.Enemies
{
    public class EnemySpawner : IEnemySpawner
    {
        private readonly ILaneManager _laneManager;
        private readonly IEnemyFactory _enemyFactory;
        private readonly IEnemyRepository _enemyRepository;
        private bool[] _laneOccupied;

        public EnemySpawner(
            ILaneManager laneManager,
            IEnemyFactory enemyFactory,
            IEnemyRepository enemyRepository)
        {
            _laneManager = laneManager;
            _enemyFactory = enemyFactory;
            _enemyRepository = enemyRepository;
            _laneOccupied = new bool[_laneManager.LaneCount];
        }
    
        public void SpawnEnemy<T>(int lane) where T : IEnemyRecipe
        {
            // Check if lane is valid
            if (lane < 0 || lane >= _laneManager.LaneCount)
            {
                throw new System.ArgumentOutOfRangeException("lane", "Lane is out of range");
            }
        
            // Check if lane is already occupied
            if (_laneOccupied[lane])
            {
                Debug.LogError("Lane is already occupied");
                return;
            }

            // Spawn the enemy
            IEnemyInstance controller = _enemyFactory.CreateEnemy<T>();
            _enemyRepository.AddEnemy(controller);
        
            // Set the enemy's position
            Vector3 position = _laneManager.GetLanePos(lane);
            controller.GameObject.transform.position = position + _laneManager.LaneSetup.EnemySpawnOffset * Vector3.forward;
            controller.GameObject.transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
        
        
        }
    }
}