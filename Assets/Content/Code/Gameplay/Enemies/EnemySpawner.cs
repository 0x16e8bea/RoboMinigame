
using Content.Code.Gameplay.Level;
using UnityEngine;

namespace Content.Code.Gameplay.Enemies
{
    public class EnemySpawner : IEnemySpawner
    {
        private readonly ILaneManager _laneManager;
        private bool[] _laneOccupied;

        public EnemySpawner(
            ILaneManager laneManager,
            IEnemyFactory enemyFactory)
        {
            _laneManager = laneManager;
            _laneOccupied = new bool[_laneManager.LaneCount];
        }
    
        public void SpawnEnemy(int lane)
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
            // ...
        
            // Set the enemy's position
            // ...
        
        
        
        }
    }
}