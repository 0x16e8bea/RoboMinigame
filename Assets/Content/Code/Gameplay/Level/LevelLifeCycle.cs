using Content.Code.Gameplay.Robot;
using UnityEngine;

namespace Content.Code.Gameplay.Level
{
    public class LevelLifeCycle : ILevelLifeCycle
    {
        private readonly IRobotFactory _robotFactory;
        private readonly ILaneManager _laneManager;

        public LevelLifeCycle(
            IRobotFactory robotFactory,
            ILaneManager laneManager)
        {
            _robotFactory = robotFactory;
            _laneManager = laneManager;

            InitializeLevel();
        }

        public void InitializeLevel()
        {
            (IRobotController controller, IRobotStateMachine stateMachine) = _robotFactory.InstantiateRobot();
            controller.MoveToLaneInstantly(_laneManager.StartLaneIndex);
            stateMachine.Start();
        }
    }

}