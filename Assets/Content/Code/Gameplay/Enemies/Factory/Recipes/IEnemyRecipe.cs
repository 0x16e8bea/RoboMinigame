using Content.Code.Gameplay.Enemies.Instance;
using UnityEngine;

namespace Content.Code.Gameplay.Enemies
{
    public interface IEnemyRecipe
    {
        public IEnemyInstance Create();
    }
}