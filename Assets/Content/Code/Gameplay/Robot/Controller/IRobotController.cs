using Content.Code.Gameplay.Robot.State;
using Cysharp.Threading.Tasks;

namespace Content.Code.Gameplay.Robot.Controller
{
    public interface IRobotController
    { 
        IRobotData Data { get; }
        RobotSettings Settings { get; }
        void Jump();
        void MoveToLaneInstantly(int laneIndex);
        UniTask<MovementResult> Move(MovementDirection direction);
        void Shoot();

        enum MovementDirection
        {
            Left,
            Right,
        }
    }
}