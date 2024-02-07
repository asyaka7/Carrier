using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.SceneManagement;

namespace Assets.Code.Scripts
{
    internal class GameManager : Singletone<GameManager>
    {
        [SerializeField]
        public GameSettings gameSettings;

        public enum PlayerStateType { Live, Dead };

        [HideInInspector]
        public PlayerStateType PlayerState = PlayerStateType.Live;

        protected override void Awake()
        {
            base.Awake();
            PlayerState = PlayerStateType.Live;
        }

        public void Win()
        {
            AudioPlayer.Instance.Play(gameSettings.winAudio);
            //gameSettings.winParticle?.Play();
            // show win screen
            ReloadLevel(gameSettings.reloadDelay);
        }

        public void GameOver()
        {
            AudioPlayer.Instance.Play(gameSettings.crushAudio);
            //gameSettings.crushParticle?.Play();
            // show game over screen
            PlayerState = PlayerStateType.Dead;
            ReloadLevel(gameSettings.reloadDelay);
        }

        public void ReloadLevel(float delay)
        {
            Invoke("ReloadLevel", delay);
        }

        public void ReloadLevel()
        {
            string activeSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(activeSceneName);
        }
    }
}
