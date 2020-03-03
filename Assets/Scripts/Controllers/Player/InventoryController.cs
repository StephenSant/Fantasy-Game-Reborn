using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryController : MonoBehaviour
{
    public int money;
    public List<ItemBlueprint> items;
    [SerializeField]
    SlotController[] slots;
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

        UpdateInventory();
    }
    //WIP
    public void UpdateInventory()
    {
        
        // Crashes unity...

        //stores the slots.
        slots.GetLength(UIManager.instance.slotParent.transform.childCount);
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = UIManager.instance.slotParent.transform.GetChild(i).GetComponent<SlotController>();
        }
        slots = UIManager.instance.slotParent.GetComponentsInChildren<SlotController>();

        // if there are more items then there are slots
        for (int i = 0; items.Count > slots.Length; i++)
        {
            Instantiate(UIManager.instance.slotPrefab, UIManager.instance.slotParent.transform);
        }

        //loop through the slots to make sure they're showing the right things
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].item = items[i];
        }
    }
}
