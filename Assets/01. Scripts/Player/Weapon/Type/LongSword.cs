using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongSword : IWeapon<PlayerWeapon>
{
    Vector3 tr = new Vector3(0.008f, 0.035f, 0.054f);
    Vector3 rot = new Vector3(-171.349f, -87.72601f, 16.90099f);
    public void EndAttack(PlayerWeapon Weapon)
    {
        Weapon.WeaponCol.enabled = false;
        Weapon.AruaPrefab.IsActive = false;
        Weapon.WeaponTrail.enabled = false;
    }

    public void StartAttack(PlayerWeapon Weapon)
    {
        Weapon.WeaponCol.enabled = !Weapon.WeaponCol.enabled;
    }
    public void ChangeExit(PlayerWeapon Weapon)
    {
        Weapon.CurrentWeapon.SetActive(false);
        Weapon.CurrentWeapon = null;
        Weapon.AruaPrefab = null;
        Weapon.WeaponCol = null;
        Weapon.WeaponTrail = null;
        Weapon.SkilPrefab = null;
    }


    public void WeaponSetting(PlayerWeapon Weapon)
    {
        foreach (GameObject wp in Weapon.WeaponList)
        {
            if (wp.name == ItemColumn.EWeapon.LongSword.ToString())
            {
                wp.gameObject.SetActive(true);
                Weapon.CurrentWeapon = wp;
                wp.transform.localPosition = tr;
                wp.transform.localRotation = Quaternion.Euler(rot);
                Weapon.WeaponCol = wp.GetComponentInChildren<Collider>();
                Weapon.WeaponTrail = wp.GetComponentInChildren<XftWeapon.XWeaponTrail>();
                Weapon.AruaPrefab = wp.GetComponentInChildren<PSMeshRendererUpdater>();
                Weapon.AruaPrefab.IsActive = false;
                //Weapon.SkilPrefab = Weapon.EffectHolder.SerachPlayerEffects(wp.name);
                //Weapon.SkilPrefab.gameObject.SetActive(false);
                //Weapon.AruaPrefab.gameObject.SetActive(false);
                Weapon.WeaponTrail.enabled = false;
                Weapon.WeaponCol.enabled = false;
            }
        }
    }

    public void WeaponSkill(PlayerWeapon Weapon)
    {
        Weapon.SkilPrefab.SetActive(true);
        Weapon.SkilPrefab.GetComponent<EffectSet>().SetStartEffect(Weapon.transform);
    }

    public void WeaponArua(PlayerWeapon Weapon)
    {
        Weapon.AruaPrefab.IsActive = !Weapon.AruaPrefab.IsActive;
    }

    public void SetTrail(PlayerWeapon Weapon)
    {
        if (!Weapon.WeaponTrail.enabled)
        {
            Weapon.WeaponTrail.enabled = true;
        }
        else
        {
            Weapon.WeaponTrail.enabled = false;
        }
    }

    public void NAtkSound(PlayerWeapon Weapon)
    {
        GameSoundManager.Instance.SetPlayerFx("sword1", Weapon.CurrentWeapon.gameObject.transform);
        //각 무기의 사운드 출력 조정
    }

    public void SAtkSound(PlayerWeapon Weapon)
    {
        GameSoundManager.Instance.SetPlayerFx("sword3", Weapon.CurrentWeapon.gameObject.transform);
    }
}
