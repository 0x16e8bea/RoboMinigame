using Content.Code.Gameplay.Robot.Controller.Monobehaviour;
using Content.Code.Gameplay.Robot.Projectiles;
using UnityEngine;

namespace Content.Code.Gameplay.Robot.Controller.Mono
{
    public class RobotDefinition : MonoBehaviour, IRobotDefinition
    {
        [SerializeField] private Animator _blastAnimator;
        [SerializeField] private ParticleSystem _blastParticleSystem;
        [SerializeField] private ParticleCollisionNotifier _particleCollisionNotifier;

        public Animator BlastAnimator => _blastAnimator;
        public ParticleSystem BlastParticleSystem => _blastParticleSystem;
        public IParticleCollisionNotifier ParticleCollisionNotifier => _particleCollisionNotifier;
    }
}