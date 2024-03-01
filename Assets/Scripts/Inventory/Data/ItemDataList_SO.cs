using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataList_SO",menuName = "Inventory/ItemDataList_SO")]
public class ItemDataList_SO : ScriptableObject
{
    public List<ItemDetails> itemDetailsList;

    public ItemDetails GetItemDetails(ItemName itemName)
    {
        return itemDetailsList.Find(i => i.itemName == itemName);
    }
}

[System.Serializable]
public class ItemDetails
{
    public ItemName itemName;
    public Sprite itemSprite;
    
}