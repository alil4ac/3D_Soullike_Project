using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform _tr;

    public Transform Tr { get { return _tr; } private set { _tr = value; } }

    private void Awake()
    {
        _tr = this.transform;
    }
}
