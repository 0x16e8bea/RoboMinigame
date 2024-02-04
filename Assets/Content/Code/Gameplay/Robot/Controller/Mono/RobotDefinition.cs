using Content.Code.Gameplay.Robot.Projectiles;
using UnityEngine;
using UnityEngine.Serialization;

namespace Content.Code.Gameplay.Robot.Controller.Mono
{
    public class RobotDefinition : MonoBehaviour, IRobotDefinition
    {
        [SerializeField] private Animator _blastAnimator;
        [SerializeField] private ParticleSystem _blastParticleSystem;
        [SerializeField] private ParticleCollisionNotifier _particleCollisionNotifier;
        [SerializeField] private AnimationClip _laneChangeAnimationClip;
        [SerializeField] private Animator _robotAnimator;
        
        public Animator BlastAnimator => _blastAnimator;
        public ParticleSystem BlastParticleSystem => _blastParticleSystem;
        public IParticleCollisionNotifier ParticleCollisionNotifier => _particleCollisionNotifier;
        
        public AnimationClip JumpAnimation => _laneChangeAnimationClip;
        public AnimationClip LaneChangeAnimation => _laneChangeAnimationClip;
        public Animator RobotAnimator => _robotAnimator;
        
    }
}