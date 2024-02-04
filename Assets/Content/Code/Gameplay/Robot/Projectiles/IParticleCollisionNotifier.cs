using System;
using UnityEngine;

namespace Content.Code.Gameplay.Robot.Projectiles
{
    public interface IParticleCollisionNotifier
    {
        void AddListener(Action<GameObject> action);
        void RemoveListener(Action<GameObject> action);
    }
}