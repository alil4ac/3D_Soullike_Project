using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    //static string nextScene;

    //public static void LoadScene(string sceneName)
    //{
    //    nextScene = sceneName;
    //    //SceneManager.LoadScene("LoadingScene");
    //}

    //void Start()
    //{
    //    //StartCoroutine(LoadSceneProcess());
    //}

    //IEnumerator LoadSceneProcess()
    //{
    //    if (string.IsNullOrEmpty(nextScene))
    //    {
    //        yield break;
    //    }

    //    AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
    //    op.allowSceneActivation = false;

    //    while (!op.isDone)
    //    {
    //        yield return null;

    //        if (op.progress >= 0.9f)
    //        {
    //            op.allowSceneActivation = true;
    //        }
    //    }
    //}

    [SerializeField]
    private Image FadeImage;


    private void Awake()
    {
        FadeImage.gameObject.SetActive(true);

        FadeImage.color = new Color(0, 0, 0, 1f);
    }

    private void Start()
    {
        StartCoroutine(_FadeOut());
    }

    private IEnumerator _FadeOut()
    {
        float progeress = 1f;
        while (true)
        {
            if(progeress > 0f)
            {
                FadeImage.color = new Color(0, 0, 0, progeress);

                progeress -= Time.fixedDeltaTime;

                yield return new WaitForFixedUpdate();
            }

            if (progeress <= 0f)
            {
                StartCoroutine(FadeIn());
                yield break;
            }
        }
    }

    private IEnumerator FadeIn()
    {
        float progeress = 0f;

        float delay = 3f;

        bool IsDone = false;

        while (true)
        {
            if(delay > 0f)
            {
                delay -= 0.01f;
            }
            else if(delay <= 0f && IsDone == false)
            {
                IsDone = true;
            }

            if(IsDone == true)
            {
                FadeImage.color = new Color(0, 0, 0, progeress);

                progeress += 0.01f;
            }

            if(progeress > 1f)
            {
                FadeImage.color = new Color(0, 0, 0, 1f);
                SceneLoadManager.Instance.StartLoad();
                yield break;
            }

            yield return new WaitForSeconds(0.01f);
        }
    }

}


