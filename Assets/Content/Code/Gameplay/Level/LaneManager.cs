using UnityEngine;

namespace Content.Code.Gameplay.Level
{
    public class LaneManager : ILaneManager
    {
        private readonly ILaneSetup _laneSetup;
        private Transform[] lanes;

        public ILaneSetup LaneSetup => _laneSetup;

        public LaneManager(ILaneSetup laneSetup)
        {
            _laneSetup = laneSetup;
            lanes = laneSetup.Lanes;
        }
    
        public int StartLaneIndex => _laneSetup.StartLane;
        public int LaneCount => _laneSetup.Lanes.Length;

        public Vector3 GetLanePos(int laneIndex)
        {
            return lanes[laneIndex].position + Vector3.forward * _laneSetup.PlayerSpawnOffset;
        }
    
        public Vector3 GetNextLanePos(int laneIndex)
        {
            return lanes[laneIndex + 1].position + Vector3.forward * _laneSetup.PlayerSpawnOffset;
        }
    
        public Vector3 GetPreviousLanePos(int laneIndex)
        {
            return lanes[laneIndex - 1].position + Vector3.forward * _laneSetup.PlayerSpawnOffset;
        }
    
    }
}