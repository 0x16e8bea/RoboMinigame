using UnityEngine;

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

    public Transform GetLane(int laneIndex)
    {
        return lanes[laneIndex];
    }
    
    public Transform GetNextLane(int laneIndex)
    {
        return lanes[laneIndex + 1];
    }
    
    public Transform GetPreviousLane(int laneIndex)
    {
        return lanes[laneIndex - 1];
    }
    
}