using UnityEngine;

namespace Content.Code.Gameplay.Robot
{
    public interface IRobotFactory
    {
        (IRobotController, IRobotStateMachine) InstantiateRobot(Vector3 position = default, Quaternion rotation = default);
    }
}