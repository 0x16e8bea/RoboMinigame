namespace Content.Code.Gameplay.Enemies
{
    public interface IEnemyController
    {
        void Attack();
        void Move();
        void Kill();
    }
}