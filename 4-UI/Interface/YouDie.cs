using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class YouDie : MonoBehaviour
{
    [SerializeField]
    private Image Background;

    [SerializeField]
    private TextMeshProUGUI YouDieText;

    [SerializeField]
    private Image BossFelled;

    [SerializeField]
    private Image BossFelled2;


    #region Method

    public void UIStart()
    {
        Background.color = new Color(Background.color.r, Background.color.g, Background.color.b, 0f);

        YouDieText.color = new Color(YouDieText.color.r, YouDieText.color.g, YouDieText.color.b, 0f);

        BossFelled.color = new Color(BossFelled.color.r, BossFelled.color.g, BossFelled.color.b, 0f);

        BossFelled2.color = new Color(BossFelled2.color.r, BossFelled2.color.g, BossFelled2.color.b, 0f);

        YouDieText.gameObject.SetActive(false);

        BossFelled.gameObject.SetActive(false);

        BossFelled2.gameObject.SetActive(false);

        Background.gameObject.SetActive(false);

        this.gameObject.SetActive(false);
    }

    #region YouDie

    public void PlayerDie()
    {
        StartCoroutine(_PlayerDie());
    }

    private IEnumerator _PlayerDie()
    {
        Background.gameObject.SetActive(true);
        YouDieText.gameObject.SetActive(true);
        //플레이어 사망 사운드

        float value = 0f;
        while (true)
        {
            Background.color = new Color(Background.color.r, Background.color.g, Background.color.b, value);

            YouDieText.color = new Color(YouDieText.color.r, YouDieText.color.g, YouDieText.color.b, value);

            if(value < 1f)
            {
                value += Time.fixedDeltaTime;
            }

            if (value >= 1f)
            {
                Background.color = new Color(Background.color.r, Background.color.g, Background.color.b, 1f);

                YouDieText.color = new Color(YouDieText.color.r, YouDieText.color.g, YouDieText.color.b, 1f);
                UIManager.Instance.InGameFadeIn(SceneLoadManager.Instance.NextSceneIndex);
                Invoke("Exit", 1f);
                yield break;
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private void Exit()
    {

        YouDieText.gameObject.SetActive(false);

        Background.gameObject.SetActive(false);

        this.gameObject.SetActive(false);
    }

    #endregion

    #region BossFelled

    public void EnemyFelled()
    {
        StartCoroutine(_EnemyFelledStart());
    }

    private IEnumerator _EnemyFelledStart()
    {
        Background.gameObject.SetActive(true);

        BossFelled.gameObject.SetActive(true);

        BossFelled2.gameObject.SetActive(true);

        float value = 0f;

        while (true)
        {
            Background.color = new Color(Background.color.r, Background.color.g, Background.color.b, value);

            BossFelled.color = new Color(BossFelled.color.r, BossFelled.color.g, BossFelled.color.b, value);

            BossFelled2.color = new Color(BossFelled2.color.r, BossFelled2.color.g, BossFelled2.color.b, value);

            if(value < 1f)
            {
                value += Time.fixedDeltaTime;
            }

            if(value >= 1f)
            {
                Background.color = new Color(Background.color.r, Background.color.g, Background.color.b, 1f);

                BossFelled.color = new Color(BossFelled.color.r, BossFelled.color.g, BossFelled.color.b, 1f);

                BossFelled2.color = new Color(BossFelled2.color.r, BossFelled2.color.g, BossFelled2.color.b, 1f);
                StartCoroutine(_EnemyFelledEnd());
                yield break;
            }
        }
    }

    private IEnumerator _EnemyFelledEnd()
    {
        float delay = 2f;

        float value = 1f;

        bool isDone = false;

        while (true)
        {
            if(delay > 0f)
            {
                delay -= Time.fixedDeltaTime;
            }

            if (delay <= 0f && isDone == false)
            {
                isDone = true;
                UIManager.Instance.EndBossBattle();
            }

            if(delay <= 0f && value > 0f)
            {
                Background.color = new Color(Background.color.r, Background.color.g, Background.color.b, value);

                BossFelled.color = new Color(BossFelled.color.r, BossFelled.color.g, BossFelled.color.b, value);

                BossFelled2.color = new Color(BossFelled2.color.r, BossFelled2.color.g, BossFelled2.color.b, value);

                value -= Time.fixedDeltaTime;
            }

            if(value <= 0f)
            {
                Background.color = new Color(Background.color.r, Background.color.g, Background.color.b, 0f);

                BossFelled.color = new Color(BossFelled.color.r, BossFelled.color.g, BossFelled.color.b, 0f);

                BossFelled2.color = new Color(BossFelled2.color.r, BossFelled2.color.g, BossFelled2.color.b, 0f);


                BossFelled.gameObject.SetActive(false);

                BossFelled2.gameObject.SetActive(false);

                Background.gameObject.SetActive(false);

                yield break;
            }

            yield return new WaitForFixedUpdate();
        }
    }


    #endregion

    #endregion
}
