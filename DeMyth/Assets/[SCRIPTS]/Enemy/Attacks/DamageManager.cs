using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public Action<float> OnPlayerDamaged;

    public void DamagePlayer(float damage)
    {
        Debug.Log("Player damaged");
        //OnPlayerDamaged?.Invoke(damage);
    }
}
