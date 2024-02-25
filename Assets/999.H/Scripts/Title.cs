using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;


public class Title : MonoBehaviour
{
    #region Component Type

    [SerializeField]
    private Image Fade;

    [SerializeField]
    private Image TitleImage;

    [SerializeField]
    private GameObject TitleMenu;

    private TextMeshProUGUI[] MenuText;

    [SerializeField]
    private GameObject Menuarrow;

    [SerializeField]
    private Image arrow;

    [SerializeField]
    private GameObject OptionPanel;

    private TitleOption Option;

    #endregion

    #region Variables
    private float fadeSpeed = 0.01f;

    private int Index;

    private float ArrowX = 210f;

    private float colorLerpValue = 0f;

    bool GS = true;

    #endregion

    #region Methods

    private void Init()
    {
        MenuText = TitleMenu.GetComponentsInChildren<TextMeshProUGUI>();
        arrow = Menuarrow.GetComponentInChildren<Image>();
        arrow.gameObject.SetActive(false);
        Option = this.GetComponent<TitleOption>();
    }

    private IEnumerator StartFade()
    {
        yield return new WaitForSeconds(2f);
        Color FadeOut = Fade.color;
        float opacity = 1f;
        while (opacity >= 0)
        {
            opacity -= 0.01f;
            FadeOut.a = opacity;
            Fade.color = FadeOut;
            yield return new WaitForSeconds(0.02f);
        }

        if (Fade.color.a <= 0)
        {
            Fade.gameObject.SetActive(false);
            GameSoundManager.Instance.StartTitle();
            yield return StartCoroutine(TextIn());
        }
    }

    private IEnumerator TextIn()
    {
        float opacity = 0;

        StartCoroutine(FadeArrow());

        foreach (TextMeshProUGUI text in MenuText)
        {
            text.gameObject.SetActive(true);
            text.color = Color.white; // 각 텍스트를 하얀색으로 설정
        }

        while (opacity <= 1)
        {
            opacity += 0.01f;

            foreach (TextMeshProUGUI color in MenuText)
            {
                color.color = new Color(color.color.r, color.color.g, color.color.b, opacity);
            }

            // arrow 알파값 조정
            float arrowAlpha = Mathf.Lerp(0f, 0.5f, opacity);
            arrow.color = new Color(arrow.color.r, arrow.color.g, arrow.color.b, arrowAlpha);

            yield return new WaitForSeconds(0.01f);
        }

        if (opacity >= 1f)
        {
            arrow.gameObject.SetActive(true);
            arrow.color = Color.white;
            arrow.rectTransform.anchoredPosition = new Vector2(MenuText[Index].rectTransform.anchoredPosition.x - 7.67f, MenuText[Index].rectTransform.anchoredPosition.y - 3.88f);
            GS = false;
            yield break;
        }
    }


    private void SetSelectMenu()
    {

        arrow.rectTransform.anchoredPosition = new Vector2(MenuText[Index].rectTransform.anchoredPosition.x - 7.67f, MenuText[Index].rectTransform.anchoredPosition.y - 3.88f);


    }
    private IEnumerator FadeArrow()
    {
        float startAlpha = 0.1f;
        float targetAlpha = 0.35f;
        float fadeDuration = 1f;

        while (true)
        {
            float elapsedTime = 0f;

            // 알파값이 0에서 0.5로 천천히 오르는 부분
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float normalizedTime = elapsedTime / fadeDuration;
                float alpha = Mathf.Lerp(startAlpha, targetAlpha, normalizedTime);
                arrow.color = new Color(arrow.color.r, arrow.color.g, arrow.color.b, alpha);
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);

            elapsedTime = 0f;

            // 알파값이 0.5에서 0로 천천히 줄어드는 부분
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float normalizedTime = elapsedTime / fadeDuration;
                float alpha = Mathf.Lerp(targetAlpha, startAlpha, normalizedTime);
                arrow.color = new Color(arrow.color.r, arrow.color.g, arrow.color.b, alpha);
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }









    public void SetOpen()
    {
        switch (Index)
        {
            case 0:
                GameSoundManager.Instance.TitleUiSound();
                StartCoroutine(StartGame());
                break;
            case 1:

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                StopAllCoroutines();
                GameSoundManager.Instance.TitleUiSound();
                foreach (TextMeshProUGUI T in MenuText)
                {
                    T.color = new Color(0, 0, 0, 0);
                }
                arrow.color = new Color(0, 0, 0, 0);
                GS = true;

                Option.SetActiveOption();
                break;
            case 2:
                GameSoundManager.Instance.TitleUiSound();
                Application.Quit();
                break;

        }
    }

    private IEnumerator StartGame()
    {
        Fade.gameObject.SetActive(true);
        float progeress = 0f;
        while (true)
        {
            Fade.color = new Color(0, 0, 0, progeress);

            progeress += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();

            if (progeress > 1f)
            {
                SceneLoadManager.Instance.LoadSceneToLoading(1);
                yield break;
            }
        }
    }

    public void ReturnMain()
    {
        foreach (TextMeshProUGUI T in MenuText)
        {
            T.color = Color.white; // 원래 색깔인 하얀색으로 설정
        }
        arrow.color = Color.white; // 원래 색깔인 하얀색으로 설정
        Index = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SetSelectMenu();
        StartCoroutine(FadeArrow());
        GS = false;

    }


    #endregion

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        StartCoroutine(StartFade());
    }

    private void Update()
    {
        if (GS == false)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Index--;
                if (Index < 0) { Index = 2; }
                SetSelectMenu();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Index++;
                if (Index > 2) { Index = 0; }
                SetSelectMenu();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                SetOpen();

            }
        }
    }
}
