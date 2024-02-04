using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Content.Code.Gameplay.Gamepad.Mono
{
    public class InputButtonSimulator : MonoBehaviour
    {
        [SerializeField] string key;

        public void SimulateClick()
        {
            // Convert string to Keycode
            Enum.TryParse<Key>(key, out var keyCode);
        
            if (keyCode == Key.None)
            {
                Debug.LogError($"Key {key} not found");
                return;
            }
        
            Debug.Log($"Simulating click on {keyCode}");

            InputSystem.QueueStateEvent(Keyboard.current, new KeyboardState(keyCode));
            InputSystem.QueueStateEvent(Keyboard.current, new KeyboardState());
        }

    }
}