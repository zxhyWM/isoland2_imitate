using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    //获得变量
    public Button leftButton, rightButton;

    public int currentIndex;//显示ui物品的序号

    public SlotUI slotUI;

    private void OnEnable()
    {
        EventHandler.UpdateUIEvent += OnUpdateUIEvent;
    }

    private void OnDisable()
    {
        EventHandler.UpdateUIEvent -= OnUpdateUIEvent;
    }

    private void OnUpdateUIEvent(ItemDetails itemDetails, int index)
    {
        if (itemDetails == null)
        {
            slotUI.SetEmpty();
            currentIndex = -1;
            leftButton.interactable = false;
            rightButton.interactable = false;
        }
        else
        {
            currentIndex = index;
            slotUI.SetItem(itemDetails);

            if (index > 0)
                leftButton.interactable = true;
            if (index == -1)
            {
                leftButton.interactable = false;
                rightButton.interactable = false;
            }
        }
    }
    
    public void SwitchItem(int amount)
    {
        //点击左键减一个，右键加一个amount传值-1或+1
        var index = currentIndex + amount;
        if (index < currentIndex)
        {
            leftButton.interactable = false;
            rightButton.interactable = true;
        }else if (index > currentIndex)
        {
            leftButton.interactable = true;
            rightButton.interactable = false;
        }
        else
        {
            leftButton.interactable = true;
            rightButton.interactable = true;
        }
        //拿到index后传进去
        EventHandler.CallChangeItemEvent(index);
    }
    
}
