using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : IWeapon<PlayerWeapon>
{
    Vector3 tr = new Vector3(-0.035f, -0.559f, 0.173f);
    Vector3 rot = new Vector3(80.29f, -17.672f, -191.408f);
    Vector3 scale = new Vector3(0.03849118f, 0.03849118f, 0.01782181f);
    public void StartAttack(PlayerWeapon Weapon)
    {
        Weapon.WeaponCol.enabled = !Weapon.WeaponCol.enabled;
    }

    public void EndAttack(PlayerWeapon Weapon)
    {
        Weapon.WeaponCol.enabled = false;
        Weapon.AruaPrefab.IsActive = false;
        Weapon.WeaponTrail.enabled = false;
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
        Debug.Log("Set Katana");
        foreach (GameObject wp in Weapon.WeaponList)
        {
            if (wp.name == ItemColumn.EWeapon.Katana.ToString())
            {
                wp.gameObject.SetActive(true);
                Weapon.CurrentWeapon = wp;
                wp.transform.localPosition = tr;
                wp.transform.localRotation = Quaternion.Euler(rot);
                wp.transform.localScale = scale;
                Weapon.WeaponCol = wp.GetComponent<Collider>();
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


    public void WeaponArua(PlayerWeapon Weapon)
    {
        Weapon.AruaPrefab.IsActive = !Weapon.AruaPrefab.IsActive;
    }

    public void NAtkSound(PlayerWeapon Weapon)
    {

        GameSoundManager.Instance.SetPlayerFx("katana1", Weapon.CurrentWeapon.gameObject.transform);

    }

    public void SAtkSound(PlayerWeapon Weapon)
    {

        GameSoundManager.Instance.SetPlayerFx("katana2", Weapon.CurrentWeapon.gameObject.transform);

    }
}
