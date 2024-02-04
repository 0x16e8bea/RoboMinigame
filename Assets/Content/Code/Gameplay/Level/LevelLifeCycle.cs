using Content.Code.Gameplay.Enemies;
using Content.Code.Gameplay.Gamepad;
using Content.Code.Gameplay.Robot.Controller;
using Content.Code.Gameplay.Robot.Controller.Monobehaviour;
using Content.Code.Gameplay.Robot.Factory;
using Content.Code.Gameplay.Robot.Projectiles;
using Content.Code.Gameplay.Robot.StateMachine;

namespace Content.Code.Gameplay.Level
{
    public class LevelLifeCycle : ILevelLifeCycle
    {
        private readonly IRobotFactory _robotFactory;
        private readonly ILaneManager _laneManager;
        private readonly IEnemySpawner _enemySpawner;
        private readonly IEnemyRepository _enemyRepository;
        
        private EnemyCollisionNotifier _enemyCollisionNotifier;

        public LevelLifeCycle(
            IRobotFactory robotFactory,
            ILaneManager laneManager,
            IGamepadController gamepadController,
            IGamepadStateMachine gamepadStateMachine,
            IEnemySpawner enemySpawner,
            IEnemyRepository enemyRepository)
        {
            _robotFactory = robotFactory;
            _laneManager = laneManager;
            _enemySpawner = enemySpawner;
            _enemyRepository = enemyRepository;
        }

        public void InitializeLevel()
        {
            (IRobotController controller, IRobotStateMachine stateMachine, IRobotDefinition _robotDefinition) = _robotFactory.InstantiateRobot();
            controller.MoveToLaneInstantly(_laneManager.StartLaneIndex);
            stateMachine.Start();
            
            _enemyCollisionNotifier = new EnemyCollisionNotifier(_robotDefinition.ParticleCollisionNotifier, _enemyRepository);

            // TODO: Delete this as it is only for testing purposes
            _enemySpawner.SpawnEnemy<SimpleEnemyRecipe>(0);
        }
    }

}