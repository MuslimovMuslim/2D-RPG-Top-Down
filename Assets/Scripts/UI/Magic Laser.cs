using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Этот скрипт управляет поведением магического лазера. Он содержит следующие ключевые методы:
public class MagicLaser : MonoBehaviour
{
    [SerializeField] private float laserGrowTime = 2f;

    private bool isGrowing = true;
    private float laserRange;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider2D;

// Инициализирует компоненты SpriteRenderer и CapsuleCollider2D.
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

//Вызывает метод LaserFaceMouse, который поворачивает лазер в сторону мыши.
    private void Start() {
        LaserFaceMouse();
    }

// Если лазер сталкивается с неразрушимым объектом, он перестает расти.
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<Indestructible>() && !other.isTrigger) {
            isGrowing = false;
        }
    }

//Обновляет дальность лазера и запускает корутину для увеличения длины лазера.
    public void UpdateLaserRange(float laserRange) {
        this.laserRange = laserRange;
        StartCoroutine(IncreaseLaserLengthRoutine());
    }

//Это корутина, которая постепенно увеличивает длину лазера до заданной дальности. Она также обновляет размер и смещение коллайдера.
    private IEnumerator IncreaseLaserLengthRoutine() {
        float timePassed = 0f;

        while (spriteRenderer.size.x < laserRange && isGrowing)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / laserGrowTime;

            // sprite 
            spriteRenderer.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), 1f);

            // collider
            capsuleCollider2D.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), capsuleCollider2D.size.y);
            capsuleCollider2D.offset = new Vector2((Mathf.Lerp(1f, laserRange, linearT)) / 2, capsuleCollider2D.offset.y);

            yield return null;
        }

        StartCoroutine(GetComponent<SpriteFade>().SlowFadeRoutine());
    }

//Поворачивает лазер в сторону мыши.
    private void LaserFaceMouse() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = transform.position - mousePosition;
        transform.right = -direction;
    }
}
