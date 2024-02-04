using UnityEngine;
using UnityEngine.Serialization;

namespace Content.Code.Gameplay.Robot.State
{
    public class RobotData : MonoBehaviour, IRobotData
    {
        [SerializeField] private int health;
        [SerializeField] private bool isGrounded;
        [SerializeField] private bool canShoot;
        [SerializeField] private bool isChangingLanes;
        [SerializeField] private Vector3 velocity;
    
        public int Health
        {
            get => health;
            set => health = value;
        }
    
        public bool IsGrounded
        {
            get => isGrounded;
            set => isGrounded = value;
        }
    
        public bool CanShoot
        {
            get => canShoot;
            set => canShoot = value;
        }
    
        public bool IsChangingLanes
        {
            get => isChangingLanes;
            set => isChangingLanes = value;
        }
    
        public Vector3 Velocity
        {
            get => velocity;
            set => velocity = value;
        }
    }
}