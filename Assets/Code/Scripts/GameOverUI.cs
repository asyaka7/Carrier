using Assets.Code.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshPro reloadText;
    [SerializeField] GameObject bodyUI;

    float reloadTime;

    // Start is called before the first frame update
    void Start()
    {
        reloadTime = GameManager.Instance.gameSettings.reloadDelay;
        SetReloadText(reloadTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.PlayerState == GameManager.PlayerStateType.Live)
        {
            bodyUI.SetActive(false);
        }

        if (GameManager.Instance.PlayerState == GameManager.PlayerStateType.Dead)
        {
            if (bodyUI != null)
            {
                bodyUI.SetActive(true);
                reloadTime -= Time.deltaTime;
                SetReloadText(reloadTime);
            }
        }
    }

    private void SetReloadText(float value)
    {
        reloadText.text = value.ToString("F0");
    }
}
