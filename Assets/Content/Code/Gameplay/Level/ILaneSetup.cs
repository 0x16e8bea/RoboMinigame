using UnityEngine;

public interface ILaneSetup
{
    Transform[] Lanes { get; }
    int StartLane { get; }
    float PlayerSpawnOffset { get; }
    float EnemySpawnOffset { get; }
}