using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerachTarget : MonoBehaviour
{
    [SerializeField]
    private Transform Target;

    private float TargetingDistance;

    [SerializeField]
    private bool _IsTargetting = false;

    public bool IsTargetting { get { return _IsTargetting; } set { _IsTargetting = value; } }


}
