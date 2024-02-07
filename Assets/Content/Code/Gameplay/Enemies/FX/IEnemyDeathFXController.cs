using Content.Code.Gameplay.Enemies.Factory.Recipes;
using UnityEngine;

namespace Content.Code.Gameplay.Enemies.FX
{
    public interface IEnemyDeathFXController
    {
        void PlayDeathParticlesAtLocation(
            Vector3 location);
    }
}