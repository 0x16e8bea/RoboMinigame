using Content.Code.Gameplay.Robot.Projectiles;
using UnityEngine;

namespace Content.Code.Gameplay.Robot.Controller.Monobehaviour
{
    public interface IRobotDefinition
    {
        Animator BlastAnimator { get; }
        ParticleSystem BlastParticleSystem { get; }
        IParticleCollisionNotifier ParticleCollisionNotifier { get; }
    }
}