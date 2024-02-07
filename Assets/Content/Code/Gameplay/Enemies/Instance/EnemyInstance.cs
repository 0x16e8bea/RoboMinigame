using System;
using Content.Code.Gameplay.Enemies.Controller;
using Content.Code.Gameplay.Enemies.Mono;
using Content.Code.Gameplay.Enemies.StateMachine;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Content.Code.Gameplay.Enemies.Instance
{
    public class EnemyInstance : IEnemyInstance
    {
        public int InstanceId { get; } = -1;
        
        public IEnemyDefinition EnemyDefinition { get; }
        public int LaneIndex { get; set; } = -1;
        public GameObject GameObject { get; }
        public IEnemyController Controller { get; }
        public IEnemyStateMachine StateMachine { get; }
        public event Action<IEnemyInstance> OnDestroyed;
        
        public void DestroyImmediate()
        {
            Object.Destroy(GameObject);
            OnDestroyed?.Invoke(this);
        }
        
        public async UniTaskVoid DestroyAfterParticles()
        {
            StateMachine.OnDestroy();
            
            // Wait one frame
            await UniTask.Yield();

            var meshRenderer = EnemyDefinition.MeshRenderer;
            meshRenderer.enabled = false;

            var projectileParticleSystem = EnemyDefinition.ProjectileParticleSystem;
            var destroyedParticleSystem = EnemyDefinition.DestroyedParticleSystem;

            if (projectileParticleSystem.isPlaying)
            {
                projectileParticleSystem.Stop();
            }
            

            // Create tasks that wait for each particle system to finish playing
            var projectileParticlesTask = UniTask.WaitWhile(() => projectileParticleSystem.particleCount > 0);
            var destroyedParticlesTask = UniTask.WaitWhile(() => destroyedParticleSystem.particleCount > 0);

            // Wait for both tasks to complete
            await UniTask.WhenAll(projectileParticlesTask, destroyedParticlesTask);

            DestroyImmediate();
        }

        public EnemyInstance(GameObject gameObject, IEnemyController controller, IEnemyStateMachine stateMachine)
        {
            GameObject = gameObject;
            InstanceId = gameObject.GetInstanceID();
            Controller = controller;
            StateMachine = stateMachine;
            EnemyDefinition = gameObject.GetComponent<EnemyDefinition>();
        }
    }
}