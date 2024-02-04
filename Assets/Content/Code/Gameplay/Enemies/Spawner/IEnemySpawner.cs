namespace Content.Code.Gameplay.Enemies
{
    public interface IEnemySpawner
    {
        void SpawnEnemy<T>(int lane) where T : IEnemyRecipe;
    }
}