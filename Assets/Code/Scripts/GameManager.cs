using Assets.Code.Scripts.Player;
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

        public event Action PlayerIsDead;

        protected override void Awake()
        {
            base.Awake();
        }

        public void Win()
        {
            AudioPlayer.Instance.Play(gameSettings.winAudio);
            // to do: move fx here
            ReloadLevel(gameSettings.reloadDelay);
        }

        public void GameOver()
        {
            // to do: move fx here
            //Invoke("StartReloading", 2);
            StartReloading();
        }

        private void StartReloading()
        {
            PlayerIsDead?.Invoke();
            ReloadLevel(gameSettings.deathDuration + gameSettings.reloadDelay);
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
