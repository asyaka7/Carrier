using System;
using UnityEngine;
using Assets.Code.Scripts.General.StateMachine;
using Assets.Code.Scripts.Player.StateMachine.Flying;

namespace Assets.Code.Scripts.Player.StateMachine
{
    public enum PlayerFlyAnimStateType { Acceleration, Fly, Down };
    internal class ComplexFlyingState : IState
    {
        PlayerController playerController;
        FlyingStateMachine stateMachine;
        public ComplexFlyingState(PlayerController playerController)
        {
            this.playerController = playerController;
            FlyingStateFabric stateFabric = new FlyingStateFabric(playerController);
            stateMachine = new FlyingStateMachine(stateFabric);
        }

        public void Enter()
        {
            playerController.animator.SetTrigger("isFlying");
            //.SetBool("isFlying", true);
            stateMachine.InitState(PlayerFlyAnimStateType.Acceleration);
            
            if (stateMachine.CurrentState != null)
            {
                FlyAccelerationState flyAccelerationState = stateMachine.CurrentState as FlyAccelerationState;
                flyAccelerationState.OnFinished += OnAccelerationFinished;
            }
        }

        private void OnAccelerationFinished()
        {
            stateMachine.InitState(PlayerFlyAnimStateType.Fly);
            //stateMachine.CurrentState.OnFinished -= OnAccelerationFinished;
        }

        public void Exit()
        {
            //playerController.animator.SetBool("isFlying", false);
        }

        public void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                MakeFlap();
            }

            stateMachine.Update();
        }

        private void MakeFlap()
        {
            PlayFlySound();
        }

        private void PlayFlySound()
        {
            if (!AudioPlayer.Instance.IsPlaying)
            {
                AudioPlayer.Instance.Play(playerController.flapAudio);
            }
        }
    }
}
