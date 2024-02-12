using Assets.Code.Scripts;
using Assets.Code.Scripts.Player;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshPro reloadText;
    [SerializeField] GameObject bodyUI;

    float reloadTime;

    void Start()
    {
        reloadTime = GameManager.Instance.gameSettings.reloadDelay;
        GameManager.Instance.PlayerIsDead += PlayerIsDead;
    }

    private void PlayerIsDead()
    {
        SetReloadText(reloadTime);
        bodyUI.SetActive(true);
    }

    void Update()
    {
        if (bodyUI != null && bodyUI.activeSelf)
        {
            reloadTime -= Time.deltaTime;
            SetReloadText(reloadTime);
        }
    }

    private void SetReloadText(float value)
    {
        float roundValue = Mathf.Ceil(value); // to reduce "0" time on screen
        reloadText.text = roundValue.ToString("F0");
    }

    private void OnDestroy()
    {
        GameManager.Instance.PlayerIsDead -= PlayerIsDead;
    }
}
