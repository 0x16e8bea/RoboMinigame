using System.Threading;
using Cysharp.Threading.Tasks;
using UnityHFSM;

namespace Content.Code.Gameplay.Enemies
{
    public class SimpleEnemyStateMachine : IEnemyStateMachine
    {
        private StateMachine<State, Trigger> _fsm;
        private CancellationTokenSource _cancellationTokenSource; // For managing task cancellation.
        private readonly IEnemyController _enemyController;

        enum Trigger
        {
            StartAttack,
            EndAttack,
            DamageTaken,
        }

        enum State
        {
            Idle,
            Attack,
        }


        public SimpleEnemyStateMachine(IEnemyController enemyController)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            _enemyController = enemyController;
            _fsm = new StateMachine<State, Trigger>();

            _fsm.AddState(State.Idle);
            _fsm.AddState(State.Attack, OnEnterAttack);

            _fsm.AddTriggerTransition(Trigger.StartAttack, State.Idle, State.Attack);
            _fsm.AddTriggerTransition(Trigger.EndAttack, State.Attack, State.Idle);
            _fsm.SetStartState(State.Idle);

            _fsm.Init();
            
            _fsm.Trigger(Trigger.StartAttack);
            
            
        }
        
        public void OnDeath()
        {
            CancelAttack();
        }

        private void OnEnterAttack(State<State, Trigger> obj)
        {
            AttackRoutine(_cancellationTokenSource.Token).Forget();
        }

        private async UniTask AttackRoutine(CancellationToken cancellationToken)
        {
            _enemyController.Attack();
            await UniTask.Delay(2000, cancellationToken: cancellationToken);
            _fsm.Trigger(Trigger.EndAttack);
            await UniTask.Delay(2000, cancellationToken: cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            _fsm.Trigger(Trigger.StartAttack);
        }

        private void CancelAttack()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = new CancellationTokenSource(); // Reset the token source for future use.
        }
    }
}