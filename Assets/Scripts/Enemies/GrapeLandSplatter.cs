using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//управляет анимацией удара снаряда "Винограда".
public class GrapeLandSplatter : MonoBehaviour
{
    private SpriteFade spriteFade;

    private void Awake() {
        spriteFade = GetComponent<SpriteFade>();
    }

//Запускает анимацию исчезновения и отключает коллайдер.
    private void Start() {
        StartCoroutine(spriteFade.SlowFadeRoutine());

        Invoke("DisableCollider", 0.2f);
    }

//вызывает урон игроку при столкновении.

    private void OnTriggerEnter2D(Collider2D other) {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        playerHealth?.TakeDamage(1, transform);
    }

//отключает коллайдер.
    private void DisableCollider() {
        GetComponent<CapsuleCollider2D>().enabled = false;
    }
}
