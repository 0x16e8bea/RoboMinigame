using System;
using UnityEngine;

namespace Content.Code.Gameplay.Enemies
{
    public interface IEnemyController
    {
        void Attack();
        void Move();
        void Kill();
        GameObject GameObject { get; }
    }
}