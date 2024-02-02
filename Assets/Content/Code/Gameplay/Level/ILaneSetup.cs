using UnityEngine;

public interface ILaneSetup
{
    Transform[] Lanes { get; }
    int StartLane { get; }
    float CharacterLaneOffset { get; }
}