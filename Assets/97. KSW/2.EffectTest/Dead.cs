using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour
{
    public int maxHP = 100;
    private int currentHP;

    [SerializeField]
    private GameObject extinctionPrefab;

    private void Start()
    {
        currentHP = maxHP;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("slash"))
        {
            TakeDamage(10);
        }
        else if (other.CompareTag("thrust"))
        {
            TakeDamage(20);
        }
    }

    private void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(extinctionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
