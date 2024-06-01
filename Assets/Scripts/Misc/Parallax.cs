using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxOffset = -0.15f;

    private Camera cam;
    private Vector2 startPos;
    private Vector2 travel => (Vector2)cam.transform.position - startPos;

// Метод Awake вызывается при загрузке экземпляра скрипта.
    private void Awake() {
        cam = Camera.main;
    }


// Записывает начальную позицию.
    private void Start() {
        startPos = transform.position;
    }

// Метод FixedUpdate вызывается с фиксированным интервалом.
// Корректирует позицию на основе движения камеры для параллакс-эффекта.
    private void FixedUpdate() {
        transform.position = startPos + travel * parallaxOffset;
    }
}
