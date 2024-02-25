using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMonsterPlayer : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float fastMovementSpeed;
    public float fastMovementDuration = 0.2f;

    private Rigidbody rb;
    private float currentMovementSpeed;
    private bool isFastMoving;
    private float fastMovementTimer;
    private Collider cd;

    public float hp;
    public float maxHp=200;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cd = GetComponent<Collider>();
        fastMovementSpeed = movementSpeed * 1.5f;
        currentMovementSpeed = movementSpeed;
        isFastMoving = false;
        fastMovementTimer = 0f;

        hp=maxHp;
    }


    /// <summary>
    /// 피격 테스트 코드
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyAtk"))
        {
            hp -= other.GetComponentInParent<Enemy>().EDamage;
            Debug.LogWarning("Player Hitted");
            Debug.Log($"{hp}, {other.GetComponentInParent<Enemy>().EDamage}");
        }

        if (other.gameObject.CompareTag("EnemySAtk"))
        {
            

            if (other.GetComponentInParent<FireBallExplosion>())
            {
                hp -= other.GetComponent<FireBallExplosion>().FireBallDamage;
                Debug.Log($"{hp}, {other.GetComponent<FireBallExplosion>().FireBallDamage}");
            }
            else
            {
                hp -= other.GetComponentInParent<Enemy>().ESDamage;
                Debug.Log($"{hp}, {other.GetComponentInParent<Enemy>().ESDamage}");
            }

            Debug.LogWarning("Player Hitted Hard");
            
        }
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * currentMovementSpeed;

        rb.velocity = movement;

        if (Input.GetKeyDown(KeyCode.Space) && !isFastMoving)
        {
            isFastMoving = true;
            currentMovementSpeed = fastMovementSpeed;
            fastMovementTimer = fastMovementDuration;
        }

        if (isFastMoving)
        {
            fastMovementTimer -= Time.deltaTime;
            if (fastMovementTimer <= 0f)
            {
                isFastMoving = false;
                currentMovementSpeed = movementSpeed;
            }
        }
    }
}

