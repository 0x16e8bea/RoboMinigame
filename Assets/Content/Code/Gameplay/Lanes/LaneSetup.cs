using UnityEngine;

namespace Content.Code.Gameplay.Level
{
    public class LaneSetup : MonoBehaviour, ILaneSetup
    {
        [SerializeField] private Lane[] lanes;
        [SerializeField] private int startLane;
        [SerializeField] private float playerSpawnOffset;
        [SerializeField] private float enemySpawnOffset;

        #region Getters

        public Lane[] Lanes => lanes;
        public int StartLane => startLane;
        public float PlayerSpawnOffset => playerSpawnOffset;
        public float EnemySpawnOffset => enemySpawnOffset;

        #endregion

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (UnityEditor.Selection.activeGameObject != gameObject)
                return;
        
            // Draw a line for each of the lanes
            for (int i = 0; i < lanes.Length; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(lanes[i].transform.position, lanes[i].transform.position + Vector3.forward * 100);
                // Write the lane number
                DrawString(i.ToString(), lanes[i].transform.position + Vector3.up * 0.5f, Color.white, new Vector2(0.5f, 0.5f));
            }

            if (startLane >= lanes.Length || startLane < 0)
            {
                DrawString("Start lane out of range", lanes[0].transform.position + Vector3.up * 1.5f, Color.yellow, new Vector2(0.5f, 0.5f));
                return;
            }

            // Draw the offset which runs perpendicular to the lanes
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(lanes[0].transform.position + Vector3.forward * playerSpawnOffset,
                lanes[lanes.Length - 1].transform.position + Vector3.forward * playerSpawnOffset);

            if (startLane < lanes.Length)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(lanes[startLane].transform.position + Vector3.forward * playerSpawnOffset, 0.5f);
                DrawString("Start lane " + startLane + " with offset " + playerSpawnOffset, lanes[startLane].transform.position + Vector3.forward * playerSpawnOffset + Vector3.up * 1.5f, Color.green, new Vector2(0.5f, 0.5f));
            }
        
            Gizmos.color = Color.red;
            Gizmos.DrawLine(lanes[0].transform.position + Vector3.forward * enemySpawnOffset,
                lanes[lanes.Length - 1].transform.position + Vector3.forward * enemySpawnOffset);
        
            DrawString("Enemy spawn offset " + enemySpawnOffset, lanes[0].transform.position + Vector3.forward * enemySpawnOffset + Vector3.up * 1.5f, Color.red, new Vector2(0.5f, 0.5f));
        }
    
        /// <summary>
        /// https://gist.github.com/Arakade/9dd844c2f9c10e97e3d0
        /// </summary>
        /// <param name="text"></param>
        /// <param name="worldPos"></param>
        /// <param name="colour"></param>
        static public void DrawString(string text, Vector3 worldPosition, Color textColor, Vector2 anchor,
            float textSize = 15f)
        {
            var view = UnityEditor.SceneView.currentDrawingSceneView;
            if (!view)
                return;
            Vector3 screenPosition = view.camera.WorldToScreenPoint(worldPosition);
            if (screenPosition.y < 0 || screenPosition.y > view.camera.pixelHeight || screenPosition.x < 0 ||
                screenPosition.x > view.camera.pixelWidth || screenPosition.z < 0)
                return;
            var pixelRatio = UnityEditor.HandleUtility.GUIPointToScreenPixelCoordinate(Vector2.right).x -
                             UnityEditor.HandleUtility.GUIPointToScreenPixelCoordinate(Vector2.zero).x;
            UnityEditor.Handles.BeginGUI();
            var style = new GUIStyle(GUI.skin.label)
            {
                fontSize = (int)textSize,
                normal = new GUIStyleState() { textColor = textColor }
            };
            Vector2 size = style.CalcSize(new GUIContent(text)) * pixelRatio;
            var alignedPosition =
                ((Vector2)screenPosition +
                 size * ((anchor + Vector2.left + Vector2.up) / 2f)) * (Vector2.right + Vector2.down) +
                Vector2.up * view.camera.pixelHeight;
            GUI.Label(new Rect(alignedPosition / pixelRatio, size / pixelRatio), text, style);
            UnityEditor.Handles.EndGUI();
#endif
        }
    }
}