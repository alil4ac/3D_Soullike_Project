using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallExplosion : MonoBehaviour
{
    [SerializeField]
    float fireballDamage;

    public float FireBallDamage => fireballDamage;
    
    [SerializeField]
    GameObject ExplosionPrefab;

    
    [SerializeField]
    EnemyData enemyData;

    private void Awake()
    {
        
        fireballDamage = enemyData.ESDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.layer==LayerMask.NameToLayer("PlayerHit")|| other.gameObject.layer==LayerMask.NameToLayer("Ground"))
        {
            
            GameObject explode = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            GameSoundManager.Instance.SetMonsterFx("Explosion15", this.transform);
            Destroy(explode, 5f);
            Destroy(this.gameObject);
            
        }
    }
}
