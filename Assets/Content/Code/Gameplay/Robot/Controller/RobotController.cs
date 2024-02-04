using Content.Code.Gameplay.Gamepad;
using Content.Code.Gameplay.Gamepad.Health;
using Content.Code.Gameplay.Lanes;
using Content.Code.Gameplay.Level;
using Content.Code.Gameplay.Robot.Controller.Mono;
using Content.Code.Gameplay.Robot.Factory;
using Content.Code.Gameplay.Robot.State;
using Content.Code.Gameplay.Robot.Utilities;
using Content.Code.UnityFeatures.ScriptLifeCycle;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Content.Code.Gameplay.Robot.Controller
{
    public class RobotController : IRobotController, IUpdate
    {
        private static readonly int ShootTrigger = Animator.StringToHash("Shoot");

        private readonly GameObject _robotInstance;
        private readonly ILaneManager _laneManager;
        private readonly IRobotFactory _robotFactory;
        private readonly RobotSettings _robotSettings;
        private readonly IHealthIndicatorController _healthIndicatorController;
        private readonly IRobotDefinition _robotDefinition;

        private int _currentLane;
        private IRobotData _robotData;

        public bool CanUpdate => true;
        public IRobotData Data => _robotData;
        public RobotSettings Settings => _robotSettings;

        public RobotController(
            GameObject robotInstance,
            IMonoHookManager monoHookManager,
            ILaneManager laneManager,
            RobotSettings robotSettings,
            IHealthIndicatorController healthIndicatorController)
        {
            _robotInstance = robotInstance;
            _laneManager = laneManager;
            _robotSettings = robotSettings;
            _healthIndicatorController = healthIndicatorController;
            _robotDefinition = _robotInstance.GetComponent<IRobotDefinition>();
            monoHookManager.AddUpdateListener(this);
            _robotData = _robotInstance.GetComponent<IRobotData>();
            healthIndicatorController.SetHealthIndicator(_robotData.Health);
        }

        public void Jump()
        {
            _robotData.Velocity = Vector3.up * Mathf.Sqrt(2 * _robotSettings.RegularJumpHeight * Physics.gravity.magnitude);
            _robotData.IsGrounded = false;
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
            float jumpHeight = Mathf.Max(currentPos.y, targetPos.y) + _robotSettings.LaneChangePeakHeight;
            Vector3 peakPoint = (direction * 0.5f) + currentPos + Vector3.up * jumpHeight;

            SimpleQuadraticSolver.Coefficients coefficients = SimpleQuadraticSolver.Calc3PointIntersectionCoefficients(currentPos, peakPoint, targetPos);

            float totalDistance = direction.magnitude;
            float speed = totalDistance / _robotSettings.LaneChangeDuration;
            float accumulatedDistance = 0f;

            Vector3 startPos = currentPos;

            while (accumulatedDistance < totalDistance)
            {
                float distanceThisFrame = Mathf.Min(speed * Time.deltaTime, totalDistance - accumulatedDistance);
                accumulatedDistance += distanceThisFrame;

                // Calculate the current position based on the accumulated distance
                float xPos = Mathf.Lerp(startPos.x, targetPos.x, _robotSettings.LaneChangeAnimCurve.Evaluate(accumulatedDistance / totalDistance));
                float yPos = SimpleQuadraticSolver.CalculateY(xPos, coefficients);
                
                // Update the robot's position
                _robotInstance.transform.position = new Vector3(xPos, yPos, currentPos.z);
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            
            return true;
        }

        public void Shoot()
        {
            _robotDefinition.BlastAnimator.SetTrigger(ShootTrigger);
            _robotDefinition.BlastParticleSystem.Play();
        }

        public void TakeDamage()
        {
            if (_robotData.Health <= 0)
            {
                Debug.Log("Robot is dead");
                return;
            }
            
            _robotData.Health--;
            _healthIndicatorController.SetHealthIndicator(_robotData.Health);
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
            if (_robotData.IsChangingLanes)
            {
                _robotData.Velocity = Vector3.zero;
                return;
            }
            
            _robotData.Velocity += Physics.gravity * Time.deltaTime;
            _robotData.Velocity = Vector3.ClampMagnitude(_robotData.Velocity, _robotSettings.TerminalVelocity);

            // If velocity is indicating that we are falling, do a raycast to check if we are grounded
            if (_robotData.Velocity.y < 0)
            {
                if (Physics.Raycast(
                        _robotInstance.transform.position + Vector3.up * 0.5f,
                        Vector3.down,
                        out var hit, Mathf.Infinity,
                        LayerMask.GetMask("Ground")))
                {
                    var distanceToGround = _robotInstance.transform.position.y - hit.point.y;

                    if (distanceToGround < 0.01f)
                    {
                        _robotData.IsGrounded = true;
                        _robotData.Velocity = Vector3.zero;
                    }
                    else
                    {
                        _robotData.IsGrounded = false;
                    }
                }
            }
            _robotInstance.transform.position += _robotData.Velocity * Time.deltaTime;
        }
    }
}