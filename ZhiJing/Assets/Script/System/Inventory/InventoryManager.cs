using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : SingletonMono<InventoryManager>
{

    public GameObject slotGrid; //背包格子
    public Inventory mybag;//背包列表
    public Slot slotPrefab;
    public Text itemInformation;
    public Text itemName;
    void Awake()
    {

        
    }
    private void OnEnable()
    {
        RefreshItem();
        Instance.itemInformation.text = "";//重新加载背包UI时，物品描述栏清空
    }
    public static  void UpdateItemInfo(string itemDescription)//更新物品描述栏
    {
        Instance.itemInformation.text = itemDescription;
    }

    public static void UpdateItemName(string itemName)
    {
        Instance.itemName.text = itemName;
    }
    //将后端mybag列表中item的信息获得，然后传送给UI的slot；
    public static void CreateNewItem(Item item)
    {
        Slot newItem = Instantiate(Instance.slotPrefab, Instance.slotGrid.transform.position, quaternion.identity);//创建slot对象
        newItem.gameObject.transform.SetParent(Instance.slotGrid.transform); //设置位置
        newItem.slotItem = item;
        newItem.soltImage.sprite = item.itemImage;
    }
    public static void RefreshItem()//重新加载背包
    {
        for (int i = 0; i < Instance.slotGrid.transform.childCount; i++)//在刷新背包UI前，先清空背包UI里的物品
        {
            if (Instance.slotGrid.transform.childCount == 0)
            {
                break;
            }
            Destroy(Instance.slotGrid.transform.GetChild(i).gameObject);
            
        }
        for (int i = 0; i < Instance.mybag.itemList.Count; i++)//从后端baglist重新加载背包UI的物品
        {
            CreateNewItem(Instance.mybag.itemList[i]);
        }
    }
}
