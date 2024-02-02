using Content.Code.Gameplay.Robot;
using UnityEngine;

public class RobotFactory : IRobotFactory
{
    private readonly RobotFactorySettings _robotFactorySettings;
    private readonly IMonoHookManager _monoHookManager;
    private readonly ILaneManager _laneManager;
    private readonly PlayerInputActions _playerInputActions;

    public RobotFactory(
        RobotFactorySettings robotFactorySettings, 
        IMonoHookManager monoHookManager,
        ILaneManager laneManager,
        PlayerInputActions playerInputActions)
    {
        _robotFactorySettings = robotFactorySettings;
        _monoHookManager = monoHookManager;
        _laneManager = laneManager;
        _playerInputActions = playerInputActions;
    }

    public (IRobotController, IRobotStateMachine) InstantiateRobot(Vector3 position = default, Quaternion rotation = default)
    {
        var instance = Object.Instantiate(_robotFactorySettings.RobotPrefab, position, rotation);
        IRobotController? robotController = new RobotController(instance, _monoHookManager, _laneManager);
        IRobotStateMachine? robotStateMachine = new RobotStateMachine(_playerInputActions, robotController);
        
        if (robotController == null)
        {
            throw new System.Exception("Robot prefab does not have a component implementing IRobotController");
        }
        
        return (robotController, robotStateMachine);
    }
}