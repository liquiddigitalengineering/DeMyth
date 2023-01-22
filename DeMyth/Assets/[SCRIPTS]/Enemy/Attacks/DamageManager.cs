using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public static Action<float> OnPlayerDamaged;

    public void DamagePlayer(float damage)
    {
        OnPlayerDamaged?.Invoke(damage);
    }
}
