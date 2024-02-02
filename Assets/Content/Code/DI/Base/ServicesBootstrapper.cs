using System;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;

namespace Content.Code.DI
{
    public abstract class ServicesBootstrapper : MonoBehaviour
    {
        protected ServiceCollection ServiceCollection;
        protected IServiceProvider ServiceProvider;
        private bool IsBootstrapped { get; set; }

        #region Serialized fields

        [SerializeField] private GameObject robotPrefab;

        #endregion

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (IsBootstrapped) throw new InvalidOperationException("Bootstrapper has already been initialized.");


            ServiceCollection = new ServiceCollection();
            RegisterServices();
            ServiceProvider = ServiceCollection.BuildServiceProvider();
            InitializeServices();
            IsBootstrapped = true;
        }


        protected virtual void RegisterServices()
        {
            RegisterCharacter();
            RegisterEngineFeatures();
        }

        protected virtual void InitializeServices()
        {
            CreateAndInitializeMonoHook();
            ServiceProvider.GetRequiredService<IRobotController>();
        }

        private void RegisterEngineFeatures()
        {
            ServiceCollection.AddSingleton<IMonoHookManager, MonoHookManager>();
        }

        private void RegisterCharacter()
        {
            ServiceCollection.AddSingleton<IRobotController, RobotController>();
            ServiceCollection.AddSingleton(new RobotControllerSettings(robotPrefab));
        }

        private void CreateAndInitializeMonoHook()
        {
            var monoUpdateHook = new GameObject("MonoHook");

            monoUpdateHook
                .AddComponent<MonoHook>()
                .Initialize(ServiceProvider.GetRequiredService<IMonoHookManager>());
        }
    }
}