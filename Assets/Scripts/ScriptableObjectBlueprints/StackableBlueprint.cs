using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StackableItem", menuName = "Item/Stackable")]
public class StackableBlueprint : ItemBlueprint
{
    public ItemType itemType;
}
public enum ItemType
{
    Food,
    Quest
}