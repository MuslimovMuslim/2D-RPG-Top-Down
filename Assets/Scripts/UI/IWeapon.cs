//Это интерфейс, который определяет общие методы-
//для всех оружий. Он содержит методы Attack и GetWeaponInfo

interface IWeapon {
    public void Attack();
    public WeaponInfo GetWeaponInfo();
}
