using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon<T>
{
    void WeaponSetting(T Weapon);

    void ChangeExit(T Weapon);

    void StartAttack(T Weapon);

    void EndAttack(T Weapon);

    void WeaponSkill(T Weapon);

    void WeaponArua(T Weapon);

    void SetTrail(T Weapon);

    void NAtkSound(T Weapon);

    void SAtkSound(T Weapon);
}
