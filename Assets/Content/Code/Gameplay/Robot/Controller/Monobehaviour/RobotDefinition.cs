using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotDefinition : MonoBehaviour, IRobotDefinition
{
    [SerializeField] private Animator _blastAnimator;
    [SerializeField] private ParticleSystem _blastParticleSystem;

    public Animator BlastAnimator => _blastAnimator;
    public ParticleSystem BlastParticleSystem => _blastParticleSystem;
}