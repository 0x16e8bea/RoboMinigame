using Content.Code.Gameplay.Enemies;
using Content.Code.Gameplay.Enemies.FX;
using Content.Code.Gameplay.Enemies.Instance;
using Content.Code.Gameplay.Level;
using Content.Code.Gameplay.Robot.Projectiles;
using UnityEngine;

public class EnemyCollisionNotifier : IEnemyCollisionNotifier
{
    private readonly IEnemyRepository _enemyRepository;
    private readonly ILaneManager _laneManager;
    private readonly IEnemyDeathFXController _enemyDeathFXController;

    public EnemyCollisionNotifier(
        IParticleCollisionNotifier particleCollisionNotifier,
        IEnemyRepository enemyRepository,
        ILaneManager laneManager,
        IEnemyDeathFXController enemyDeathFXController)
    {
        _enemyRepository = enemyRepository;
        _laneManager = laneManager;
        _enemyDeathFXController = enemyDeathFXController;
        particleCollisionNotifier.AddListener(OnParticleCollision);
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Collision with " + other.name + " instance ID: " + other.GetInstanceID());

        // Try to get the enemy from the repository using the GameObject instance ID
        if (_enemyRepository.TryGetEnemy(other.GetInstanceID(), out IEnemyInstance instance))
        {
            _laneManager.GetLane(instance.LaneIndex).IsEnemySpotOccupied = false;
            instance.Destroy();
            _enemyRepository.RemoveEnemy(instance);
            _enemyDeathFXController.PlayDeathParticlesAtLocation(instance.GameObject.transform.position);
        }
    }
}