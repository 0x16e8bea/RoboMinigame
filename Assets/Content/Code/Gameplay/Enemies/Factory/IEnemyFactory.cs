using Content.Code.Gameplay.Enemies.Factory.Recipes;
using Content.Code.Gameplay.Enemies.Instance;

namespace Content.Code.Gameplay.Enemies.Factory
{
    public interface IEnemyFactory
    {
        IEnemyInstance CreateEnemy<T>() where T : IEnemyRecipe;
    }
}