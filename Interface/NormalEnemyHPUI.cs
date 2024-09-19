using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NormalEnemyHPUI : MonoBehaviour
{
    #region SerializeField Component

    
    [SerializeField]
    private Slider Health;

    [SerializeField]
    private Slider Back_Health;

    [SerializeField]
    private Image Health_Fill;

    [SerializeField]
    private Image Back_Health_Background;

    [SerializeField]
    private Image Back_Health_Fill;

    Enemy parent;
    CanvasGroup canvasGroup;

    #endregion

    private float MaxHealth;

    private bool IsActive = false;


    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        parent= gameObject.transform.root.GetComponent<Enemy>();
        UIStart();
    }

    public void UIStart()
    {
        canvasGroup.alpha = 0f;

        Health_Fill.color = new Color(1, 1, 1, 0f);

        Back_Health_Background.color = new Color(1, 1, 1, 0f);

        Back_Health_Fill.color = new Color(1, 1, 1, 0f);

        
    }

    public void StartBattle()
    {
        
        MaxHealth= parent.MaxHp;
        
        Health.value = Health.maxValue;
        Back_Health.value = Back_Health.maxValue;

        canvasGroup.alpha = 1f;
        StartCoroutine(StartHpValue());
    }

    private IEnumerator StartHpValue()
    {
        float progess = 0f;
        while (progess < 1f)
        {
            

            Health_Fill.color = new Color(1, 1, 1, progess);

            Back_Health_Background.color = new Color(1, 1, 1, progess);

            Back_Health_Fill.color = new Color(1, 1, 1, progess);

            progess += Time.deltaTime / 2;

            yield return new WaitForEndOfFrame();
        }

        if (progess >= 1f)
        {
            yield break;
        }
    }

    public void Hit(float health)
    {
        Health.value = health / MaxHealth * 100f;

        if (IsActive == false)
        {
            IsActive = true;
            StartCoroutine(HitValue(health));
        }
    }

    private IEnumerator HitValue(float health, float timer = 1f, float damp = 0.5f)
    {

        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if (timer <= 0)
        {
            while (Back_Health.value > Health.value)
            {
                Back_Health.value -= damp;
                yield return new WaitForEndOfFrame();
            }

            Debug.LogWarning(Back_Health.value + "/" + Health.value);
        }

        if (Back_Health.value <= Health.value)
        {
            Back_Health.value = Health.value;
            IsActive = false;
            yield break;
        }
    }
}