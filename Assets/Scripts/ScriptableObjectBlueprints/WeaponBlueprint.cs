using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponItem", menuName = "Item/Weapon")]
public class WeaponBlueprint : ItemBlueprint
{
    public int damage;
    public float range;
    public WeaponType weaponType;
}
public enum WeaponType
{
    MainHand,
    OffHand,
    BothHand
}