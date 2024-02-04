using UnityEngine;

namespace Content.Code.Gameplay.Lanes
{
    public interface ILaneManager
    {
        Vector3 GetLanePos(int laneIndex);
        Vector3 GetNextLanePos(int laneIndex);
        Vector3 GetPreviousLanePos(int laneIndex);
        ILaneSetup LaneSetup { get; }
        int StartLaneIndex { get; }
        int LaneCount { get; }
        ILane GetLane(int laneIndex);
    }
}