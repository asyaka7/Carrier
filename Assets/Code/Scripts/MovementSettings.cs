﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Scripts
{
    [Serializable]
    public struct MovementSettings
    {
        [Header("Fly")]
        public float jumpSpeed;
        public float backSpeed;
        public float directSpeed;
        public float walkSpeed;

        //public BirdPlayerSettings(float jumpSpeed, float backSpeed, float directSpeed)
        //{
        //    //JumpSpeed = 1f;
        //    //backSpeed = 0.5f;
        //    //directSpeed = 3f;

        //    this.jumpSpeed = jumpSpeed;
        //    this.backSpeed = backSpeed;
        //    this.directSpeed = directSpeed;
        //}
    }

    [Serializable]
    internal struct GameSettings
    {
        public float reloadDelay;

        public AudioClip crushAudio;
        public AudioClip winAudio;

        public ParticleSystem winParticle;
        public ParticleSystem crushParticle;

        public GameSettings(float reloadDelay)
        {
            // reloadDelay = 2f
            this.reloadDelay = reloadDelay;

            this.winAudio = null;
            this.crushAudio = null;
            this.winParticle = null;
            this.crushParticle = null;
        }
    }
}
