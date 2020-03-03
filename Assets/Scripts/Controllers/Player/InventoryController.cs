using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryController : MonoBehaviour
{
    public int money;
    public List<ItemBlueprint> items;
    [SerializeField]
    public List<SlotController> slots;
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
            UpdateInventory();
        }
    }

    public void UpdateInventory()
    {
        //DONT LOOK AT MY SHAME!

        for (int i = 0; i < UIManager.instance.slotParent.transform.childCount; i++)
        {
            Destroy (UIManager.instance.slotParent.transform.GetChild(i).gameObject);
        }
        int a = 1;
        foreach (ItemBlueprint item in items)
        {
            GameObject slotTemp;
            slotTemp = Instantiate(UIManager.instance.slotPrefab, UIManager.instance.slotParent.GetComponent<RectTransform>().position + Vector3.down * (a*55-27.5f), UIManager.instance.slotPrefab.transform.rotation, UIManager.instance.slotParent.transform);
            slotTemp.GetComponent<SlotController>().item = item;
            a++;
        }

        for (int i = 0; i < UIManager.instance.slotParent.transform.childCount; i++)
        {
            UIManager.instance.slotParent.transform.GetChild(i).GetComponent<SlotController>().UpdateSlot();
        }
    }
}
