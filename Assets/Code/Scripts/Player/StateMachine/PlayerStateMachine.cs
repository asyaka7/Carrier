using Assets.Code.Scripts.General.StateMachine;

namespace Assets.Code.Scripts.Player.StateMachine
{
    public enum PlayerAnimStateType { Idle, Fly, Walk, Dead };

    internal class PlayerStateMachine : StateMachine<PlayerAnimStateType>
    {
        public PlayerStateMachine(PlayerStateFabric playerStateFabric) : base(playerStateFabric)
        {
            
        }
    }   
}
