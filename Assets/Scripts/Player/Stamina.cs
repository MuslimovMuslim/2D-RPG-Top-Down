using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Этот класс отвечает за управление выносливостью игрока.
public class Stamina : Singleton<Stamina>
{
    public int CurrentStamina { get; private set; }

    [SerializeField] private Sprite fullStaminaImage, emptyStaminaImage;
    [SerializeField] private int timeBetweenStaminaRefresh = 3;

    private Transform staminaContainer;
    private int startingStamina = 3;
    private int maxStamina;
    const string STAMINA_CONTAINER_TEXT = "Stamina Container";

// Инициализация максимального и начального уровней выносливости.
    protected override void Awake() {
        base.Awake();

        maxStamina = startingStamina;
        CurrentStamina = startingStamina;
    }

// Инициализация контейнера выносливости.
    private void Start() {
        staminaContainer = GameObject.Find(STAMINA_CONTAINER_TEXT).transform;
    }

//Уменьшает текущий уровень выносливости и обновляет визуализацию.
    public void UseStamina() {
        CurrentStamina--;
        UpdateStaminaImages();
    }

//Восстанавливает единицу выносливости и обновляет визуализацию.
    public void RefreshStamina() {
        if (CurrentStamina < maxStamina) {
            CurrentStamina++;
        }
        UpdateStaminaImages();
    }

//Корутин для восстановления выносливости через заданные интервалы.
    private IEnumerator RefreshStaminaRoutine() {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenStaminaRefresh);
            RefreshStamina();
        }
    }

//Обновляет визуализацию выносливости на экране.
    private void UpdateStaminaImages() {
        for (int i = 0; i < maxStamina; i++)
        {
            if (i <= CurrentStamina - 1) {
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = fullStaminaImage;
            } else {
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = emptyStaminaImage;
            }
        }

        if (CurrentStamina < maxStamina) {
            StopAllCoroutines();
            StartCoroutine(RefreshStaminaRoutine());
        }
    }
}
