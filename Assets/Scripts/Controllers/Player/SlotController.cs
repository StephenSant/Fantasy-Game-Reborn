using UnityEngine;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    public ItemSlot itemSlot;
    public Text namePlace;
    public Image iconPlace;
    public Text amountPlace;
    public Text weightPlace;
    public Text valuePlace;

    public void UpdateSlot()
    {
        namePlace.text = itemSlot.item.itemName;
        iconPlace.sprite = itemSlot.item.icon;
        amountPlace.text = "" + itemSlot.amount;
        weightPlace.text = "" + itemSlot.weight;
        valuePlace.text = "" + itemSlot.value;
    }
}
