using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveStone : MonoBehaviour
{
    [SerializeField] private BasicSlam slam;

    private DamageManager damageManager;
    private float damage;

    private void Awake()
    {
        damageManager = GameObject.FindGameObjectWithTag("Player").GetComponent<DamageManager>();
        damage = slam.GetGraveStoneDamage; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Debug.Log("Player lol");
            //damageManager.DamagePlayer(damage);
    }

    public void DisableStone() => this.gameObject.SetActive(false);
}
