using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour, IInteractable
{
    //What it should be.
    /*
     * this should have the items variables and when it is picked up, it creates a scriptable object in the inventory!
    */

    //[Header("Base")]
    //public string itemName;
    //public int value;
    //public float weight;
    //public Sprite icon;

    //[Header("Stackable")]
    //public int amount;
    //public ItemType itemType;

    //[Header("Weapon")]
    //public int damage;
    //public float range;
    //public WeaponType weaponType;

    //[Header("Armour")]

    public ItemBlueprint item;


    public void Interact(GameObject interactor)
    {
        interactor.GetComponent<InventoryController>().AddItem(item);
        Destroy(gameObject);
    }
}
