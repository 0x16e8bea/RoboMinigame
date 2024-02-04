using UnityEngine;
using UnityEngine.InputSystem;

namespace Content.Code.Gameplay.Gamepad.Mono
{
    public class GamepadMouseDetector : MonoBehaviour
    {
        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("GamepadButton")))
            {
                // If not clicked do nothing
                if (!Mouse.current.leftButton.wasPressedThisFrame)
                {
                    return;
                }
            
                Debug.Log($"Clicked on {hit.collider.gameObject.name}");

                hit.collider.gameObject.GetComponent<InputButtonSimulator>()?.SimulateClick();
            }
        }
    }
}