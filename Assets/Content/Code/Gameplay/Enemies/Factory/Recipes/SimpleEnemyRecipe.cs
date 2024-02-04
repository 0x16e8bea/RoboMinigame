using Content.Code.Gameplay.Enemies.Controller;
using Content.Code.Gameplay.Enemies.Instance;
using Content.Code.Gameplay.Enemies.StateMachine;
using UnityEngine;

namespace Content.Code.Gameplay.Enemies.Factory.Recipes
{
    public record SimpleEnemyRecipe(GameObject Prefab) : IEnemyRecipe
    {
        public IEnemyInstance Create()
        {
            var enemyGameObject = Object.Instantiate(Prefab);
            var controller = new SimpleEnemyController(enemyGameObject);
            var stateMachine = new SimpleEnemyStateMachine(controller);

            var enemyInstance = new EnemyInstance(enemyGameObject, controller, stateMachine);
            
            return enemyInstance;
        }
    }
}