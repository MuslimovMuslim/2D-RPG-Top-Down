using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//отвечает за поведение снарядов (проектилей) в игре.
public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleOnHitPrefabVFX;
    [SerializeField] private bool isEnemyProjectile = false;
    [SerializeField] private float projectileRange = 10f;

    private Vector3 startPosition;

//Сохраняет начальную позицию снаряда.
    private void Start() {
        startPosition = transform.position;
    }

// Каждую рамку вызывает методы для движения снаряда и проверки дальности.
    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

//Обновляет дальность полёта снаряда.
    public void UpdateProjectileRange(float projectileRange){
        this.projectileRange = projectileRange;
    }

//Обновляет скорость движения снаряда.
    public void UpdateMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }


//Обрабатывает столкновения с объектами, нанося урон игроку или врагу, и создаёт визуальный эффект при попадании.
    private void OnTriggerEnter2D(Collider2D other) {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        Indestructible indestructible = other.gameObject.GetComponent<Indestructible>();
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();

        if (!other.isTrigger && (enemyHealth || indestructible || player)) {
            if ((player && isEnemyProjectile) || (enemyHealth && !isEnemyProjectile)) {
                player?.TakeDamage(1, transform);
                Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            } else if (!other.isTrigger && indestructible) {
                Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

//Проверяет, не превысил ли снаряд свою дальность полёта, и уничтожает его, если это так.
    private void DetectFireDistance() {
        if (Vector3.Distance(transform.position, startPosition) > projectileRange) {
            Destroy(gameObject);
        }
    }

//Двигает снаряд вперёд.
    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }
}
