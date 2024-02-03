using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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
        private readonly RobotSettings _robotSettings;

        public RobotController(
            GameObject robotInstance,
            IMonoHookManager monoHookManager,
            ILaneManager laneManager,
            RobotSettings robotSettings)
        {
            _robotInstance = robotInstance;
            _laneManager = laneManager;
            _robotSettings = robotSettings;
            monoHookManager.AddUpdateListener(this);
        }
        

        public void MoveToLaneInstantly(int laneIndex)
        {
            _currentLane = laneIndex;
            _robotInstance.transform.position = _laneManager.GetLanePos(_currentLane);
        }

        private async UniTask<bool> MoveToLaneParabola(int laneIndex)
        {
            Vector3 currentPos = _laneManager.GetLanePos(_currentLane);
            Vector3 targetPos = _laneManager.GetLanePos(laneIndex);
            Vector3 direction = targetPos - currentPos;
            float jumpHeight = Mathf.Max(currentPos.y, targetPos.y) + _robotSettings.JumpHeight;
            Vector3 peakPoint = (direction * 0.5f) + currentPos + Vector3.up * jumpHeight;

            SimpleQuadraticSolver.Coefficients coefficients = SimpleQuadraticSolver.Calc3PointIntersectionCoefficients(currentPos, peakPoint, targetPos);

            float totalDistance = direction.magnitude;
            float speed = totalDistance / _robotSettings.JumpDuration;
            float accumulatedDistance = 0f;

            Vector3 startPos = currentPos;

            while (accumulatedDistance < totalDistance)
            {
                float distanceThisFrame = Mathf.Min(speed * Time.deltaTime, totalDistance - accumulatedDistance);
                accumulatedDistance += distanceThisFrame;

                // Calculate the current position based on the accumulated distance
                float xPos = Mathf.Lerp(startPos.x, targetPos.x, _robotSettings.JumpCurve.Evaluate(accumulatedDistance / totalDistance));
                float yPos = SimpleQuadraticSolver.CalculateY(xPos, coefficients);
                
                // Update the robot's position
                _robotInstance.transform.position = new Vector3(xPos, yPos, currentPos.z);
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            

            return true;
        }


        public void Shoot()
        {
            
        }

        public void Jump()
        {
            
        }

        public async UniTask<MovementResult> Move(IRobotController.MovementDirection direction)
        {
            var nextLane = _currentLane + (direction == IRobotController.MovementDirection.Left ? -1 : 1);
            
            if (nextLane < 0 || nextLane >= _laneManager.LaneCount)
            {
                return new MovementResult(false);
            }
            
            var isLaneChangeSuccessful = await MoveToLaneParabola(nextLane);
            _currentLane = nextLane;
            
            return new MovementResult(isLaneChangeSuccessful);
        }


        public void Update()
        {
        
        }

        public void FixedUpdate()
        {
        
        }
    }
}