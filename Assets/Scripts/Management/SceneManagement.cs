using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//используется для управления переходами между сценами
public class SceneManagement : Singleton<SceneManagement>
{
    public string SceneTransitionName { get; private set; }

//Этот метод позволяет установить имя перехода между сценами. 
//Он принимает один аргумент - sceneTransitionName, который 
//становится новым именем перехода.
    public void SetTransitionName(string sceneTransitionName) {
        this.SceneTransitionName = sceneTransitionName;
    }
}

