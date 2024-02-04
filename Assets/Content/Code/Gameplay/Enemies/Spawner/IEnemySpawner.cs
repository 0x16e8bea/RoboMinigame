using Content.Code.Gameplay.Enemies.Factory.Recipes;
using Content.Code.Gameplay.Enemies.Instance;

namespace Content.Code.Gameplay.Enemies.Spawner
{
    public interface IEnemySpawner
    {
        IEnemyInstance? SpawnEnemy<T>(int laneIndex) where T : IEnemyRecipe;
    }
}