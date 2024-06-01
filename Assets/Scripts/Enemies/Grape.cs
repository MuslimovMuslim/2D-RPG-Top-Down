using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Этот класс управляет врагом типа "Виноград".
public class Grape : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject grapeProjectilePrefab;

    private Animator myAnimator;
    private SpriteRenderer spriteRenderer;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack");

//вызывается при создании объекта. Инициализирует компоненты.
    private void Awake() {
        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

//выполняет атаку, активирует анимацию и переворот спрайта.
    public void Attack() {
        myAnimator.SetTrigger(ATTACK_HASH);

        if (transform.position.x - PlayerController.Instance.transform.position.x < 0) {
            spriteRenderer.flipX = false;
        } else {
            spriteRenderer.flipX = true;
        }
    }

//создает снаряд при анимации атаки.
    public void SpawnProjectileAnimEvent() {
        Instantiate(grapeProjectilePrefab, transform.position, Quaternion.identity);
    }
}
