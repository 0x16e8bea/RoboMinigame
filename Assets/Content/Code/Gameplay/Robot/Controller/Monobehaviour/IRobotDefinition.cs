using UnityEngine;

public interface IRobotDefinition
{
    Animator BlastAnimator { get; }
    ParticleSystem BlastParticleSystem { get; }
}