using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InterfaceLayer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Rune;

    private Color DefaultColor = new Color(0.8588235f, 0.8666667f, 0.9137255f, 1f);

    private Color EffectColor = new Color(0.8207547f, 0.7950528f, 0.4452207f, 1f);

    public void UIStart()
    {
        Rune.text = CharacterManager.Instance.Data.Rune.ToString();
    }

    public void SetRune()
    {
        Rune.text = CharacterManager.Instance.Data.Rune.ToString();
        StartCoroutine(SetEffect());
    }

    

    private IEnumerator SetEffect()
    {
        float time = 1f;
        while (true)
        {
            Rune.color = EffectColor;

            time -= Time.deltaTime;

            if(time <= 0f)
            {
                Rune.color = DefaultColor;

                yield break;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
