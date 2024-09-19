using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InGameFadeLayer : MonoBehaviour
{
    [SerializeField]
    private Image FadeImage;

    [SerializeField]
    private Image LoadingImage;

    public void UIStart()
    {
        FadeImage.color = new Color(0, 0, 0, 1);
        FadeImage.gameObject.SetActive(false);
    }

    public void FadeIn(int SceneIndex)
    {
        StartCoroutine(_FadeIn(SceneIndex));
    }

    public void FadeOut()
    {
        StartCoroutine(_FadeOut());
    }

    public void IntoLoad()
    {
        FadeImage.gameObject.SetActive(false);
    }

    private IEnumerator _FadeIn(int SceneIndex)
    {
        if (FadeImage.gameObject.activeInHierarchy == false)
        {
            FadeImage.gameObject.SetActive(true);
        }
        float progeress = 0f;
        while (true)
        {
            FadeImage.color = new Color(0, 0, 0, progeress);

            progeress += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();


            if(progeress > 1f)
            {
                SceneLoadManager.Instance.LoadSceneToLoading(SceneIndex);

                yield break;
            }
        }
    }

    private IEnumerator _FadeOut()
    {
        if(FadeImage.gameObject.activeInHierarchy == false)
        {
            FadeImage.gameObject.SetActive(true);
        }
        float progeress = 1f;

        while (true)
        {
            FadeImage.color = new Color(0, 0, 0, progeress);
            progeress -= Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();

            if(progeress < 0f)
            {
                FadeImage.gameObject.SetActive(false);
                GameManager.Instance.IsPaused = false;
                yield break;
            }
        }
    }
}
