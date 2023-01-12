using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public static Action OnEnemyDeath;

    [SerializeField] private Slider slider;
    [SerializeField] private EnemyHealthSO healthSO;

    private float currentHealth;

    private void Awake()
    {     
        slider.maxValue = healthSO.MaxHealth;
        slider.value = healthSO.MaxHealth;
        currentHealth = healthSO.MaxHealth;
    }


    private void OnDamaged(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0) OnEnemyDeath?.Invoke();

        slider.value = currentHealth;
    }
}
