using Content.Code.Gameplay.Gamepad;
using Content.Code.Gameplay.Gamepad.Health;
using Content.Code.Gameplay.Lanes;
using Content.Code.Gameplay.Level;
using Content.Code.Gameplay.Robot.Collisions;
using Content.Code.Gameplay.Robot.Controller;
using Content.Code.Gameplay.Robot.Controller.Mono;
using Content.Code.Gameplay.Robot.Instance;
using Content.Code.Gameplay.Robot.Projectiles;
using Content.Code.Gameplay.Robot.StateMachine;
using Content.Code.UnityFeatures.ScriptLifeCycle;
using UnityEngine;

namespace Content.Code.Gameplay.Robot.Factory
{
    public class RobotFactory : IRobotFactory
    {
        private readonly RobotFactorySettings _robotFactorySettings;
        private readonly IMonoHookManager _monoHookManager;
        private readonly ILaneManager _laneManager;
        private readonly PlayerInputActions _playerInputActions;
        private readonly RobotSettings _robotSettings;
        private readonly IHealthIndicatorController _healthIndicatorController;

        public RobotFactory(
            RobotFactorySettings robotFactorySettings, 
            IMonoHookManager monoHookManager,
            ILaneManager laneManager,
            PlayerInputActions playerInputActions,
            RobotSettings robotSettings,
            IHealthIndicatorController healthIndicatorController)
        {
            _robotFactorySettings = robotFactorySettings;
            _monoHookManager = monoHookManager;
            _laneManager = laneManager;
            _playerInputActions = playerInputActions;
            _robotSettings = robotSettings;
            _healthIndicatorController = healthIndicatorController;
        }

        public IRobotInstance InstantiateRobot(Vector3 position = default, Quaternion rotation = default)
        {
            GameObject? gameObject = Object.Instantiate(_robotFactorySettings.RobotPrefab, position, rotation);
            IRobotController? robotController = new RobotController(gameObject, _monoHookManager, _laneManager, _robotSettings, _healthIndicatorController);
            IRobotStateMachine? robotStateMachine = new RobotStateMachine(_playerInputActions, robotController);
        
            if (robotController == null)
            {
                throw new System.Exception("Robot prefab does not have a component implementing IRobotController");
            }
        
            return new RobotInstance(robotController, robotStateMachine, gameObject.GetComponent<IRobotDefinition>(), new RobotCollisionNotifier(robotController));
        }
    }
}