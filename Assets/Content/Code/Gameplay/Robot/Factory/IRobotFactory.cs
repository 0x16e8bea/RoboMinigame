using Content.Code.Gameplay.Robot.Controller;
using Content.Code.Gameplay.Robot.Controller.Monobehaviour;
using Content.Code.Gameplay.Robot.StateMachine;
using UnityEngine;

namespace Content.Code.Gameplay.Robot.Factory
{
    public interface IRobotFactory
    {
        (IRobotController, IRobotStateMachine, IRobotDefinition) InstantiateRobot(Vector3 position = default, Quaternion rotation = default);
    }
}