using UnityEngine;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    public ItemBlueprint item;
    public Text namePlace;
    public Image iconPlace;
    public Text amountPlace;
    public Text weightPlace;
    public Text valuePlace;

    public void UpdateSlot()
    {
        namePlace.text = item.itemName;
        iconPlace.sprite = item.icon;
        weightPlace.text = "" + item.weight;
        valuePlace.text = "" + item.value;
        if (item is StackableBlueprint)
        {
            StackableBlueprint itemTemp = item as StackableBlueprint;
            amountPlace.text = ""+itemTemp.amount;
        }
    }
}
