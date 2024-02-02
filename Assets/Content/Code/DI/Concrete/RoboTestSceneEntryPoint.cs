using Content.Code.DI;
using Content.Code.Gameplay.Level;
using Content.Code.Gameplay.Robot;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;

public class RoboTestSceneEntryPoint : ServicesBootstrapper
{
    ServicesBootstrapper _servicesBootstrapper;
    
    #region Serialized fields

    [SerializeField] private GameObject robotPrefab;
    [SerializeField] private LaneSetup laneSetup;

    #endregion
    
    private PlayerInputActions _playerInputActions;

    protected override void Setup()
    {
        InitializeAndEnablePlayerInput();
        base.Setup();
    }

    private void InitializeAndEnablePlayerInput()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
    }

    protected override void RegisterServices()
    {
        base.RegisterServices();
        RegisterCharacter();
        RegisterLevel();
    }

    private void RegisterLevel()
    {
        ServiceCollection.AddSingleton<ILevelLifeCycle, LevelLifeCycle>();
        ServiceCollection.AddSingleton<ILaneManager, LaneManager>();
        ServiceCollection.AddSingleton<ILaneSetup>(laneSetup);
    }

    protected override void InitializeServices()
    {
        base.InitializeServices();
        ServiceProvider.GetRequiredService<ILevelLifeCycle>();
    }

    private void RegisterCharacter()
    {
        ServiceCollection.AddSingleton<IRobotFactory, RobotFactory>();
        ServiceCollection.AddSingleton(new RobotFactorySettings(robotPrefab));
        ServiceCollection.AddSingleton(_playerInputActions);
    }
    
    

}
