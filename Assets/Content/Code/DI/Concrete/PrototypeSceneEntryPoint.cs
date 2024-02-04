using System;
using System.Collections.Generic;
using Content.Code.Common.Collision;
using Content.Code.DI.Base;
using Content.Code.Gameplay.Enemies.Collision;
using Content.Code.Gameplay.Enemies.Factory;
using Content.Code.Gameplay.Enemies.Factory.Recipes;
using Content.Code.Gameplay.Enemies.FX;
using Content.Code.Gameplay.Enemies.Repository;
using Content.Code.Gameplay.Enemies.Spawner;
using Content.Code.Gameplay.Gamepad.Controller;
using Content.Code.Gameplay.Gamepad.Health;
using Content.Code.Gameplay.Gamepad.StateMachine;
using Content.Code.Gameplay.Lanes;
using Content.Code.Gameplay.Level;
using Content.Code.Gameplay.Robot.Controller;
using Content.Code.Gameplay.Robot.Factory;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;

namespace Content.Code.DI.Concrete
{
    public class PrototypeSceneEntryPoint : ServicesBootstrapper
    {
        ServicesBootstrapper _servicesBootstrapper;
    
        #region Serialized fields

        [SerializeField] private GameObject robotPrefab;
        [SerializeField] private GameObject gamepadPrefab;
        [SerializeField] private LaneSetup laneSetup;
        [SerializeField] private RobotSettings robotSettings;
        [SerializeField] private EnemyDeathFXController enemyDeathFXController;
        [SerializeField] private HealthIndicatorController healthIndicatorController;
    

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
            ServiceCollection.AddSingleton<IHealthIndicatorController>(healthIndicatorController);
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
            ServiceCollection.AddSingleton<IEnemyDeathFXController>(enemyDeathFXController);
            ServiceCollection.AddSingleton<ICollisionReceiver, EnemyCollisionReceiver>();
        }
    
    

    }
}