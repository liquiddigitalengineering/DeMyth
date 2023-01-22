using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static Action OnGameOver;

    [SerializeField] private float maxHealth;
    [SerializeField] private TextMeshProUGUI healthText;
    private float currentHealth;


    private void OnEnable()
    {
        DamageManager.OnPlayerDamaged += Damaged;
    }

    private void OnDisable()
    {
        DamageManager.OnPlayerDamaged -= Damaged;
    }

    private void Awake()
    {
        currentHealth = maxHealth;
        healthText.text = currentHealth.ToString();
    }

    private void Damaged(float damage)
    {
        currentHealth -= damage;

        if (currentHealth > 1) {
            healthText.text = currentHealth.ToString();
            return;
        }

        OnGameOver?.Invoke();

    }
}
