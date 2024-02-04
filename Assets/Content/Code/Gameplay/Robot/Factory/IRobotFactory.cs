using Content.Code.Gameplay.Robot.Instance;
using UnityEngine;

namespace Content.Code.Gameplay.Robot.Factory
{
    public interface IRobotFactory
    {
        IRobotInstance InstantiateRobot(Vector3 position = default, Quaternion rotation = default);
    }
}