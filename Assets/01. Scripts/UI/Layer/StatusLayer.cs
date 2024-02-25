using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StatusLayer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Level;

    [SerializeField]
    private TextMeshProUGUI Rune;

    [SerializeField]
    private TextMeshProUGUI Vta;

    [SerializeField]
    private TextMeshProUGUI Mel;

    [SerializeField]
    private TextMeshProUGUI Agi;

    [SerializeField]
    private TextMeshProUGUI Str;

    [SerializeField]
    private TextMeshProUGUI Int;

    [SerializeField]
    private TextMeshProUGUI MaxHealth;

    [SerializeField]
    private TextMeshProUGUI Health;

    [SerializeField]
    private TextMeshProUGUI MaxForcePoint;

    [SerializeField]
    private TextMeshProUGUI ForcePoint;

    [SerializeField]
    private TextMeshProUGUI Stamina;

    [SerializeField]
    private TextMeshProUGUI MeleeATK;

    [SerializeField]
    private TextMeshProUGUI MagicATK;

    [SerializeField]
    private TextMeshProUGUI Armor;

    [SerializeField]
    private TextMeshProUGUI DeF;

    [SerializeField]
    private Fade StatusFade;


    public void UIStart()
    {
        StatusFade.UIStart();
        this.gameObject.SetActive(false);
    }

    public void OpenStstus()
    {
        SetStatus();
        StatusFade.FadeIn(0.5f);
        UIManager.Instance.IsActiveStatusLayer = true;
    }

    public void CloseStatus()
    {
        StatusFade.FadeOut(0.5f);

        Invoke("Exit", 0.5f);
    }

    private void Exit()
    {
        UIManager.Instance.IsActiveStatusLayer = false;
        this.gameObject.SetActive(false);
    }

    private void SetStatus()
    {
        Level.text = CharacterManager.Instance.Data.Level.ToString();

        Rune.text = CharacterManager.Instance.Data.Rune.ToString();

        Vta.text = CharacterManager.Instance.Data.Vta.ToString();

        Mel.text = CharacterManager.Instance.Data.Mel.ToString();

        Agi.text = CharacterManager.Instance.Data.Agi.ToString();

        Str.text = CharacterManager.Instance.Data.Str.ToString();

        Int.text = CharacterManager.Instance.Data.Int.ToString();

        MaxHealth.text = CharacterManager.Instance.Data.MaxHealth.ToString();

        Health.text = CharacterManager.Instance.Data.Health.ToString();

        MaxForcePoint.text = CharacterManager.Instance.Data.MaxForcePoint.ToString();

        ForcePoint.text = CharacterManager.Instance.Data.ForcePoint.ToString();

        Stamina.text = CharacterManager.Instance.Data.MaxStamina.ToString();

        MeleeATK.text = (Mathf.Floor(CharacterManager.Instance.Return_MeleeATK() * 10f) / 10f).ToString();

        MagicATK.text = (Mathf.Floor(CharacterManager.Instance.Return_MagicATK() * 10f) / 10f).ToString();

        Armor.text = CharacterManager.Instance.Data.Armor.ToString();

        DeF.text = Mathf.Floor((CharacterManager.Instance.HitReduce() * 10f) / 10f).ToString();
    }

}
