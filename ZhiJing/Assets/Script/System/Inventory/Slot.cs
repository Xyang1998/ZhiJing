 using System.Collections;
using System.Collections.Generic;
 using UnityEngine.UI;
using UnityEngine;

public class Slot : MonoBehaviour//背包UI前端的物品
{
   public Item slotItem;
   public Image soltImage;
   public void ItemOnClick()//物品背包UI点击后显示物品信息
   {
      SystemMediator.Instance.inventoryManager.UpdateItemInfo(slotItem.itemInfo);
      Debug.Log(slotItem.itemName);
      SystemMediator.Instance.inventoryManager.UpdateItemName(slotItem.itemName);
      
   }
}
