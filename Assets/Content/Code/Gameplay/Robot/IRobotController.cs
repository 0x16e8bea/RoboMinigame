public interface IRobotController
{ 
    enum MovementDirection
    {
        Left,
        Right,
    }
    void Shoot();
    void Jump();
    void Move(MovementDirection direction);
    void MoveToLaneInstantly(int laneIndex);
}