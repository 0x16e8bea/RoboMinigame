using UnityEngine;

namespace Content.Code.Gameplay.Robot
{
    public class RobotController : IRobotController, IUpdate, IFixedUpdate
    {
        public bool CanUpdate => true;
        public bool CanFixedUpdate => true;
    
        private int _currentLane;

        private readonly GameObject _robotInstance;
        private readonly ILaneManager _laneManager;
        private readonly IRobotFactory _robotFactory;

        public RobotController(
            GameObject robotInstance,
            IMonoHookManager monoHookManager,
            ILaneManager laneManager)
        {
            _robotInstance = robotInstance;
            _laneManager = laneManager;
            monoHookManager.AddUpdateListener(this);
        }
        

        public void MoveToLaneInstantly(int laneIndex)
        {
            _currentLane = laneIndex;
            _robotInstance.transform.position = _laneManager.GetLane(_currentLane).position;
        }

        public void Shoot()
        {
            
        }

        public void Jump()
        {
            
        }

        public void Move(IRobotController.MovementDirection direction)
        {
            var nextLane = _currentLane + (direction == IRobotController.MovementDirection.Left ? -1 : 1);
            
            if (nextLane < 0 || nextLane >= _laneManager.LaneCount)
            {
                return;
            }
        }


        public void Update()
        {
        
        }

        public void FixedUpdate()
        {
        
        }
    }
}