using Content.Code.Gameplay.Enemies.Instance;
using UnityEngine;

namespace Content.Code.Gameplay.Enemies
{
    public interface IEnemyFactory
    {
        IEnemyInstance CreateEnemy<T>() where T : IEnemyRecipe;
    }
}