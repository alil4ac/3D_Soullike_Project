using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSet : MonoBehaviour
{
    private ParticleSystem[] effects;

    private Collider col;


    public float speed = 60f;

    public float lifeTime = 0.15f;
    private void Awake()
    {
        effects = this.GetComponentsInChildren<ParticleSystem>();
        col = this.GetComponentInChildren<Collider>();
        col.enabled = false;
    }

    private void Start()
    {
        this.enabled = false;
    }

    public void SetStartEffect(Transform tr)
    {
        lifeTime = 0.15f;
        Vector3 crs = Vector3.Cross(tr.right, Vector3.up);
        this.transform.position = tr.position;
        this.transform.rotation = Quaternion.LookRotation(crs);
        col.enabled = true;
        col.transform.localRotation = Quaternion.LookRotation(crs);
        SetEndEffect();
    }

    public void SetEndEffect()
    {
        StartCoroutine(_SetEndEffect());
    }

    private IEnumerator _SetEndEffect()
    {
        while (true)
        {
            int chk = 1;
            foreach (ParticleSystem eff in effects)
            {
                if (eff.isPlaying == false)
                {
                    chk++;
                }
                if (chk == effects.Length)
                {
                    col.transform.localPosition = Vector3.zero;
                    this.gameObject.SetActive(false);
                    yield break;
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private void Update()
    {
        if (lifeTime <= 0f)
            return;

        else
        {
            lifeTime -= Time.deltaTime;
            col.transform.localPosition += Vector3.forward * Time.deltaTime * 70f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<HitTest>() != null)
        {
            foreach(ParticleSystem eff in effects)
            {
                eff.Stop();
            }
        }
    }
}
