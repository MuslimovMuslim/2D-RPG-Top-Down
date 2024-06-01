using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//управляет активным инвентарем в игре. Вот что делают его методы:
public class ActiveInventory : Singleton<ActiveInventory>
{
    private int activeSlotIndexNum = 0;

    private PlayerControls playerControls;


//Этот метод вызывается при инициализации объекта. Здесь инициализируется playerControls.
    protected override void Awake() {
        base.Awake();

        playerControls = new PlayerControls();
    }

//Этот метод вызывается после инициализации всех объектов.Зд-
//-есь устанавливается обработчик событий для клавиатуры, который вызывает ToggleActiveSlot при нажатии клавиш.
    private void Start() {
        playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
    }

//Этот метод вызывается, когда объект становится активным. Здесь включаются playerControls
    private void OnEnable() {
        playerControls.Enable();
    }
//Этот метод вызывается, чтобы экипировать начальное оружие.
// Он вызывает ToggleActiveHighlight с индексом 0.
    public void EquipStartingWeapon() {
        ToggleActiveHighlight(0);
    }

// Этот метод переключает активный слот на основе переданного значения.
// Он вызывает ToggleActiveHighlight с индексом, уменьшенным на 1.
    private void ToggleActiveSlot(int numValue) {
        ToggleActiveHighlight(numValue - 1);
    }

// Этот метод выделяет активный слот и вызывает ChangeActiveWeapon.
// Он также делает все остальные слоты неактивными.
    private void ToggleActiveHighlight(int indexNum) {
        activeSlotIndexNum = indexNum;

        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

        ChangeActiveWeapon();
    }

/*Этот метод меняет активное оружие.
 Если у ActiveWeapon.Instance уже есть 
 активное оружие, оно уничтожается. Затем
  создается новое оружие на основе информации 
  о оружии в активном слоте инвентаря.*/ 
    private void ChangeActiveWeapon() {

        if (ActiveWeapon.Instance.CurrentActiveWeapon != null) {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        Transform childTransform = transform.GetChild(activeSlotIndexNum);
        InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
        WeaponInfo weaponInfo = inventorySlot.GetWeaponInfo();
        GameObject weaponToSpawn = weaponInfo.weaponPrefab;
        
        if (weaponInfo == null) {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }


        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform);

        //ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        //newWeapon.transform.parent = ActiveWeapon.Instance.transform;

        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }
}
