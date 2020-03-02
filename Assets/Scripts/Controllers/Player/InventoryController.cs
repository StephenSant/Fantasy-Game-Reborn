using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public int money;
    public List<ItemBlueprint> items;
    public List<SlotInfo> slots;
    public WeaponBlueprint weapon;
    public float maxWeight;
    [SerializeField]
    float totalWeight;

    public void LateUpdate()
    {
        totalWeight = CheckWeight();
    }

    public float CheckWeight()
    {
        float weightTemp = 0;
        foreach (ItemBlueprint item in items)
        {
            if (item is StackableBlueprint)
            {
                StackableBlueprint itemTemp = item as StackableBlueprint;
                weightTemp = itemTemp.weight * itemTemp.amount;
            }
            else
            {
                weightTemp = item.weight;
            }
        }
        if (weapon != null)
        {
            weightTemp += weapon.weight;
        }

        //Also put in the armour's weight!

        return weightTemp;
    }

    public void AddItem(ItemBlueprint itemToAdd)
    {
        if (totalWeight + itemToAdd.weight > maxWeight)
        {
            Debug.Log("Too heavy.");
            return;
        }
        else
        {
            if (items.Contains(itemToAdd) /*|| !weapons ||!armour*/)
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

}
public struct SlotInfo
{
    public Text namePlace;
    public Sprite iconPlace;
    public Text amountPlace;
}