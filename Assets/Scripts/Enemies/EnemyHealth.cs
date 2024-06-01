using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Этот класс управляет здоровьем врага
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private float knockBackThrust = 15f;

    private int currentHealth;
    private Knockback knockback;
    private Flash flash;

//вызывается при создании объекта. Инициализирует компоненты.
    private void Awake() {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

//вызывается при старте игры. Устанавливает текущее здоровье.
    private void Start() {
        currentHealth = startingHealth;
    }

//уменьшает здоровье врага, вызывает отбрасывание и проверяет на смерть.
    public void TakeDamage(int damage) {
        currentHealth -= damage;
        knockback.GetKnockedBack(PlayerController.Instance.transform, knockBackThrust);
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
    }

//корутина для проверки смерти после задержки.
    private IEnumerator CheckDetectDeathRoutine() {
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DetectDeath();
    }

//проверяет здоровье и уничтожает врага при его отсутствии.
    public void DetectDeath() {
        if (currentHealth <= 0) {
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            GetComponent<PickUpSpawner>().DropItems();
            Destroy(gameObject);
        }
    }
}
