using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Этот класс отвечает за управление анимацией удара.
public class SlashAnim : MonoBehaviour
{
    private ParticleSystem ps;

//Инициализирует систему частиц, получая её компонент.
    private void Awake() {
        ps = GetComponent<ParticleSystem>();
    }

//Проверяет, активна ли система частиц (ps.IsAlive()), и если она больше не активна, уничтожает объект
    private void Update() {
        if (ps && !ps.IsAlive()) {
            DestroySelf();
        }
    }

//Уничтожает игровой объект.
    public void DestroySelf() {
        Destroy(gameObject);
    }
}
