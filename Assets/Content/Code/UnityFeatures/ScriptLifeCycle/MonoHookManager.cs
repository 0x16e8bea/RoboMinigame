using System.Collections.Generic;

namespace Content.Code.UnityFeatures.ScriptLifeCycle
{
    public class MonoHookManager : IMonoHookManager
    {
        private readonly ICollection<IUpdate> _updateEnumerator = new List<IUpdate>();
        private readonly ICollection<IFixedUpdate> _fixedUpdateEnumerator = new List<IFixedUpdate>();
        private readonly ICollection<IOnApplicationQuit> _applicationQuitEnumerator = new List<IOnApplicationQuit>();

        public void AddUpdateListener(IUpdate update)
        {
            _updateEnumerator.Add(update);
        }

        public void RemoveUpdateListener(IUpdate update)
        {
            _updateEnumerator.Remove(update);
        }

        public void AddFixedUpdateListener(IFixedUpdate update)
        {
            _fixedUpdateEnumerator.Add(update);
        }

        public void RemoveFixedUpdateListener(IFixedUpdate update)
        {
            _fixedUpdateEnumerator.Remove(update);
        }

        public void AddOnApplicationQuitListener(IOnApplicationQuit onApplicationQuit)
        {
            _applicationQuitEnumerator.Add(onApplicationQuit);
        }

        public void RemoveOnApplicationQuitListener(IOnApplicationQuit onApplicationQuit)
        {
            _applicationQuitEnumerator.Remove(onApplicationQuit);
        }

        public IEnumerable<IUpdate> UpdateEnumerator()
        {
            return _updateEnumerator;
        }

        public IEnumerable<IFixedUpdate> FixedUpdateEnumerator()
        {
            return _fixedUpdateEnumerator;
        }

        public IEnumerable<IOnApplicationQuit> OnApplicationQuitEnumerator()
        {
            return _applicationQuitEnumerator;
        }
    }
}
