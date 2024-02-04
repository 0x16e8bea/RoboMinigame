using UnityEngine;

namespace Content.Code.Gameplay.Lanes
{
    public class LaneManager : ILaneManager
    {
        private readonly ILaneSetup _laneSetup;
        private ILane[] lanes;

        public ILaneSetup LaneSetup => _laneSetup;

        public LaneManager(ILaneSetup laneSetup)
        {
            _laneSetup = laneSetup;
            lanes = laneSetup.Lanes;
        }
    
        public int StartLaneIndex => _laneSetup.StartLane;
        public int LaneCount => _laneSetup.Lanes.Length;
        public ILane GetLane(int laneIndex)
        {
            return lanes[laneIndex];
        }

        public Vector3 GetLanePos(int laneIndex)
        {
            return lanes[laneIndex].Transform.position + Vector3.forward * _laneSetup.PlayerSpawnOffset;
        }
    
        public Vector3 GetNextLanePos(int laneIndex)
        {
            return lanes[laneIndex + 1].Transform.position + Vector3.forward * _laneSetup.PlayerSpawnOffset;
        }
    
        public Vector3 GetPreviousLanePos(int laneIndex)
        {
            return lanes[laneIndex - 1].Transform.position + Vector3.forward * _laneSetup.PlayerSpawnOffset;
        }
    
    }
}