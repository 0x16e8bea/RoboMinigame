using Content.Code.Gameplay.Robot;
using Cysharp.Threading.Tasks;

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