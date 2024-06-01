using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//представляет собой класс, который реализует интерфейс IWeapon. Этот класс управляет поведением оружия в игре.
public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject magicLaser;
    [SerializeField] private Transform magicLaserSpawnPoint;

    private Animator myAnimator;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack");

//Этот метод вызывается при инициализации объекта. Здесь аниматор инициализируется.
    private void Awake() {
        myAnimator = GetComponent<Animator>();
    }

//Этот метод вызывается каждый кадр. Здесь вызывается метод MouseFollowWithOffset(), который заставляет оружие следовать за мышью с некоторым смещением.
    private void Update() {
        MouseFollowWithOffset();
    }

//Этот метод вызывается, когда игрок атакует. Он запускает анимацию атаки.
    public void Attack() {
        myAnimator.SetTrigger(ATTACK_HASH);
    }

//Этот метод вызывается во время анимации атаки (через Animation Event). Он создает новый магический лазер в точке спавна.
    public void SpawnStaffProjectileAnimEvent() {
        GameObject newLaser = Instantiate(magicLaser, magicLaserSpawnPoint.position, Quaternion.identity);
        newLaser.GetComponent<MagicLaser>().UpdateLaserRange(weaponInfo.weaponRange);
    }

//Этот метод возвращает информацию об оружии.
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

//Этот метод заставляет оружие следовать за мышью с некоторым смещением. Он вычисляет угол между мышью и игроком и поворачивает оружие в этом направлении.
    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
