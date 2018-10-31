using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    private string itemID;
    private int quantityOfItem;
	void Start () {
        GameObject childItemID = GetComponentInChildren<Transform>().gameObject;
        itemID = childItemID.tag;
        quantityOfItem = 0;
	}
	
	// Update is called once per frame

    public string GetItemID()
    {
        return itemID;
    }

    public void CallIncreaseQuantity()
    {
        IncreaseQuantity();
    }
    private void IncreaseQuantity()
    {
        quantityOfItem++;
    }

    public int GetQuantity()
    {
        return quantityOfItem;
    }
}
