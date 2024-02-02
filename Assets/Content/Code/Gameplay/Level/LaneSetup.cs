using UnityEngine;

public class LaneSetup : MonoBehaviour, ILaneSetup
{
    [SerializeField] private Transform[] lanes;
    [SerializeField] private int startLane;
    [SerializeField] private float characterLaneOffset;

    #region Getters

    public Transform[] Lanes => lanes;
    public int StartLane => startLane;
    public float CharacterLaneOffset => characterLaneOffset;

    #endregion

    private void OnDrawGizmos()
    {
        // Draw a line for each of the lanes
        for (int i = 0; i < lanes.Length; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(lanes[i].position, lanes[i].position + Vector3.forward * 10);
            // Write the lane number
            DrawString(i.ToString(), lanes[i].position + Vector3.up * 0.5f);
        }
        
        if (startLane >= lanes.Length || startLane < 0)
        {
            DrawString("Start lane out of range", lanes[0].position + Vector3.up * 1.5f, Color.red);
            return;
        }
        
        // Draw the offset which runs perpendicular to the lanes
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(lanes[0].position + Vector3.forward * characterLaneOffset, lanes[lanes.Length - 1].position + Vector3.forward * characterLaneOffset);

        if (startLane < lanes.Length)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(lanes[startLane].position + Vector3.forward * characterLaneOffset, 0.5f);
            
        }
    }
    
    /// <summary>
    /// https://gist.github.com/Arakade/9dd844c2f9c10e97e3d0
    /// </summary>
    /// <param name="text"></param>
    /// <param name="worldPos"></param>
    /// <param name="colour"></param>
    static void DrawString(string text, Vector3 worldPos, Color? colour = null) {
        UnityEditor.Handles.BeginGUI();
        if (colour.HasValue) GUI.color = colour.Value;
        var view = UnityEditor.SceneView.currentDrawingSceneView;
        Vector3 screenPos = view.camera.WorldToScreenPoint(worldPos);
        Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
        GUI.Label(new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height + 4, size.x, size.y), text);
        UnityEditor.Handles.EndGUI();
    }
}