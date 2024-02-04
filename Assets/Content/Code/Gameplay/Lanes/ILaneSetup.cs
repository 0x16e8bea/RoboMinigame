namespace Content.Code.Gameplay.Lanes
{
    public interface ILaneSetup
    {
        Lane[] Lanes { get; }
        int StartLane { get; }
        float PlayerSpawnOffset { get; }
        float EnemySpawnOffset { get; }
    }
}