namespace Assets.Code.Scripts.General.StateMachine
{
    public interface IStateFabric<TStateType>
    { 
        public IState Create(TStateType type);
    }
}
