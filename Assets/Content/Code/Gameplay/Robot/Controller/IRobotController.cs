using Content.Code.Gameplay.Robot;
using Cysharp.Threading.Tasks;

public interface IRobotController
{ 
    enum MovementDirection
    {
        Left,
        Right,
    }
    void Shoot();
    void Jump();
    UniTask<MovementResult> Move(MovementDirection direction);
    void MoveToLaneInstantly(int laneIndex);
}