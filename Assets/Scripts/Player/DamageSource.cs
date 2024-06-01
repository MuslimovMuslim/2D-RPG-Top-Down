using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    private int damageAmount;

//Получает текущий активный оружие из ActiveWeapon.Instance.CurrentActiveWeapon.
//Приводит это оружие к интерфейсу IWeapon и получает информацию о его уроне (weaponDamage), которую сохраняет в damageAmount.
    private void Start() {
        MonoBehaviour currenActiveWeapon = ActiveWeapon.Instance.CurrentActiveWeapon;
        damageAmount = (currenActiveWeapon as IWeapon).GetWeaponInfo().weaponDamage;
    }

//Этот метод вызывается при столкновении объекта с триггером.
//Проверяет, имеет ли объект, с которым произошло столкновение, компонент EnemyHealth.
//Если имеет, вызывает метод TakeDamage этого компонента, передавая количество урона (damageAmount).
    private void OnTriggerEnter2D(Collider2D other) {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        enemyHealth?.TakeDamage(damageAmount);
    }
}
