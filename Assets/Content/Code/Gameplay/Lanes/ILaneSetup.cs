using UnityEngine;

namespace Content.Code.Gameplay.Level
{
    public interface ILaneSetup
    {
        Lane[] Lanes { get; }
        int StartLane { get; }
        float PlayerSpawnOffset { get; }
        float EnemySpawnOffset { get; }
    }
}