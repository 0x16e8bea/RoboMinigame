using Content.Code.Gameplay.Enemies.Instance;
using Content.Code.Gameplay.Enemies.Mono;
using UnityEngine;

namespace Content.Code.Gameplay.Enemies
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