using System.Threading;
using Content.Code.Common.Collision;
using Content.Code.Gameplay.Enemies;
using Content.Code.Gameplay.Enemies.Factory.Recipes;
using Content.Code.Gameplay.Enemies.FX;
using Content.Code.Gameplay.Enemies.Instance;
using Content.Code.Gameplay.Enemies.Repository;
using Content.Code.Gameplay.Enemies.Spawner;
using Content.Code.Gameplay.Gamepad;
using Content.Code.Gameplay.Gamepad.Controller;
using Content.Code.Gameplay.Gamepad.StateMachine;
using Content.Code.Gameplay.Lanes;
using Content.Code.Gameplay.Robot.Factory;
using Content.Code.Gameplay.Robot.Instance;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Content.Code.Gameplay.Level
{
    public class LevelLifeCycle : ILevelLifeCycle
    {
        public ICollisionReceiver CollisionReceiver { get; }
        private readonly IRobotFactory _robotFactory;
        private readonly ILaneManager _laneManager;
        private readonly IEnemySpawner _enemySpawner;
        private ICollisionReceiver _collisionReceiver;
        private IRobotInstance _robotInstance;

        public LevelLifeCycle(
            IRobotFactory robotFactory,
            ILaneManager laneManager,
            IGamepadController gamepadController,
            IGamepadStateMachine gamepadStateMachine,
            IEnemySpawner enemySpawner,
            IEnemyRepository enemyRepository,
            IEnemyDeathFXController enemyDeathFXController,
            ICollisionReceiver collisionReceiver)
        {
            CollisionReceiver = collisionReceiver;
            _robotFactory = robotFactory;
            _laneManager = laneManager;
            _enemySpawner = enemySpawner;
            _collisionReceiver = collisionReceiver;
        }

        public void InitializeLevel()
        {
            _robotInstance = _robotFactory.InstantiateRobot();
            _robotInstance.Controller.MoveToLaneInstantly(_laneManager.StartLaneIndex);
            _robotInstance.StateMachine.Start();
            
            _collisionReceiver.RegisterCollisions(_robotInstance.RobotDefinition.ParticleCollisionNotifier);
            
            SpawnEnemies(new CancellationToken()).Forget();
        }
        
        // TODO: This is just a placeholder, it should be replaced with a proper implementation.
        async UniTask SpawnEnemies(CancellationToken token)
        {
            await UniTask.Delay(500, cancellationToken: token);

            while (!token.IsCancellationRequested)
            {
                var randomInterval = Random.Range(0, _laneManager.LaneCount);
                IEnemyInstance? enemy = _enemySpawner.SpawnEnemy<SimpleEnemyRecipe>(randomInterval);
                
                if (enemy == null)
                {
                    await UniTask.Delay(1000, cancellationToken: token);
                    continue;
                }
                
                _robotInstance.CollisionReceiver.RegisterCollisions(enemy.EnemyDefinition.ParticleCollisionNotifier);
                await UniTask.Delay(1000, cancellationToken: token);
            }
            
        }
        
        
    }

}