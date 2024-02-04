using System;
using System.Collections.Generic;
using Content.Code.Gameplay.Enemies.Factory.Recipes;
using Content.Code.Gameplay.Enemies.Instance;

namespace Content.Code.Gameplay.Enemies.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IDictionary<Type, IEnemyRecipe> _recipes;

        public EnemyFactory(IDictionary<Type, IEnemyRecipe> recipes)
        {
            _recipes = recipes;
        }

        public IEnemyInstance CreateEnemy<T>() where T : IEnemyRecipe
        {
            if (!_recipes.TryGetValue(typeof(T), out var creator))
            {
                throw new ArgumentOutOfRangeException("T", "Invalid enemy type");
            }
            
            return creator.Create();
        }

    }
}