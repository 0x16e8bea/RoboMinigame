using System;
using System.Collections.Generic;
using Content.Code.Gameplay.Enemies.Factory.Recipes;
using Content.Code.Gameplay.Enemies.Instance;
using UnityEngine;

namespace Content.Code.Gameplay.Enemies.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IDictionary<Type, List<IEnemyRecipe>> _recipes;

        public EnemyFactory(IDictionary<Type, List<IEnemyRecipe>> recipes)
        {
            _recipes = recipes;
        }

        public IEnemyInstance CreateEnemy<T>() where T : IEnemyRecipe
        {
            if (!_recipes.TryGetValue(typeof(T), out var creators) || creators.Count == 0)
            {
                throw new ArgumentOutOfRangeException("T", "Invalid enemy type or no recipes available");
            }

            var randomIndex = UnityEngine.Random.Range(0, creators.Count);
            return creators[randomIndex].Create();
        }
    }
}