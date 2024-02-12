﻿using Assets.Code.Scripts.General.StateMachine;
using System;

namespace Assets.Code.Scripts.Player.StateMachine
{
    internal class DeathState : IState
    {
        PlayerController playerController;
        public DeathState(PlayerController playerController) 
        { 
            this.playerController = playerController;
        }
        public void Enter()
        {
            playerController.animator.SetTrigger("isDead");
            //.SetBool("isDead", true);
        }

        public void Exit()
        {
            //playerController.animator.SetBool("isDead", false);
        }

        public void Update()
        {
            // play feather blust after some time
        }
    }
}