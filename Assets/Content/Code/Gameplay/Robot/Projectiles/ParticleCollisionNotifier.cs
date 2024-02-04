using System;
using UnityEngine;

namespace Content.Code.Gameplay.Robot.Projectiles
{
    public class ParticleCollisionNotifier : MonoBehaviour, IParticleCollisionNotifier
    {
        delegate void OnParticleCollisionDelegate(GameObject other);

        private OnParticleCollisionDelegate? _onParticleCollisionDelegate;

        public void AddListener(Action<GameObject> action)
        {
            _onParticleCollisionDelegate += action.Invoke;
        }

        public void RemoveListener(Action<GameObject> action)
        {
            _onParticleCollisionDelegate -= action.Invoke;
        }

        void OnParticleCollision(GameObject other)
        {
            Debug.Log("Collision with " + other.name);

            if (_onParticleCollisionDelegate != null)
            {
                _onParticleCollisionDelegate.Invoke(other);
            }
        }
    }
}