using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Weapon")]
public class WeaponInfo : ScriptableObject
{
//Возвращает или устанавливает префаб оружия.
    public GameObject weaponPrefab;

// Возвращает или устанавливает время перезарядки оружия.
    public float weaponCooldown;

//Возвращает или устанавливает урон оружия.
    public int weaponDamage;

//Возвращает или устанавливает дальность стрельбы оружия.
    public float weaponRange;
}
