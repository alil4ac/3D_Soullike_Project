using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BossHP : MonoBehaviour
{
    #region SerializeField Component

    [SerializeField]
    private TextMeshProUGUI BossName;

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

    #endregion

    private float MaxHealth;

    private bool IsActive = false;

    public void UIStart()
    {
        BossName.color = new Color(1, 1, 1, 0f);

        Health_Fill.color = new Color(1, 1, 1, 0f);

        Back_Health_Background.color = new Color(1, 1, 1, 0f);

        Back_Health_Fill.color = new Color(1, 1, 1, 0f);

        this.gameObject.SetActive(false);
    }

    public void StartBossBattle(float MaxHP, string name)
    {
        MaxHealth = MaxHP;
        BossName.text = name;
        Health.value = Health.maxValue;
        Back_Health.value = Back_Health.maxValue;
        StartCoroutine(StartBossValue());
    }

    private IEnumerator StartBossValue()
    {
        float progess = 0f;
        while (progess < 1f)
        {
            BossName.color = new Color(1, 1, 1, progess);

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

    public void HitBoss(float health)
    {
        Health.value = health / MaxHealth * 100f;

        if (IsActive == false)
        {
            IsActive = true;
            StartCoroutine(HitHossValue(health));
        }
    }

    private IEnumerator HitHossValue(float health, float timer = 1f, float damp = 0.5f)
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

    public void EndBossValue()
    {
        StartCoroutine(_EndBossValue());
    }

    private IEnumerator _EndBossValue()
    {
        float value = 1f;

        while (true)
        {
            if(value > 0f)
            {
                BossName.color = new Color(1, 1, 1, value);
                BossName.color = new Color(1, 1, 1, value);
                BossName.color = new Color(1, 1, 1, value);
                BossName.color = new Color(1, 1, 1, value);
                BossName.color = new Color(1, 1, 1, value);
                BossName.color = new Color(1, 1, 1, value);

                value -= Time.fixedDeltaTime;

            }

            if(value <= 0f)
            {
                BossName.color = new Color(1, 1, 1, 0);
                BossName.color = new Color(1, 1, 1, 0);
                BossName.color = new Color(1, 1, 1, 0);
                BossName.color = new Color(1, 1, 1, 0);
                BossName.color = new Color(1, 1, 1, 0);
                BossName.color = new Color(1, 1, 1, 0);

                this.gameObject.SetActive(false);

                yield break;
            }

            yield return new WaitForFixedUpdate();
        }
    }

}
