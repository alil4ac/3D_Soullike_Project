using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    GameObject ExplosionPrefab;
    private void OnTriggerEnter(Collider other)
    {
        GameObject explode = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(explode, 5f);
        Destroy(gameObject);

    }
}
