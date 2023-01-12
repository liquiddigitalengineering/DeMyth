using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="enenmyHealth", menuName ="Enemy/Health")]
public class EnemyHealthSO : ScriptableObject
{
    [Min(1)]
    public float MaxHealth;
}
