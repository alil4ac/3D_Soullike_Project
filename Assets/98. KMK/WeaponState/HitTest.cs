using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInParent<PlayerController>() != null)
        {
            Debug.Log("Get Hit ->" + this.gameObject.name);
        }
    }
}
