using Assets.Code.Scripts.General.StateMachine;
using System;

namespace Assets.Code.Scripts.Player.StateMachine
{
    internal class PlayerStateFabric : IStateFabric<PlayerAnimStateType>
    {
        PlayerController playerController;
        public PlayerStateFabric(PlayerController playerController)
        {
            this.playerController = playerController;
        }

        public IState Create(PlayerAnimStateType type)
        {
            switch (type)
            {
                case PlayerAnimStateType.Idle:
                    return new IdleState(playerController);
                case PlayerAnimStateType.Walk:
                    return new WalkingState(playerController);
                case PlayerAnimStateType.Fly:
                    return new ComplexFlyingState(playerController);
                case PlayerAnimStateType.Dead:
                    return new DeathState(playerController);
                default: throw new NotImplementedException();
            }
        }
    }
}
