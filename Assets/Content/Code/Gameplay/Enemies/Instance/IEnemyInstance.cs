﻿using System;
using Content.Code.Gameplay.Enemies.Mono;
using UnityEngine;

namespace Content.Code.Gameplay.Enemies.Instance
{
    public interface IEnemyInstance
    {
        int InstanceId { get; }
        GameObject GameObject { get; }
        IEnemyController Controller { get; }
        IEnemyStateMachine StateMachine { get; }
        IEnemyDefinition EnemyDefinition { get; }
        int LaneIndex { get; set; }

        // TODO: Decide to keep this or not. Right now it is just used for the repository to remove the enemy from the list.
        event Action<IEnemyInstance> OnDestroyed;
        
        public void Destroy();
    }
}