using System.Threading;
using Content.Code.Gameplay.Enemies;
using Content.Code.Gameplay.Enemies.FX;
using Content.Code.Gameplay.Enemies.Instance;
using Content.Code.Gameplay.Gamepad;
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
        
        async UniTask SpawnEnemies(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                Debug.Log("Spawning enemy");
                var randomInterval = UnityEngine.Random.Range(0, _laneManager.LaneCount);
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