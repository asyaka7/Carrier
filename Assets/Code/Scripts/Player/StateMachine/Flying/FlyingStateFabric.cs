using Assets.Code.Scripts.General.StateMachine;
using System;

namespace Assets.Code.Scripts.Player.StateMachine.Flying
{
    public class FlyingStateFabric : IStateFabric<PlayerFlyAnimStateType>
    {
        PlayerController playerController;
        public FlyingStateFabric(PlayerController playerController)
        {
            this.playerController = playerController;
        }
        public IState Create(PlayerFlyAnimStateType type)
        {
            switch (type)
            {
                case PlayerFlyAnimStateType.Acceleration:
                    return new FlyAccelerationState(playerController);
                case PlayerFlyAnimStateType.Down:
                    break;
                case PlayerFlyAnimStateType.Fly:
                    return new BaseFlyingState(playerController);
            }
            throw new NotImplementedException();
        }
    }
}
