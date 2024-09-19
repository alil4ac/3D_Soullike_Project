using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponState<T>
{
    public IWeapon<T> CurrentWapon { get; private set; }

    public T m_Weapon;

    public WeaponState(T Weapon, IWeapon<T> Type)
    {
        m_Weapon = Weapon;

        Initialze(Type);
    }

    public void Initialze(IWeapon<T> Type)
    {
        CurrentWapon = Type;

        CurrentWapon.WeaponSetting(m_Weapon);
    }

    public void ChangeWeapon(IWeapon<T> NewWeapon)
    {
        CurrentWapon.ChangeExit(m_Weapon);

        CurrentWapon = NewWeapon;

        CurrentWapon.WeaponSetting(m_Weapon);
    }

    public void StartAttack()
    {
        CurrentWapon.StartAttack(m_Weapon);
    }

    public void EndAttack()
    {
        CurrentWapon.EndAttack(m_Weapon);
    }

    public void WeaponSkill()
    {
        CurrentWapon.WeaponSkill(m_Weapon);
    }

    public void WeaponArua()
    {
        CurrentWapon.WeaponArua(m_Weapon);
    }

    public void SetTrail()
    {
        CurrentWapon.SetTrail(m_Weapon);
    }

    public void NAtkSound()
    {
        CurrentWapon.NAtkSound(m_Weapon);
    }

    public void SAtkSound()
    {
        CurrentWapon.SAtkSound(m_Weapon);
    }
}
