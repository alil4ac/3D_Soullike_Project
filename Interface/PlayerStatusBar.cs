using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStatusBar : MonoBehaviour
{
    #region Status Slider

    [SerializeField]
    private Slider Health;

    [SerializeField]
    private Slider Back_Health;

    [SerializeField]
    private Slider ForcePoint;

    [SerializeField]
    private Slider Back_ForcePoint;

    [SerializeField]
    private Slider Stamina;

    [SerializeField]
    private Slider Back_Stamina;

    #endregion

    #region IsOnCoroutine

    private bool IsOnHP = false;

    private bool IsOnFP = false;

    private bool IsOnStamina = false;

    #endregion

    #region Method

    private void Init()
    {
        Health.maxValue = 100f;
        Health.value = Health.maxValue;
        ForcePoint.value = 100f;
        ForcePoint.value = ForcePoint.maxValue;
        Stamina.value = 100f;
        Stamina.maxValue = Stamina.maxValue;


        Back_Health.maxValue = 100f;
        Back_Health.value = Back_Health.maxValue;
        Back_ForcePoint.value = 100f;
        Back_ForcePoint.value = Back_ForcePoint.maxValue;
        Back_Stamina.value = 100f;
        Back_Stamina.maxValue = Back_Stamina.maxValue;
    }

    public void LoadData()
    {
        Health.value = (CharacterManager.Instance.Data.Health / CharacterManager.Instance.Data.MaxHealth) * 100;
        Back_Health.value = Health.value;
        ForcePoint.value = (CharacterManager.Instance.Data.ForcePoint / CharacterManager.Instance.Data.MaxForcePoint) * 100;
        Back_ForcePoint.value = (CharacterManager.Instance.Data.Stamina / CharacterManager.Instance.Data.MaxStamina) * 100;
        Stamina.value = 100;
    }

    #region HP

    public void HitHP()
    {
        Health.value = (CharacterManager.Instance.Data.Health / CharacterManager.Instance.Data.MaxHealth) * 100f;
        if(IsOnHP == false)
        {
            IsOnHP = true;
            StartCoroutine(HitHPValue());
        }
    }

    private IEnumerator HitHPValue(float delay = 1.5f, float damp = 1f)
    {
        while (Health.value < Back_Health.value)
        {
            if (delay > 0)
            {
                delay -= Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }
            else
            {
                Back_Health.value -= damp;

                yield return new WaitForEndOfFrame();
            }
        }

        if (Back_Health.value <= Health.value)
        {
            Back_Health.value = Health.value;

            IsOnHP = false;

            yield break;
        }
    }

    public void UsePotion()
    {
        StartCoroutine(UsePotionValue());
    }

    private IEnumerator UsePotionValue(float damp = 1f)
    {
        while (true)
        {
            Health.value = CharacterManager.Instance.Data.Health / CharacterManager.Instance.Data.MaxHealth * 100f;

            if (IsOnHP == false && Health.value >= Health.maxValue)
            {
                Health.value = Health.maxValue;
                yield break;
            }
            else if (IsOnHP == true)
            {
                yield break;
            }

            yield return new WaitForEndOfFrame();

        }
    }

    #endregion

    #region FP

    public void UseFP()
    {
        ForcePoint.value = (CharacterManager.Instance.Data.ForcePoint - CharacterManager.Instance.Data.MaxForcePoint) / 100f;
        StartCoroutine(UseFPValue());
    }

    private IEnumerator UseFPValue(float delay = 0.5f, float damp = 0.01f)
    {
        if (IsOnFP) { yield break; }
        else
        {
            IsOnFP = true;

            while (ForcePoint.value < Back_ForcePoint.value)
            {
                if (delay > 0)
                {
                    delay -= Time.deltaTime;

                    yield return new WaitForEndOfFrame();
                }
                else
                {
                    Back_ForcePoint.value -= damp;

                    yield return new WaitForEndOfFrame();
                }
            }

            if (Back_ForcePoint.value <= ForcePoint.value)
            {
                Back_ForcePoint.value = ForcePoint.value;

                IsOnFP = false;

                yield break;
            }
        }
    }

    #endregion

    #region UseStamina

    public void UseStamina()
    {
        Stamina.value = (CharacterManager.Instance.Data.Stamina / CharacterManager.Instance.Data.MaxStamina) * 100f;
        if(IsOnStamina == false)
        {
            IsOnStamina = true;
            StartCoroutine(UseStaminaValue());
        }
    }

    private IEnumerator UseStaminaValue()
    {
        while (true)
        {
            Stamina.value = CharacterManager.Instance.Data.Stamina / CharacterManager.Instance.Data.MaxStamina * 100f;

            if (Stamina.value >= Stamina.maxValue)
            {
                IsOnStamina = false;
                yield break;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    #endregion

    #endregion

    private void Awake()
    {
        Init();
    }
}
