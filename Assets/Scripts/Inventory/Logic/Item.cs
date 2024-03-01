using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemName itemName;

    public void ItemClicked()
    {
        //添加物品到背包，并隐藏
        InventoryManager.Instance.AddItem(itemName);
        this.gameObject.SetActive(false);
    }
    
}
