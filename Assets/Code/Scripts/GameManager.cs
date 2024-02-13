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
        public event Action PlayerIsWin;
        public event Action UIHealthVisibleChanged;

        [SerializeField] public bool isUIHealthVisible;

        bool isControlPause = false;

        private void OnLevelWasLoaded(int level)
        {
            isControlPause = false;
        }

        private void Update()
        {
            if (isControlPause)
                return;

            if (Input.GetKeyDown(KeyCode.R))
            {
                GameManager.Instance.ReloadLevel();
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                isUIHealthVisible = !isUIHealthVisible;
                UIHealthVisibleChanged?.Invoke();
            }
        }

        public void Win()
        {
            //isControlPause = true;
            AudioPlayer.Instance.Play(gameSettings.winAudio);
            // to do: move fx here
            PlayerIsWin?.Invoke();
        }

        public void GameOver()
        {
            // to do: move fx here
            isControlPause = true;
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
