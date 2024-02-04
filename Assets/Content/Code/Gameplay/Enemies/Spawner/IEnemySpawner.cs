namespace Content.Code.Gameplay.Enemies
{
    public interface IEnemySpawner
    {
        void SpawnEnemy<T>(int laneIndex) where T : IEnemyRecipe;
    }
}