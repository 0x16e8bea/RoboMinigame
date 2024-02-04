using Content.Code.Gameplay.Enemies;
using Content.Code.Gameplay.Enemies.Instance;
using Content.Code.Gameplay.Robot.Projectiles;
using UnityEngine;

public class EnemyCollisionNotifier : IEnemyCollisionNotifier
{
    private readonly IEnemyRepository _enemyRepository;

    public EnemyCollisionNotifier(
        IParticleCollisionNotifier particleCollisionNotifier,
        IEnemyRepository enemyRepository)
    {
        _enemyRepository = enemyRepository;
        particleCollisionNotifier.AddListener(OnParticleCollision);
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Collision with " + other.name + " instance ID: " + other.GetInstanceID());
        
        // Try to get the enemy from the repository using the GameObject instance ID
        if (_enemyRepository.TryGetEnemy(other.GetInstanceID(), out IEnemyInstance instance))
        {
            _enemyRepository.RemoveEnemy(instance);
            instance.Destroy();
        }

    }
}