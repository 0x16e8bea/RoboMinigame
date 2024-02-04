namespace Content.Code.UnityFeatures.ScriptLifeCycle
{
    public interface IFixedUpdate
    {
        bool CanFixedUpdate { get; }
        void FixedUpdate();
    }
}