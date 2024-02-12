using Assets.Code.Scripts.General.StateMachine;
using System;

namespace Assets.Code.Scripts.Player.StateMachine
{
    internal class IdleState : IState
    {
        PlayerController playerController;
        public IdleState(PlayerController playerController) 
        {
            this.playerController = playerController;
        }
        public void Enter()
        {
            playerController.animator.SetTrigger("isIdle");
            playerController.RestorePosture(); // todo: add smooth rotation
        }

        public void Exit()
        {
        }

        public void Update()
        {
        }
    }
}
