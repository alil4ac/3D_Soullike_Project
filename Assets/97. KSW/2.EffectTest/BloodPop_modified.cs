using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPop_modified : MonoBehaviour
{
    [SerializeField]
    GameObject BloodPrefab1;
    [SerializeField]
    GameObject BloodPrefab2;
    [SerializeField]
    GameObject BloodPrefab3;
    [SerializeField]
    GameObject BloodPrefab4;
    [SerializeField]
    GameObject BloodPrefab5;
    [SerializeField]
    GameObject BloodPrefab6;

    private Collider[] col;
    private Rigidbody rigid;

    void Start()
    {
        
        col = GetComponentsInChildren<Collider>();
        rigid = GetComponent<Rigidbody>();

       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer==LayerMask.NameToLayer("PlayerAtk"))
        {
            Vector3 contactPoint = other.ClosestPoint(transform.position);
            Vector3 direction = (contactPoint - transform.position).normalized;
            Quaternion spawnRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(30, -65, 70);

            int randomIndex = Random.Range(1, 4);
            switch (randomIndex)
            {
                case 1:
                    Instantiate(BloodPrefab1, contactPoint, spawnRotation);
                    break;
                case 2:
                    Instantiate(BloodPrefab2, contactPoint, spawnRotation);
                    break;
                case 3:
                    Instantiate(BloodPrefab3, contactPoint, spawnRotation);
                    break;
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAtk"))
        {
            Vector3 contactPoint = other.ClosestPoint(transform.position);
            Vector3 direction = (contactPoint - transform.position).normalized;
            Quaternion spawnRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(30, -65, 70);

            int randomIndex = Random.Range(4, 7);
            switch (randomIndex)
            {
                case 4:
                    Instantiate(BloodPrefab4, contactPoint, spawnRotation);
                    break;
                case 5:
                    Instantiate(BloodPrefab5, contactPoint, spawnRotation);
                    break;
                case 6:
                    Instantiate(BloodPrefab6, contactPoint, spawnRotation);
                    break;
            }
        }
    }
}
