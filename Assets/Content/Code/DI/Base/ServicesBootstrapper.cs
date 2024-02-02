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
        
        private void Awake()
        {
            Setup();
        }

        protected virtual void Setup()
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
            RegisterEngineFeatures();
        }

        protected virtual void InitializeServices()
        {
            CreateAndInitializeMonoHook();
        }

        private void RegisterEngineFeatures()
        {
            ServiceCollection.AddSingleton<IMonoHookManager, MonoHookManager>();
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