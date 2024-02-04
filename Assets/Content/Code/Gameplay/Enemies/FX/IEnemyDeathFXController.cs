using UnityEngine;

namespace Content.Code.Gameplay.Enemies.FX
{
    public interface IEnemyDeathFXController
    {
        void PlayDeathParticlesAtLocation(Vector3 location);
    }
}