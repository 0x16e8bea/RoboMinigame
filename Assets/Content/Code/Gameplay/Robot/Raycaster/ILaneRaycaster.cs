using UnityEngine;

public interface ILaneRaycaster
{
    /// <summary>
    /// Casts a ray from the given position to the ground and returns true if it hits something.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="hit"></param>
    /// <returns></returns>
    bool RaycastGround(Vector3 position, out RaycastHit hit);
    
    /// <summary>
    /// Casts a ray from the given position to the ground and returns true if it hits something.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="hit"></param>
    /// <param name="maxDistance"></param>
    /// <returns></returns>
    bool RaycastGround(Vector3 position, out RaycastHit hit, float maxDistance);
    
}