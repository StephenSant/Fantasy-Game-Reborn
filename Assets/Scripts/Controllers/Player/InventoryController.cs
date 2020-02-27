using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public int money;
    public List<ItemBlueprint> items;
    public List<SlotInfo> slots;
    public ItemBlueprint itemForAdding;
    public WeaponBlueprint weapon;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            AddItem(itemForAdding);
        }
    }

    public void AddItem(ItemBlueprint itemToAdd)
    {
        if (items.Contains(itemToAdd) /*|| weapons ||armour*/)
        {
            foreach (StackableBlueprint stackable in items)
            {
                if (itemToAdd.itemName == stackable.itemName)
                {
                    stackable.amount++;
                }
            }
        }
        else
        {
            items.Add(itemToAdd);
        }
    }

}
public struct SlotInfo
{
    public Text namePlace;
    public Sprite iconPlace;
    public Text amountPlace;
}