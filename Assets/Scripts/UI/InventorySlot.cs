using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Этот скрипт представляет собой слот инвентаря. Он содержит-
// информацию об оружии (weaponInfo)-
// и метод GetWeaponInfo, который возвращает эту информацию.
public class InventorySlot : MonoBehaviour
{
    [SerializeField] private WeaponInfo weaponInfo;

    public WeaponInfo GetWeaponInfo() {
        return weaponInfo;
    }
}
