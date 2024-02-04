using System;
using System.Collections.Generic;
using Content.Code.DI;
using Content.Code.Gameplay.Enemies;
using Content.Code.Gameplay.Gamepad;
using Content.Code.Gameplay.Level;
using Content.Code.Gameplay.Robot.Controller;
using Content.Code.Gameplay.Robot.Factory;
using Content.Code.Gameplay.Robot.Projectiles;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;

public class PrototypeSceneEntryPoint : ServicesBootstrapper
{
    ServicesBootstrapper _servicesBootstrapper;
    
    #region Serialized fields

    [SerializeField] private GameObject robotPrefab;
    [SerializeField] private GameObject gamepadPrefab;
    [SerializeField] private LaneSetup laneSetup;
    [SerializeField] private RobotSettings robotSettings;
    
    // TODO: Perhaps add a scriptable object containing all the enemy prefabs
    [SerializeField] private GameObject enemy1Prefab;


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
        RegisterGamepad();
        RegisterEnemy();
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
        ServiceProvider.GetRequiredService<ILevelLifeCycle>().InitializeLevel();
    }

    private void RegisterCharacter()
    {
        ServiceCollection.AddSingleton<IRobotFactory, RobotFactory>();
        ServiceCollection.AddSingleton(new RobotFactorySettings(robotPrefab));
        ServiceCollection.AddSingleton(_playerInputActions);
        ServiceCollection.AddSingleton(robotSettings);
    }
    
    private void RegisterGamepad()
    {
        ServiceCollection.AddSingleton<IGamepadController, GamepadController>();
        ServiceCollection.AddSingleton(new GamepadControllerSettings(gamepadPrefab)); 
        ServiceCollection.AddSingleton<IGamepadStateMachine, GamepadStateMachine>();
    }
    
    private void RegisterEnemy()
    {
        ServiceCollection.AddSingleton<IEnemyFactory, EnemyFactory>();
        ServiceCollection.AddSingleton<IEnemySpawner, EnemySpawner>();
        ServiceCollection.AddSingleton<IDictionary<Type, IEnemyRecipe>>(new Dictionary<Type, IEnemyRecipe>
        {
            {typeof(SimpleEnemyRecipe), new SimpleEnemyRecipe(enemy1Prefab)}
        });
        ServiceCollection.AddSingleton<IEnemyRepository, EnemyRepository>();
    }
    
    

}
