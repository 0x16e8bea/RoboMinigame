using UnityEngine;

public interface ILane
{
    Transform Transform { get; }
    bool IsEnemySpotOccupied { get; set; }
}