using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Content.Code.UnityFeatures.ScriptLifeCycle
{
    public class MonoHook : MonoBehaviour
    {
        protected IEnumerable<IUpdate> updateEnumerator = new List<IUpdate>();
        protected IEnumerable<IFixedUpdate> fixedUpdateEnumerator = new List<IFixedUpdate>();
        protected IEnumerable<IOnApplicationQuit> onApplicationQuitEnumerator = new List<IOnApplicationQuit>();

        // Serialize field is used to show the value in the inspector
        // Add attribute to grey it out in the inspector
        [SerializeField] private int updateListenerCount;
        [SerializeField] private int fixedUpdateListenerCount;
        [SerializeField] private int onApplicationQuitListenerCount;

        public void Initialize(IMonoHookManager monoHookManager)
        {
            // Get the correct enumerator from the monoHookManager based on T
            updateEnumerator = monoHookManager.UpdateEnumerator();
            fixedUpdateEnumerator = monoHookManager.FixedUpdateEnumerator();
            onApplicationQuitEnumerator = monoHookManager.OnApplicationQuitEnumerator();
        

            // Get the count of the listeners
            updateListenerCount = updateEnumerator.Count();
            fixedUpdateListenerCount = fixedUpdateEnumerator.Count();
            onApplicationQuitListenerCount = onApplicationQuitEnumerator.Count();
        }
    
        void Update()
        {
            foreach (var listener in updateEnumerator)
            {
                if (listener.CanUpdate)
                    listener.Update();
            }
        }
        
        private void FixedUpdate()
        {
            foreach (var listener in fixedUpdateEnumerator)
            {
                if (listener.CanFixedUpdate)
                    listener.FixedUpdate();
            }
        }

        private void OnApplicationQuit()
        {
            foreach (var listener in onApplicationQuitEnumerator)
            {
                listener.OnApplicationQuit();
            }
        }

    }
}