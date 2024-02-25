using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingScene : MonoBehaviour
{
    #region Component Type
    [SerializeField]
    private GameObject Fadelist;
    private Image Fade;

    private Slider LoadingBar;
    #endregion
    #region Methods
    private void FadeOut()
    {
        Fade.DOFade(0f, 2f);
        Fade.gameObject.SetActive(false);
    }
    private IEnumerator Loading()
    {
        float Value = 0;
        LoadingBar.value = 0;
        while(Value >= 1f)
        {
            LoadingBar.value = SceneLoadManager.Instance.progess;
            Debug.Log(LoadingBar.value);
            yield return new WaitForEndOfFrame();
        }
    }

    #endregion
    private void Start()
    {
        Fade = Fadelist.GetComponentInChildren<Image>();
        LoadingBar = this.GetComponentInChildren<Slider>();
        FadeOut();
    }
    private void Update()
    {
        
    }

    public void Scenec()
    {
        SceneLoadManager.Instance.LoadSceneToLoading(1);
        StartCoroutine(Loading());
    }
}
