using Content.Code.Gameplay.Robot.Controller;
using Content.Code.Gameplay.Robot.Controller.Monobehaviour;
using Content.Code.Gameplay.Robot.StateMachine;

namespace Content.Code.Gameplay.Robot.Instance
{
    public interface IRobotInstance
    {
        IRobotController Controller { get; }
        IRobotStateMachine StateMachine { get; }
        IRobotDefinition RobotDefinition { get; }
        ICollisionReceiver CollisionReceiver { get; }
    }
}