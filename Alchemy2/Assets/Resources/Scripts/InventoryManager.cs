using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    // Use this for initialization
    private List<Item> inventory = new List<Item>();
	void Update () {
	}

    public void CallAddItem(Item currentItem)
    {
        AddItem(currentItem);
    }

    private void AddItem(Item currentItem)
    {
        int indexOfItem = -1;
        foreach(Item item in inventory)
        {
            if(item.GetItemID() == currentItem.GetItemID())
            {
                indexOfItem = inventory.IndexOf(item);
            }
        }

        if(indexOfItem > -1)
        {
            inventory[indexOfItem].CallIncreaseQuantity();
        }
        else
        {
            currentItem.CallIncreaseQuantity();
            inventory.Add(currentItem);
        }
    }

    public List<Item> GetInvetory()
    {
        return inventory;
    }

}
