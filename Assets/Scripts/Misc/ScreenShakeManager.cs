using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShakeManager : Singleton<ScreenShakeManager>
{
    private CinemachineImpulseSource source; // Источник импульса для встряски экрана

    // Метод, вызываемый при пробуждении объекта
    protected override void Awake() {
        base.Awake(); // Вызов метода Awake() базового класса
        
        source = GetComponent<CinemachineImpulseSource>(); // Получение компонента CinemachineImpulseSource
    }

    // Метод для встряски экрана
    public void ShakeScreen() {
        source.GenerateImpulse(); // Генерация импульса
    }
}
