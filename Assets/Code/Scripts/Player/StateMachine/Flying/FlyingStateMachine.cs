using Assets.Code.Scripts.General.StateMachine;

namespace Assets.Code.Scripts.Player.StateMachine.Flying
{
    internal class FlyingStateMachine : StateMachine<PlayerFlyAnimStateType>
    {
        public FlyingStateMachine(FlyingStateFabric flyingStateFabric) : base(flyingStateFabric)
        {

        }

    }

}
