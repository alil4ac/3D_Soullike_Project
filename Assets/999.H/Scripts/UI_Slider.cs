using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class UI_Slider : MonoBehaviour
{
    [SerializeField]
    private Slider BossBlackHP;
    [SerializeField]
    private Slider BossHP;
    [SerializeField]
    private Slider PlayerBlackHP;
    [SerializeField]
    private Slider PlayerHP;

    [SerializeField]
    private Image BossHPBackground;
    [SerializeField]
    private Image BossNowHP;
    [SerializeField]
    private Image BossHitHP;
    [SerializeField]
    private Image PlayerHPBackground;
    [SerializeField]
    private Image PlayerNowHP;
    [SerializeField]
    private Image PlayerHitHP;

    //플레이어 체력바

    //보스 체력바
    public void SetBossHP(int MaxHP)
    {
        BossBlackHP.gameObject.SetActive(true);
        StartCoroutine(Boss());
        BossHP.value = 0f;
        BossHP.maxValue = MaxHP;
        BossBlackHP.maxValue = MaxHP;
    }
    private IEnumerator Boss()
    {
        Color CBackGround = BossHPBackground.color;
        Color CHitHP = BossHitHP.color;
        Color CNowHP = BossNowHP.color;

        float opacity = 0f;

        while(opacity <= 1f)
        {
            opacity += 0.1f;
            CBackGround.a = opacity;
            BossHPBackground.color = CBackGround;
            
            if(CBackGround.a >= 1f)
            {
                CNowHP.a = opacity;
                BossNowHP.color = CNowHP;
            }

            yield return new WaitForSeconds(0.1f);
        
        }

        if (opacity > 1f)
        {
            int NowHp = 1;
            int value = (int)BossHP.maxValue / 50;
            while (NowHp <= BossHP.maxValue)
            {
                NowHp += value;
                BossHP.value = NowHp;
                yield return new WaitForFixedUpdate();
            }
            BossBlackHP.maxValue = BossHP.maxValue;
            BossBlackHP.value = BossHP.maxValue;
            CHitHP.a = 1f;
            BossHitHP.color = CHitHP;
        }

    }
    //피격 시 제력이 닳는 효과
    bool BossisSetval = false;
    public void BossDamage(int damage)
    {
        BossHP.value -= damage;
        if (BossBlackHP.value <= 0)
        {
            StartCoroutine(BossHpBaDelete());
        }
        if (BossisSetval == false)
        {
            StartCoroutine(BossHitHPValue());
        }
    }

    private IEnumerator BossHpBaDelete()
    {
        Color CBackGround = BossHPBackground.color;
        Color CHitHP = BossHitHP.color;
        Color CNowHP = BossNowHP.color;
        float opacity = 1f;
        while (opacity <= 1f)
        {
            opacity -= 0.1f;
            CBackGround.a = opacity;
            CHitHP.a = opacity;
            CNowHP.a = opacity;
            BossHPBackground.color = CBackGround;
            BossHitHP.color = CHitHP;
            BossNowHP.color = CNowHP;
            if (opacity <= 0)
            {
                BossBlackHP.gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    private IEnumerator BossHitHPValue()
    {
        float timer = 0.2f;
        BossisSetval = true;
        while (BossBlackHP.value != BossHP.value)
        {
            if (timer > 0f)
            {
                timer -= 0.1f;
            }
            else if (timer <= 0f)
            {
                BossBlackHP.value -= 60 * Time.fixedDeltaTime;

                if (BossBlackHP.value <= BossHP.value)
                {
                    BossBlackHP.value = BossHP.value;
                    BossisSetval = false;
                    yield break;
                }
            }
            yield return new WaitForSeconds(0.1f);

        }
    }

    bool PlayerisSetval = false;
    public void PlayerDamage(int damage)
    {
        PlayerHP.value -= damage;
        if (PlayerHP.value <= 0)
        {
            StartCoroutine(PlayerHpBaDelete());
        }
        if (PlayerisSetval == false)
        {
            StartCoroutine(PlayerHitHPValue());
        }
    }

    private IEnumerator PlayerHpBaDelete()
    {
        Color CBackGround = BossHPBackground.color;
        Color CHitHP = BossHitHP.color;
        Color CNowHP = BossNowHP.color;
        float opacity = 1f;
        while (opacity <= 1f)
        {
            opacity -= 0.1f;
            CBackGround.a = opacity;
            CHitHP.a = opacity;
            CNowHP.a = opacity;
            BossHPBackground.color = CBackGround;
            BossHitHP.color = CHitHP;
            BossNowHP.color = CNowHP;
            if (opacity <= 0)
            {
                BossBlackHP.gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    private IEnumerator PlayerHitHPValue()
    {
        float timer = 0.2f;
        PlayerisSetval = true;
        while (BossBlackHP.value != BossHP.value)
        {
            if (timer > 0f)
            {
                timer -= 0.1f;
            }
            else if (timer <= 0f)
            {
                BossBlackHP.value -= 60 * Time.fixedDeltaTime;

                if (BossBlackHP.value <= BossHP.value)
                {
                    BossBlackHP.value = BossHP.value;
                    PlayerisSetval = false;
                    yield break;
                }
            }
            yield return new WaitForSeconds(0.1f);

        }
    }
}
