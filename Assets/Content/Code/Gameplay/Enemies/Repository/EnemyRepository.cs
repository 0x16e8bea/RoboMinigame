using System.Collections.Generic;
using Content.Code.Gameplay.Enemies.Instance;
using UnityEngine;

namespace Content.Code.Gameplay.Enemies.Repository
{
    public class EnemyRepository : IEnemyRepository
    {
        private readonly Dictionary<int, IEnemyInstance> _enemies;

        public EnemyRepository()
        {
            _enemies = new Dictionary<int, IEnemyInstance>();
        }

        public void AddEnemy(IEnemyInstance enemyInstance)
        {
            _enemies.Add(enemyInstance.InstanceId, enemyInstance);
            enemyInstance.OnDestroyed += RemoveEnemy;
        }

        public void RemoveEnemy(IEnemyInstance enemyInstance)
        {
            if (!_enemies.ContainsKey(enemyInstance.InstanceId))
            {
                return;
            }
            
            enemyInstance.OnDestroyed -= RemoveEnemy;
            _enemies.Remove(enemyInstance.InstanceId);
        }
        
        public bool TryGetEnemy(int getInstanceID, out IEnemyInstance enemyInstance)
        {
            Debug.Log("Instance ID: " + getInstanceID);
            return _enemies.TryGetValue(getInstanceID, out enemyInstance);
        }

        public IEnumerable<IEnemyInstance> GetAllEnemies()
        {
            return _enemies.Values;
        }
    }
}