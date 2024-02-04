using Content.Code.Gameplay.Enemies.Instance;

namespace Content.Code.Gameplay.Enemies
{
    public interface IEnemySpawner
    {
        IEnemyInstance? SpawnEnemy<T>(int laneIndex) where T : IEnemyRecipe;
    }
}