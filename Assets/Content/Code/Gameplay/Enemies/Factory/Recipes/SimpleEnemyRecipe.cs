using Content.Code.Gameplay.Enemies.Controller;
using Content.Code.Gameplay.Enemies.Instance;
using Content.Code.Gameplay.Enemies.Mono;
using Content.Code.Gameplay.Enemies.StateMachine;
using UnityEngine;

namespace Content.Code.Gameplay.Enemies.Factory.Recipes
{
    public record SimpleEnemyRecipe(GameObject Prefab, float ProjectileSpeed = 30) : IEnemyRecipe
    {
        public IEnemyInstance Create()
        {
            var enemyGameObject = Object.Instantiate(Prefab);
            var controller = new SimpleEnemyController(enemyGameObject);
            var stateMachine = new SimpleEnemyStateMachine(controller);
            
            // Get the particle system and set the projectile speed
            var enemyDefinition = enemyGameObject.GetComponent<EnemyDefinition>();
            var mainModule = enemyDefinition.ProjectileParticleSystem.main;
            mainModule.startSpeed = ProjectileSpeed;

            var enemyInstance = new EnemyInstance(enemyGameObject, controller, stateMachine);
            
            return enemyInstance;
        }
    }
}