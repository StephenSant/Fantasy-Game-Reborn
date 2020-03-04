using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryController : MonoBehaviour
{
    public int money;
    public List<ItemSlot> slots;
    public WeaponBlueprint weapon;
    public float maxWeight;
    [SerializeField]
    float totalWeight;

    int selectedSlotTemp;
    public void LateUpdate()
    {
        
        totalWeight = CheckWeight();
        if (UIManager.instance.selectedSlot != selectedSlotTemp)
        {
            selectedSlotTemp = UIManager.instance.selectedSlot;
            UpdateSelectedItem();
        }
    }

    public float CheckWeight()
    {
        if (slots.Count != 0)
        {
            float weightTemp = 0;
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].item is StackableBlueprint)
                {
                    StackableBlueprint itemTemp = slots[i].item as StackableBlueprint;
                    slots[i].Weight = itemTemp.weight * slots[i].amount;
                }
                else
                {
                    weightTemp = slots[i].item.weight;
                }
            }
            if (weapon != null)
            {
                weightTemp += weapon.weight;
            }

            //Also put in the armour's weight!

            return weightTemp;
        }
        else
        {
            return 0;
        }
    }

    public void AddItem(ItemBlueprint itemToAdd)
    {
        //if its too heavy
        if (totalWeight + itemToAdd.weight > maxWeight)
        {
            Debug.Log("Too heavy.");
            return;
        }
        //if its not too heavy
        else
        {
            for (int i = 0; i < slots.Count; i++)
            {

                //if we have the item and is stackable 
                if (slots[i].item == itemToAdd /*|| !weapons ||!armour*/)
                {
                    //if the item is stackable
                    if (itemToAdd is StackableBlueprint)
                    {
                        //add to stack
                        slots[i].amount++;
                        UpdateInventory();
                        return;
                    }
                }
            }
            //make a new slot
            ItemSlot newItem = new ItemSlot();
            newItem.item = itemToAdd;
            newItem.amount = 1;
            newItem.value = itemToAdd.value;
            newItem.weight = itemToAdd.weight;
            slots.Add(newItem);
            UpdateInventory();
            return;
        }
    }

    public void UpdateInventory()
    {
        //get rid of the slots
        for (int i = 0; i < UIManager.instance.slotParent.transform.childCount; i++)
        {
            Destroy(UIManager.instance.slotParent.transform.GetChild(i).gameObject);
        }

        //replace the slots
        for (int i = 0; i < slots.Count; i++)
        {
            GameObject slotTemp;
            //places slot
            slotTemp = Instantiate(UIManager.instance.slotPrefab, UIManager.instance.slotParent.GetComponent<RectTransform>().position + Vector3.down * (i * 55 + 27.5f), UIManager.instance.slotPrefab.transform.rotation, UIManager.instance.slotParent.transform);
            //set slot
            slotTemp.GetComponent<SlotController>().itemSlot = slots[i];
            slotTemp.GetComponent<SlotController>().slotId = i+1;

        }

        ////Update slots to make sure they're right
        for (int i = 0; i < UIManager.instance.slotParent.transform.childCount; i++)
        {
            UIManager.instance.slotParent.transform.GetChild(i).GetComponent<SlotController>().UpdateSlot();
        }
    }

    public void UpdateSelectedItem()
    {
        UIManager manager = UIManager.instance;
        manager.selectedItemImage.sprite = slots[manager.selectedSlot-1].item.icon;
        manager.selectedItemName.text = slots[manager.selectedSlot-1].item.name;
        //manager.selectedItemInfo.text =  slots[manager.selectedSlot]
        //health, damage, defence, range, ect...
    }
}
[System.Serializable]
public class ItemSlot
{
    public ItemBlueprint item;
    public int amount;
    public int value;
    public float weight;

    public float Weight
    {
        set { weight = value; }
        get { return weight; }
    }
}
