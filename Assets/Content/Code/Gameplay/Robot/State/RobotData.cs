using UnityEngine;

namespace Content.Code.Gameplay.Robot.State
{
    public class RobotData : MonoBehaviour, IRobotData
    {
        [SerializeField] private int _health;
        [SerializeField] private bool _isGrounded;
        [SerializeField] private bool _canShoot;
        [SerializeField] private bool _isChangingLanes;
        [SerializeField] private Vector3 _velocity;
    
        public int Health
        {
            get => _health;
            set => _health = value;
        }
    
        public bool IsGrounded
        {
            get => _isGrounded;
            set => _isGrounded = value;
        }
    
        public bool CanShoot
        {
            get => _canShoot;
            set => _canShoot = value;
        }
    
        public bool IsChangingLanes
        {
            get => _isChangingLanes;
            set => _isChangingLanes = value;
        }
    
        public Vector3 Velocity
        {
            get => _velocity;
            set => _velocity = value;
        }
    }
}