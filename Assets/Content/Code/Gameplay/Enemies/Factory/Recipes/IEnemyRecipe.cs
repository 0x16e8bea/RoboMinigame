using Content.Code.Gameplay.Enemies.Instance;

namespace Content.Code.Gameplay.Enemies.Factory.Recipes
{
    public interface IEnemyRecipe
    {
        public IEnemyInstance Create();
    }
}