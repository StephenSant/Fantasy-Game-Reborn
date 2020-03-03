using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StackableItem", menuName = "Item/Stackable")]
public class StackableBlueprint : ItemBlueprint
{
    public int amount;
    public ItemType itemType;

    public float weightStacked;
    public int valueStacked;

    public void UpdateItem()
    {
        weightStacked = weight * amount;
        valueStacked = value * amount; 
    }
}
public enum ItemType
{
    Food,
    Quest
}