using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//отвечает за поведение меча, которым управляет игрок.
public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private float swordAttackCD = .5f;
    [SerializeField] private WeaponInfo weaponInfo;

    private Transform weaponCollider;
    private Animator myAnimator;

    private GameObject slashAnim;

// Инициализация аниматора.
    private void Awake() {
        myAnimator = GetComponent<Animator>();
    }

//Получение коллайдера оружия и точки появления анимации.
    private void Start() {
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        slashAnimSpawnPoint = GameObject.Find("SlashSpawnPoint").transform;
    }

//Обновление позиции меча относительно мыши.
    private void Update() {
        MouseFollowWithOffset();
    }

//Возвращает информацию о мече.
    public WeaponInfo GetWeaponInfo() {
        return weaponInfo;
    }

// Выполняет атаку мечом, активирует коллайдер и создаёт анимацию удара.
    public void Attack() {
        myAnimator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);
        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
    }

// Деактивирует коллайдер после завершения анимации атаки.
    public void DoneAttackingAnimEvent() {
        weaponCollider.gameObject.SetActive(false);
    }


//SwingUpFlipAnimEvent, SwingDownFlipAnimEvent: Управляют ориентацией анимации удара в зависимости от направления.
    public void SwingUpFlipAnimEvent() {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (PlayerController.Instance.FacingLeft) { 
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    public void SwingDownFlipAnimEvent() {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

//Обновляет ориентацию меча в зависимости от положения мыши.
    private void MouseFollowWithOffset() {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x) {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        } else {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
