using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LevelUpLayer : MonoBehaviour
{
    private Color DefaultColor = new Color(0.8588235f, 0.8667861f, 0.9137255f, 1f);

    private int NeedRune
    {
        get
        {
            return NextLevelUpStack  * 100;
        }
    }


    private int stack = 0;
    private int NextLevelUpStack
    {
        get
        {
            stack = 0;
            for (int i = 0; i < IsStatusUp.Length; i++)
            {
                stack += IsStatusUp[i];
            }
            return stack;
        }
    }

    [SerializeField]
    private int[] IsStatusUp = new int[5] { 0, 0, 0, 0, 0 };

    [SerializeField]
    private LevelUpButton[] LevelUpButtons;

    [SerializeField]
    private LevelDownButton[] LevelDownButtons;

    [SerializeField]
    private Image IsDoneButton;

    [SerializeField]
    private TextMeshProUGUI Level;

    [SerializeField]
    private TextMeshProUGUI NextLevel;

    [SerializeField]
    private TextMeshProUGUI Rune;

    [SerializeField]
    private TextMeshProUGUI UsedRune;

    [SerializeField]
    private TextMeshProUGUI Vta;

    [SerializeField]
    private TextMeshProUGUI NextVta;

    [SerializeField]
    private TextMeshProUGUI Mel;

    [SerializeField]
    private TextMeshProUGUI NextMel;

    [SerializeField]
    private TextMeshProUGUI Agi;

    [SerializeField]
    private TextMeshProUGUI NextAgi;

    [SerializeField]
    private TextMeshProUGUI Str;

    [SerializeField]
    private TextMeshProUGUI NextStr;

    [SerializeField]
    private TextMeshProUGUI Int;

    [SerializeField]
    private TextMeshProUGUI NextInt;

    [SerializeField]
    private TextMeshProUGUI Health;

    [SerializeField]
    private TextMeshProUGUI NextHealth;

    [SerializeField]
    private TextMeshProUGUI ForcePoint;

    [SerializeField]
    private TextMeshProUGUI NextForcePoint;

    [SerializeField]
    private TextMeshProUGUI Stamina;

    [SerializeField]
    private TextMeshProUGUI NextStamina;

    [SerializeField]
    private TextMeshProUGUI MeleeATK;

    [SerializeField]
    private TextMeshProUGUI NextMeleeATK;

    [SerializeField]
    private TextMeshProUGUI MagicATK;

    [SerializeField]
    private TextMeshProUGUI NextMagicATK;

    private WeaponDataTable currentWeapon;

    [SerializeField]
    private Fade FadeLevel;

    [SerializeField]
    private TextMeshProUGUI NeedRuneText;

    public void UIStart()
    {
        FadeLevel.UIStart();
        this.gameObject.SetActive(false);
    }

    public void PreviewStatus()
    {
        #region SetText

        Vta.text = (CharacterManager.Instance.Data.Vta).ToString();
        Mel.text = (CharacterManager.Instance.Data.Mel).ToString();
        Str.text = (CharacterManager.Instance.Data.Str).ToString();
        Int.text = (CharacterManager.Instance.Data.Int).ToString();
        Agi.text = (CharacterManager.Instance.Data.Agi).ToString();
        Level.text = (CharacterManager.Instance.Data.Level).ToString();
        Rune.text = (CharacterManager.Instance.Data.Rune).ToString();

        NextVta.text = (CharacterManager.Instance.Data.Vta + IsStatusUp[0]).ToString();
        NextMel.text = (CharacterManager.Instance.Data.Mel + IsStatusUp[1]).ToString();
        NextStr.text = (CharacterManager.Instance.Data.Str + IsStatusUp[3]).ToString();
        NextInt.text = (CharacterManager.Instance.Data.Int + IsStatusUp[4]).ToString();
        NextAgi.text = (CharacterManager.Instance.Data.Agi + IsStatusUp[2]).ToString();
        NextLevel.text = (CharacterManager.Instance.Data.Level + NextLevelUpStack).ToString();
        UsedRune.text = (CharacterManager.Instance.Data.Rune - NeedRune).ToString();


        NeedRuneText.text = NeedRune.ToString();
        #endregion

        #region SetColor

        if (CharacterManager.Instance.Data.Vta < CharacterManager.Instance.Data.Vta + IsStatusUp[0])
        {
            NextVta.color = Color.blue;
        }
        else if (CharacterManager.Instance.Data.Vta > CharacterManager.Instance.Data.Vta + IsStatusUp[0])
        {
            NextVta.color = Color.red;
        }
        else if (CharacterManager.Instance.Data.Vta == CharacterManager.Instance.Data.Vta + IsStatusUp[0])
        {
            NextVta.color = DefaultColor;
        }
        else
        {
            NextVta.color = DefaultColor;
        }

        if (CharacterManager.Instance.Data.Mel < CharacterManager.Instance.Data.Mel + IsStatusUp[1])
        {
            NextMel.color = Color.blue;
        }
        else if (CharacterManager.Instance.Data.Mel > CharacterManager.Instance.Data.Mel + IsStatusUp[1])
        {
            NextMel.color = Color.red;
        }
        else if (CharacterManager.Instance.Data.Mel == CharacterManager.Instance.Data.Mel + IsStatusUp[1])
        {
            NextMel.color = DefaultColor;
        }
        else
        {
            NextMel.color = DefaultColor;
        }

        if (CharacterManager.Instance.Data.Str < CharacterManager.Instance.Data.Str + IsStatusUp[3])
        {
            NextStr.color = Color.blue;
        }
        else if (CharacterManager.Instance.Data.Str > CharacterManager.Instance.Data.Str + IsStatusUp[3])
        {
            NextStr.color = Color.red;
        }
        else if (CharacterManager.Instance.Data.Str == CharacterManager.Instance.Data.Str + IsStatusUp[3])
        {
            NextStr.color = DefaultColor;
        }
        else
        {
            NextStr.color = DefaultColor;
        }

        if (CharacterManager.Instance.Data.Int < CharacterManager.Instance.Data.Int + IsStatusUp[4])
        {
            NextInt.color = Color.blue;
        }
        else if (CharacterManager.Instance.Data.Int > CharacterManager.Instance.Data.Int + IsStatusUp[4])
        {
            NextInt.color = Color.red;
        }
        else if (CharacterManager.Instance.Data.Int == CharacterManager.Instance.Data.Int + IsStatusUp[4])
        {
            NextInt.color = DefaultColor;
        }
        else
        {
            NextInt.color = DefaultColor;
        }

        if (CharacterManager.Instance.Data.Agi < CharacterManager.Instance.Data.Agi + IsStatusUp[2])
        {
            NextAgi.color = Color.blue;
        }
        else if (CharacterManager.Instance.Data.Agi > CharacterManager.Instance.Data.Agi + IsStatusUp[2])
        {
            NextAgi.color = Color.red;
        }
        else if (CharacterManager.Instance.Data.Agi == CharacterManager.Instance.Data.Agi + IsStatusUp[2])
        {
            NextAgi.color = DefaultColor;
        }
        else
        {
            NextAgi.color = DefaultColor;
        }

        if (CharacterManager.Instance.Data.Level < CharacterManager.Instance.Data.Level + NextLevelUpStack)
        {
            NextLevel.color = Color.blue;
        }
        else if (CharacterManager.Instance.Data.Level > CharacterManager.Instance.Data.Level + NextLevelUpStack)
        {
            NextLevel.color = Color.red;
        }
        else if (CharacterManager.Instance.Data.Level == CharacterManager.Instance.Data.Level + NextLevelUpStack)
        {
            NextLevel.color = DefaultColor;
        }
        else
        {
            NextLevel.color = DefaultColor;
        }

        if (CharacterManager.Instance.Data.Rune > CharacterManager.Instance.Data.Rune - NeedRune)
        {
            UsedRune.color = Color.red;
        }
        else if (CharacterManager.Instance.Data.Rune < CharacterManager.Instance.Data.Rune - NeedRune)
        {
            UsedRune.color = Color.blue;
        }
        else if (CharacterManager.Instance.Data.Rune == CharacterManager.Instance.Data.Rune - NeedRune)
        {
            UsedRune.color = DefaultColor;
        }
        else
        {
            UsedRune.color = DefaultColor;
        }

        #endregion
    }

    public void PreviewATK()
    {
        float NextMeleeATKValue = Mathf.Floor(_Preview_MeleeATK() * 10f) / 10f;
        float NextMagicATKValue = Mathf.Floor(_Preview_MagicATK() * 10f) / 10f;

        MeleeATK.text = (Mathf.Floor(CharacterManager.Instance.Return_MeleeATK() * 10f) / 10f).ToString();
        MagicATK.text = (Mathf.Floor(CharacterManager.Instance.Return_MagicATK() * 10f) / 10f).ToString();
        NextMeleeATK.text = NextMeleeATKValue.ToString();
        NextMagicATK.text = NextMagicATKValue.ToString();

        #region SetColor

        if (CharacterManager.Instance.Return_MeleeATK() < NextMeleeATKValue)
        {
            NextMeleeATK.color = Color.blue;
        }
        else if (CharacterManager.Instance.Return_MeleeATK() > NextMeleeATKValue)
        {
            NextMeleeATK.color = Color.red;
        }
        else if (CharacterManager.Instance.Return_MeleeATK() == NextMeleeATKValue)
        {
            NextMeleeATK.color = DefaultColor;
        }
        else
        {
            NextMeleeATK.color = DefaultColor;
        }

        if (CharacterManager.Instance.Return_MagicATK() < NextMagicATKValue)
        {
            NextMagicATK.color = Color.blue;
        }
        else if (CharacterManager.Instance.Return_MagicATK() > NextMagicATKValue)
        {
            NextMagicATK.color = Color.red;
        }
        else if (CharacterManager.Instance.Return_MagicATK() == NextMagicATKValue)
        {
            NextMagicATK.color = DefaultColor;
        }
        else
        {
            NextMagicATK.color = DefaultColor;
        }

        #endregion

    }

    private float _Preview_MeleeATK()
    {
        float ATK = currentWeapon.MeleeATK + (currentWeapon.MeleeATK * 0.01f * currentWeapon.BonusSTRValue) +
            (currentWeapon.MeleeATK * 0.01f * ((CharacterManager.Instance.Data.Str + IsStatusUp[3]) * 0.1f));

        return ATK;
    }

    private float _Preview_MagicATK()
    {
        float ATK = currentWeapon.MagicATK + (currentWeapon.MagicATK * 0.01f * currentWeapon.BonusINTValue) +
            (currentWeapon.MagicATK * 0.01f * ((CharacterManager.Instance.Data.Int + IsStatusUp[4]) * 0.1f));

        return ATK;
    }

    private void PreviewLogical()
    {
        #region SetText

        Health.text = (CharacterManager.Instance.Data.MaxHealth).ToString();
        ForcePoint.text = (CharacterManager.Instance.Data.MaxForcePoint).ToString();
        Stamina.text = (CharacterManager.Instance.Data.MaxStamina).ToString();

        NextHealth.text = (CharacterManager.Instance.Data.MaxHealth + (IsStatusUp[0] * 10)).ToString();
        NextForcePoint.text = (CharacterManager.Instance.Data.MaxForcePoint + (IsStatusUp[1] * 3)).ToString();
        NextStamina.text = (CharacterManager.Instance.Data.MaxStamina + (IsStatusUp[2] * 10)).ToString();

        #endregion

        #region SetColor

        if (CharacterManager.Instance.Data.MaxHealth < CharacterManager.Instance.Data.MaxHealth + (IsStatusUp[0] * 10))
        {
            NextHealth.color = Color.blue;
        }
        else if (CharacterManager.Instance.Data.MaxHealth > CharacterManager.Instance.Data.MaxHealth + (IsStatusUp[0] * 10))
        {
            NextHealth.color = Color.red;
        }
        else if (CharacterManager.Instance.Data.MaxHealth == CharacterManager.Instance.Data.MaxHealth + (IsStatusUp[0] * 10))
        {
            NextHealth.color = DefaultColor;
        }
        else
        {
            NextHealth.color = DefaultColor;
        }

        if (CharacterManager.Instance.Data.MaxForcePoint < CharacterManager.Instance.Data.MaxForcePoint + (IsStatusUp[1] * 3))
        {
            NextForcePoint.color = Color.blue;
        }
        else if (CharacterManager.Instance.Data.MaxForcePoint > CharacterManager.Instance.Data.MaxForcePoint + (IsStatusUp[1] * 3))
        {
            NextForcePoint.color = Color.red;
        }
        else if (CharacterManager.Instance.Data.MaxForcePoint == CharacterManager.Instance.Data.MaxForcePoint + (IsStatusUp[1] * 3))
        {
            NextForcePoint.color = DefaultColor;
        }
        else
        {
            NextForcePoint.color = DefaultColor;
        }

        if (CharacterManager.Instance.Data.MaxStamina < CharacterManager.Instance.Data.MaxStamina + (IsStatusUp[2] * 10))
        {
            NextStamina.color = Color.blue;
        }
        else if (CharacterManager.Instance.Data.MaxStamina > CharacterManager.Instance.Data.MaxStamina + (IsStatusUp[2] * 10))
        {
            NextStamina.color = Color.red;
        }
        else if (CharacterManager.Instance.Data.MaxStamina == CharacterManager.Instance.Data.MaxStamina + (IsStatusUp[2] * 10))
        {
            NextStamina.color = DefaultColor;
        }
        else
        {
            NextStamina.color = DefaultColor;
        }

        #endregion

    }

    public void OpenLevelLayer()
    {
        FadeLevel.FadeIn(0.5f);
        currentWeapon = ItemManager.Instance.WeaponData[CharacterManager.Instance.Equip.Inventory[0][CharacterManager.Instance.Equip.EquipIndex[0]]];

        PreviewStatus();

        PreviewATK();

        PreviewLogical();

        UIManager.Instance.IsActiveLevelUpLayer = true;
    }

    public void CloseLevelLayer()
    {
        for (int i = 0; i < IsStatusUp.Length; i++)
        {
            IsStatusUp[i] = 0;
        }
        FadeLevel.FadeOut(0.5f);
        Invoke(nameof(Exit), 0.5f);
    }

    private void Exit()
    {
        UIManager.Instance.IsActiveLevelUpLayer = false;
        this.gameObject.SetActive(false);
    }


    public void StstusUpStack(int Index)
    {
        if (CharacterManager.Instance.Data.Rune < NeedRune + 100)
        {
            return;
        }
        else
        {
            GameSoundManager.Instance.PlayClick();
            IsStatusUp[Index]++;
            PreviewStatus();

            PreviewATK();

            PreviewLogical();
        }
    }

    public void StatusDownStack(int Index)
    {
        if (IsStatusUp[Index] == 0)
        {
            return;
        }
        else
        {
            GameSoundManager.Instance.PlayClick();
            IsStatusUp[Index]--;
            PreviewStatus();

            PreviewATK();

            PreviewLogical();
        }
    }

    public void StatusUpCompleate()
    {
        if(stack == 0)
        {
            return;
        }
        else
        {
            GameSoundManager.Instance.PlayLevelupSound();
            CharacterManager.Instance.SetLevelUp(IsStatusUp, NeedRune, stack);
            for (int i = 0; i < IsStatusUp.Length; i++)
            {
                IsStatusUp[i] = 0;
            }

            PreviewStatus();

            PreviewATK();

            PreviewLogical();

        }

    }


}
