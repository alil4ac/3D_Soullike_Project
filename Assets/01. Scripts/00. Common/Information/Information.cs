using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Information : MonoBehaviour
{
    protected int _level;

    public int Level { get { return _level; } private set{ _level = value; } }

    protected int _health;

    public int Health { get { return _health; } private set { _health = value; } }

    protected int _maxHealth;

    public int MaxHealth { get { return _maxHealth; } private set { _maxHealth = value; } }

    protected float _moveSpeed;

    public float MoveSpeed { get { return _moveSpeed; } private set { _moveSpeed = value; } }}
