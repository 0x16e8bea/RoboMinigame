using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Content.Code.Gameplay.Enemies.Instance
{
    public class EnemyInstance : IEnemyInstance
    {
        public int InstanceId { get; } = -1;
        public int LaneIndex { get; set; } = -1;
        public GameObject GameObject { get; }
        public IEnemyController Controller { get; }
        public IEnemyStateMachine StateMachine { get; }
        public event Action<IEnemyInstance> OnDestroyed;
        
        public void Destroy()
        {
            Object.Destroy(GameObject);
            StateMachine.OnDeath();
            OnDestroyed?.Invoke(this);
        }

        public EnemyInstance(GameObject gameObject, IEnemyController controller, IEnemyStateMachine stateMachine)
        {
            GameObject = gameObject;
            InstanceId = gameObject.GetInstanceID();
            Controller = controller;
            StateMachine = stateMachine;
        }
    }
}