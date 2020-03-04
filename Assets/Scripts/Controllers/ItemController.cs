using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour, IInteractable
{
    public ItemBlueprint item;

    public void Interact(GameObject interactor)
    {
        interactor.GetComponent<InventoryController>().AddItem(item);
        Destroy(gameObject);
    }
}
