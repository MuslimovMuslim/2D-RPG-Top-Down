using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Этот класс управляет перемещением врага.
public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private Knockback knockback;
    private SpriteRenderer spriteRenderer;

//вызывается при создании объекта. Инициализирует компоненты.
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
        rb = GetComponent<Rigidbody2D>();
    }

//вызывается на каждом фиксированном кадре. Управляет перемещением и переворотом спрайта.
    private void FixedUpdate() {
        if (knockback.GettingKnockedBack) { return; }

        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));

        if (moveDir.x < 0) {
            spriteRenderer.flipX = true;
        } else if (moveDir.x > 0) {
            spriteRenderer.flipX = false;
        }
    }

//задает направление движения.
    public void MoveTo(Vector2 targetPosition) {
        moveDir = targetPosition;
    }

//останавливает движение.
    public void StopMoving() {
        moveDir = Vector3.zero;
    }
}
