namespace Content.Code.UnityFeatures.ScriptLifeCycle
{
    public interface IUpdate
    {
        public bool CanUpdate { get; } 
        public void Update();
    }
}