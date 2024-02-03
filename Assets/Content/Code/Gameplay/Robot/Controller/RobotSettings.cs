using UnityEngine;

namespace Content.Code.Gameplay.Robot
{
    [CreateAssetMenu(menuName = "Gameplay/Create RobotSettings", fileName = "RobotSettings", order = 0)]
    public class RobotSettings : ScriptableObject
    {
        [SerializeField] private float _jumpHeight = 1.0f;
        [SerializeField] private float _jumpDuration = 0.5f;
        [SerializeField] private AnimationCurve _jumpCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));

        public float JumpHeight => _jumpHeight;
        public float JumpDuration => _jumpDuration;

        public AnimationCurve JumpCurve => _jumpCurve;
    }
}