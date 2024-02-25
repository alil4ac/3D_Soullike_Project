using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable Object/Enemy Data", order = int.MaxValue)]
public class EnemyData : ScriptableObject
{
    [SerializeField]
    float eHP; //실질적 최대 HP값.
    public float EHP { get { return eHP; } }


    [SerializeField]
    float eDamage;
    public float EDamage { get { return eDamage; } }

    [SerializeField]
    float eSDamage;
    public float ESDamage { get { return eSDamage; } }


    [SerializeField]
    float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }

    //[SerializeField]
    //float rotationSpeed;
    //public float RotationSpeed { get { return rotationSpeed; } }

    //[Header("0~1")]
    //[SerializeField]
    //float atkDelay=1f;
    //public float AtkDelay { get { return atkDelay; } }

    [SerializeField]
    float sigthRange;
    public float SightRange { get { return sigthRange; } }

    [SerializeField]
    float closeSigthRange;
    public float CloseSightRange { get { return closeSigthRange; } }

    [SerializeField]
    float sightHalfAngle;
    public float SightHalfAngle { get { return sightHalfAngle; } }

    [SerializeField]
    float meleeAtkRange;
    public float MeleeAtkRange { get { return meleeAtkRange; } }

    
  
    
    [Header("turnSpeed must be in 0~1")]
    [SerializeField]
    float turnSpeed;
    public float TurnSpeed { get { return turnSpeed; } }

    //[SerializeField]
    //float minDistance = 2f;
    //public float MinDistance { get { return minDistance; } }

    [Header("경험치")]
    [SerializeField]
    int exp;
    public int Exp { get { return exp; } }

    
    //    [SerializeField]
    //    float targetRadius;
    //    public float TargetRadius { get { return targetRadius; } }

    //    [SerializeField]
    //    float targetRange;
    //    public float TargetRange { get { return targetRange; } }
}
