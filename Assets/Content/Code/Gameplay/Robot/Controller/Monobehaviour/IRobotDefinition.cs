using UnityEngine;

namespace Content.Code.Gameplay.Robot.Controller.Monobehaviour
{
    public interface IRobotDefinition
    {
        Animator BlastAnimator { get; }
        ParticleSystem BlastParticleSystem { get; }
    }
}