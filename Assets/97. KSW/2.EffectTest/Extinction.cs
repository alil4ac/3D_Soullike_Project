using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinction : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 3f);
    }
}
