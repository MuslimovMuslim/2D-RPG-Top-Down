using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*Этот класс используется для управления выходами 
из областей в игре. Когда игрок пересекает триггер 
выхода из области (метод OnTriggerEnter2D), он начинает
 процесс перехода в другую сцену (sceneToLoad). 
 Это включает в себя установку имени перехода в 
 SceneManagement, затемнение экрана с помощью UIFade,
  и, наконец, загрузку новой 
сцены после короткой задержки (LoadSceneRoutine).*/
public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;

    private float waitToLoadTime = 1f;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerController>()) {
            SceneManagement.Instance.SetTransitionName(sceneTransitionName);
            UIFade.Instance.FadeToBlack();
            StartCoroutine(LoadSceneRoutine());
        }
    }

    private IEnumerator LoadSceneRoutine() {
        while (waitToLoadTime >= 0) 
        {
            waitToLoadTime -= Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}
