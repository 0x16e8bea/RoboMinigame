using Content.Code.Gameplay.Level;
using Content.Code.Gameplay.Robot.Controller;
using Content.Code.Gameplay.Robot.StateMachine;
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

        public RobotFactory(
            RobotFactorySettings robotFactorySettings, 
            IMonoHookManager monoHookManager,
            ILaneManager laneManager,
            PlayerInputActions playerInputActions,
            RobotSettings robotSettings)
        {
            _robotFactorySettings = robotFactorySettings;
            _monoHookManager = monoHookManager;
            _laneManager = laneManager;
            _playerInputActions = playerInputActions;
            _robotSettings = robotSettings;
        }

        public (IRobotController, IRobotStateMachine) InstantiateRobot(Vector3 position = default, Quaternion rotation = default)
        {
            var instance = Object.Instantiate(_robotFactorySettings.RobotPrefab, position, rotation);
            IRobotController? robotController = new RobotController(instance, _monoHookManager, _laneManager, _robotSettings);
            IRobotStateMachine? robotStateMachine = new RobotStateMachine(_playerInputActions, robotController);
        
            if (robotController == null)
            {
                throw new System.Exception("Robot prefab does not have a component implementing IRobotController");
            }
        
            return (robotController, robotStateMachine);
        }
    }
}