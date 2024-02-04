using UnityEngine;

namespace Content.Code.Gameplay.Lanes
{
    public interface ILane
    {
        Transform Transform { get; }
        bool IsEnemySpotOccupied { get; set; }
    }
}