using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using DG.Tweening;
public class Test : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    Slider BossHP;
    [SerializeField]
    Slider BlackHP;
    [SerializeField]
    Image BackGround;
    [SerializeField]
    Image Fill;
    [SerializeField]
    Image Fill2;
    [SerializeField]
    TextMeshProUGUI text;

    public Transform cameraTransform;
    public float shakeDuration = 0.1f;
    public float shakeMagnitude = 0.1f;

    private Vector3 originalPosition;
    private float currentShakeDuration = 0f;

    bool isSetval = false;
    private void Awake()
    {
        if (cameraTransform == null)
        {
            cameraTransform = GetComponent<Transform>();
        }
    }

    private void Start()
    {
        originalPosition = cameraTransform.localPosition;
    }
    public void StartFadeIn()
    {
        image.gameObject.SetActive(true);
        StartCoroutine(FadeIn());
    }
    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }
    //페이드 인
    private IEnumerator FadeIn()
    {
        Color color = image.color;
        float opacity = 0f;
        while (opacity <= 1f)
        {
            opacity += 0.1f;
            color.a = opacity;
            image.color = color;

            yield return new WaitForSeconds(0.1f);
        }
    }
    //페이드 아웃
    private IEnumerator FadeOut()
    {
        Color color = image.color;
        float opacity = 1f;
        while(opacity >= 0)
        {
            opacity -= 0.1f;
            color.a = opacity;
            image.color = color;

            yield return new WaitForSeconds(0.1f);
        }
        image.gameObject.SetActive(false);
    }
    //보스 체력바
    public void BossHpBar(string name)
    {
        BlackHP.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
        text.text = name;
        StartCoroutine(Boss());
        BossHP.value = 0;
    }
    private IEnumerator Boss()
    {
        Color Tcolor = text.color;
        Color Bcolor = BackGround.color;
        Color Fcolor = Fill.color;
        Color F2color = Fill2.color;
        float opacity = 0f;
        int MaxHp = 100;
        while(opacity <=1)
        {
            opacity += 0.1f;
            Bcolor.a = opacity;
            Tcolor.a = opacity;
            BackGround.color = Bcolor;
            text.color = Tcolor;
            if (Bcolor.a >= 1f)
            {
                Fcolor.a = opacity;
                Fill.color = Fcolor;
            }
            yield return new WaitForSeconds(0.1f);

        }

        if (opacity > 1f)
        {
            int NowHp = 1;
            int value = MaxHp/50;
            while(NowHp <= MaxHp)
            {
                NowHp += value;
                BossHP.value = NowHp;
                yield return new WaitForFixedUpdate();
            }
            BlackHP.maxValue = MaxHp;
            BlackHP.value = MaxHp;
            F2color.a = 1f;
            Fill2.color = F2color;
        }


    }
    //데미지
    public void Damage(int Damage)
    {
        BossHP.value -= Damage;
        //StartCoroutine(Shake());
        
        currentShakeDuration = shakeDuration;
        if (BossHP.value <= 0)
        {
            StartCoroutine(HpBaDelete());
        }
        else
        {
            SetShake();
        }
        
        if(isSetval == false)
        {
            StartCoroutine(SetValue());
        }
    }
    //카메라 쉐이크
    public IEnumerator Shake()
    {
        while (currentShakeDuration > 0)
        {
            cameraTransform.localPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;
            currentShakeDuration -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        cameraTransform.localPosition = originalPosition;
    }

    public void SetShake(float duration = 0.25f, float pow = 0.3f, int value = 20, float randomness = 90f, Ease ease = Ease.InOutElastic)
    {
        cameraTransform.DOShakePosition(duration, pow, value, randomness,false, true).SetEase(ease);
    }

    private IEnumerator HpBaDelete()
    {
        Color Tcolor = text.color;
        Color Bcolor = BackGround.color;
        Color Fcolor = Fill.color;
        Color F2color = Fill2.color;
        float opacity = 1f;
        while (opacity <= 1f)
        {
            opacity -= 0.1f;
            Tcolor.a = opacity;
            Bcolor.a = opacity;
            Fcolor.a = opacity;
            F2color.a = opacity;
            text.color = Tcolor;
            BackGround.color = Bcolor;
            Fill.color = Fcolor;
            Fill2.color = F2color;
            if (opacity <= 0)
            {
                text.gameObject.SetActive(false);
                BlackHP.gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    private IEnumerator SetValue()
    {
        float timer = 0.5f;
        isSetval = true;
        while  (BlackHP.value != BossHP.value)
        {
            if (timer > 0f)
            {
                timer -= 0.1f;
            }
            else if(timer <= 0f)
            {
                BlackHP.value -= 60 * Time.fixedDeltaTime;

                if (BlackHP.value <= BossHP.value)
                {
                    BlackHP.value = BossHP.value;
                    isSetval = false;
                    yield break;
                }
            }
            yield return new WaitForSeconds(0.1f);

        }
    }
}

