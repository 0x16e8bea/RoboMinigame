using UnityEngine;

namespace Content.Code.Gameplay.Lanes
{
    public class Lane : MonoBehaviour, ILane
    {
        [SerializeField] private bool isEnemySpotOccupied;
        public Transform Transform => transform;
        public bool IsEnemySpotOccupied
        {
            get => isEnemySpotOccupied;
            set
            {
                Debug.Log("Setting isEnemySpotOccupied to " + value);
                isEnemySpotOccupied = value;
            }
        }
    }
}