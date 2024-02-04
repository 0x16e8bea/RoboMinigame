using UnityEngine;

namespace Content.Code.Gameplay.Robot.Controller.Monobehaviour
{
    public class RobotDefinition : MonoBehaviour, IRobotDefinition
    {
        [SerializeField] private Animator _blastAnimator;
        [SerializeField] private ParticleSystem _blastParticleSystem;

        public Animator BlastAnimator => _blastAnimator;
        public ParticleSystem BlastParticleSystem => _blastParticleSystem;
    }
}