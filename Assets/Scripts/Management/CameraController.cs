using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : Singleton<CameraController>
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;

//Этот метод вызывается при старте игры. Внутри этого метода вызывается метод SetPlayerCameraFollow(), который устанавливает камеру так, чтобы она следовала за игроком.
    private void Start() {
        SetPlayerCameraFollow();
    }

//Этот метод устанавливает камеру так, чтобы она следовала за игроком.
    public void SetPlayerCameraFollow() {
        cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        cinemachineVirtualCamera.Follow = PlayerController.Instance.transform;
    }
}
