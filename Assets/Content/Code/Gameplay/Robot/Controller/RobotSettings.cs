using UnityEngine;
using UnityEngine.Serialization;

namespace Content.Code.Gameplay.Robot
{
    [CreateAssetMenu(menuName = "Gameplay/Create RobotSettings", fileName = "RobotSettings", order = 0)]
    public class RobotSettings : ScriptableObject
    {
        [SerializeField] private float laneChangePeakHeight = 1.0f;
        [SerializeField] private float regularJumpHeight = 1.0f;
        [SerializeField] private float laneChangeDuration = 0.5f;
        [SerializeField] private AnimationCurve laneChangeAnimCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
        [SerializeField] private float shootCooldown = 0.8f;
        [SerializeField] private float terminalVelocity = 10.0f;

        public float LaneChangePeakHeight => laneChangePeakHeight;
        public float LaneChangeDuration => laneChangeDuration;
        public AnimationCurve LaneChangeAnimCurve => laneChangeAnimCurve;
        public float ShootCooldown => shootCooldown;
        public float TerminalVelocity => terminalVelocity;
        public float RegularJumpHeight => regularJumpHeight;
        
    }
}