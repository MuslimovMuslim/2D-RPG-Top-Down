using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Этот скрипт заставляет объект следовать за мышью.
// В каждом кадре (в методе Update) вызывается метод
// FaceMouse, который вычисляет позицию мыши и поворачивает
// объект в сторону мыши.
public class MouseFollow : MonoBehaviour
{
    private void Update() {
        FaceMouse();
    }

    private void FaceMouse() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = transform.position - mousePosition;

        transform.right = -direction;
    }
}
