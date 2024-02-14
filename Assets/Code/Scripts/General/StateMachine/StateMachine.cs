using UnityEngine;

namespace Assets.Code.Scripts.General.StateMachine
{
    internal class StateMachine<TStateType>
    {
        public IState CurrentState { get; private set; }
        public TStateType CurrentStateType { get; private set; }

        IStateFabric<TStateType> stateFabric;


        public StateMachine(IStateFabric<TStateType> stateFabric)
        {
            this.stateFabric = stateFabric;
        }

        public virtual void InitState(TStateType type)
        {
            IState state = stateFabric.Create(type);
            if (CurrentState != null)
            {
                if (Debug.isDebugBuild)
                {
                    Debug.Log("Exit " + CurrentStateType.ToString());
                }
                state.Exit();
            }
            CurrentState = state;

            if (Debug.isDebugBuild)
            {
                Debug.Log("Enter " + type.ToString());
            }

            state.Enter();
            CurrentStateType = type;
        }

        public virtual void TransitTo(TStateType type)
        {
            if (CurrentStateType.Equals(type)) return;
            InitState(type);
        }

        public virtual void Update()
        {
            CurrentState.Update();
        }
    }
}
