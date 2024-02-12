using Assets.Code.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Tooltip("In seconds. How long player will live.")]
    [SerializeField] float startHealth = 50;
    [SerializeField] float maxHealth = 100;
    [SerializeField] TextMeshPro healthText;

    [SerializeField] Light playerHeart;
    [SerializeField] float minLightIntensity = 0.5f;
    [SerializeField] float maxLightIntensity = 3;

    float currentHealth = 0;
    bool isPause = false;

    void Start()
    {
        currentHealth = startHealth;
        UpdateLightIntensity();
    }

    private void UpdateLightIntensity()
    {
        if (playerHeart != null)
        {
            playerHeart.intensity = Mathf.Lerp(minLightIntensity, maxLightIntensity, currentHealth / maxHealth);
        }
    }

    void Update()
    {
        if (!isPause)
        {
            currentHealth -= Time.deltaTime;
            UpdateUI();
            UpdateLightIntensity();
            LiveOrDie();
        }
    }

    private void UpdateUI()
    {
        if (healthText != null)
        {
            healthText.text = currentHealth.ToString("F2");
        }
        else
        {
            Debug.Log("Health text UI not found");
        }
    }

    public void AddHealthPoints(float points)
    {
        currentHealth = Mathf.Clamp(currentHealth + points, 0, maxHealth);
    }

    public void TakeHealthPoints(float points)
    {
        currentHealth = Mathf.Clamp(currentHealth - points, 0, maxHealth);
    }

    private void LiveOrDie()
    {
        if (currentHealth < 0.1)
        {
            isPause = true;
            GameManager.Instance.GameOver();
        }
    }
}
