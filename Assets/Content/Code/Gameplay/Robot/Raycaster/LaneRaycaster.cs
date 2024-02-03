using UnityEngine;

public class LaneRaycaster : ILaneRaycaster
{
    const string GroundLayerName = "Ground";
    
    public bool RaycastGround(Vector3 position, out RaycastHit hit)
    {
        Ray ray = new Ray(position, Vector3.down);
        
        return Physics.Raycast(ray, out hit, default, LayerMask.GetMask(GroundLayerName));
    }

    public bool RaycastGround(Vector3 position, out RaycastHit hit, float maxDistance)
    {
        Ray ray = new Ray(position, Vector3.down);
        
        return Physics.Raycast(ray, out hit, maxDistance, LayerMask.GetMask(GroundLayerName));
    }
}