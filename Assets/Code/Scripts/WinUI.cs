using Assets.Code.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinUI : MonoBehaviour
{
    [SerializeField] GameObject bodyUI;

    void Start()
    {
        GameManager.Instance.PlayerIsWin += PlayerIsWin;
    }

    private void PlayerIsWin()
    {
        Invoke("Activate", GameManager.Instance.gameSettings.winPoseDuration);
    }

    private void Activate()
    {
        bodyUI.SetActive(true);
    }

    private void OnDestroy()
    {
        GameManager.Instance.PlayerIsWin -= PlayerIsWin;
    }
}
