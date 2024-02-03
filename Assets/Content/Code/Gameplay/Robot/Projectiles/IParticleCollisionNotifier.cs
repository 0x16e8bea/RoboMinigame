using System;
using UnityEngine;

public interface IParticleCollisionNotifier
{
    void AddListener(Action<GameObject> action);
    void RemoveListener(Action<GameObject> action);
}