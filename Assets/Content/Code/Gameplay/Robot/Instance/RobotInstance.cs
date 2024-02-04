using Content.Code.Common.Collision;
using Content.Code.Gameplay.Robot.Controller;
using Content.Code.Gameplay.Robot.Controller.Mono;
using Content.Code.Gameplay.Robot.StateMachine;

namespace Content.Code.Gameplay.Robot.Instance
{
    public class RobotInstance : IRobotInstance
    {
        public IRobotController Controller { get; }
        public IRobotStateMachine StateMachine { get; }
        public IRobotDefinition RobotDefinition { get; }
        public ICollisionReceiver CollisionReceiver { get; }

        public RobotInstance(IRobotController controller, IRobotStateMachine stateMachine, IRobotDefinition robotDefinition, ICollisionReceiver collisionReceiver)
        {
            Controller = controller;
            StateMachine = stateMachine;
            RobotDefinition = robotDefinition;
            CollisionReceiver = collisionReceiver;
        }
    }
}