using System.Collections.Generic;
using Content.Code.Gameplay.Enemies.Instance;

namespace Content.Code.Gameplay.Enemies
{
    public interface IEnemyRepository
    {
        void AddEnemy(IEnemyInstance enemyInstance);
        IEnumerable<IEnemyInstance> GetAllEnemies();
        void RemoveEnemy(IEnemyInstance enemyInstance);
        bool TryGetEnemy(int getInstanceID, out IEnemyInstance enemyInstance);
    }
}