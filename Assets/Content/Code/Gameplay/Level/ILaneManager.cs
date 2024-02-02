using UnityEngine;

public interface ILaneManager
{
    Transform GetLane(int laneIndex);
    Transform GetNextLane(int laneIndex);
    Transform GetPreviousLane(int laneIndex);
    ILaneSetup LaneSetup { get; }
    int StartLaneIndex { get; }
    int LaneCount { get; }
}