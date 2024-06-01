using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Этот класс ActiveWeapon отвечает за управление текущим активным оружием игрока в игре.
public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon { get; private set; }

    private PlayerControls playerControls;
    private float timeBetweenAttacks;

    private bool attackButtonDown, isAttacking = false;

    protected override void Awake() {
        base.Awake();

        playerControls = new PlayerControls();
    }

//Методы OnEnable() и Start(): Эти методы используются для активации PlayerControls и установки обработчиков событий для атаки
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void Start()
    {
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();

        AttackCooldown();
    }

    private void Update() {
        Attack();
    }

//Этот метод устанавливает новое оружие как текущее активное оружие игрока.
    public void NewWeapon(MonoBehaviour newWeapon) {
        CurrentActiveWeapon = newWeapon;

        AttackCooldown();
        timeBetweenAttacks = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;
    }

//Этот метод устанавливает текущее активное оружие в значение null, то есть игрок больше не держит оружие.
    public void WeaponNull() {
        CurrentActiveWeapon = null;
    }

//Этот метод запускает отсчет времени между атаками перед следующей атакой.

    private void AttackCooldown() {
        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksRoutine());
    }

//Этот метод реализует отсчет времени между атаками.
    private IEnumerator TimeBetweenAttacksRoutine() {
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
    }

//StartAttacking() и StopAttacking(): Эти методы обрабатывают нажатие и отпускание кнопки атаки.
    private void StartAttacking()
    {
        attackButtonDown = true;
    }
    private void StopAttacking()
    {
        attackButtonDown = false;
    }

//Этот метод вызывает атаку, если игрок нажимает кнопку атаки, и проверяет, доступно ли оружие и не идет ли уже атака.
    private void Attack() {
        if (attackButtonDown && !isAttacking && CurrentActiveWeapon) {
            AttackCooldown();
            (CurrentActiveWeapon as IWeapon).Attack();
        }
    }
}
