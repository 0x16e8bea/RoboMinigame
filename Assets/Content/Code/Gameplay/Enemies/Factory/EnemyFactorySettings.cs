using UnityEngine;

namespace Content.Code.Gameplay.Enemies.Factory
{
    public record EnemyFactorySettings(
        GameObject Enemy1Prefab, 
        GameObject Enemy2Prefab, 
        GameObject Enemy3Prefab);
}