using UnityEngine;

namespace Content.Code.Gameplay.Level
{
    public interface ILaneSetup
    {
        Transform[] Lanes { get; }
        int StartLane { get; }
        float PlayerSpawnOffset { get; }
        float EnemySpawnOffset { get; }
    }
}