using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : ISystem
{

    public GameObject slotGrid; //背包格子
    public Inventory mybag;//背包列表
    public Slot slotPrefab;
    public Text itemInformation;
    public Text itemName;
    void Awake()
    {

        
    }
    public override void Init()
    {
        RefreshItem();
        itemInformation.text = "";//重新加载背包UI时，物品描述栏清空
    }
    public  void UpdateItemInfo(string itemDescription)//更新物品描述栏
    {
        itemInformation.text = itemDescription;
    }

    public  void UpdateItemName(string _itemName)
    {
        itemName.text = _itemName;
    }
    //将后端mybag列表中item的信息获得，然后传送给UI的slot；
    public  void CreateNewItem(Item item)
    {
        mybag.itemList.Add(item);
        Slot newItem = Instantiate(slotPrefab, slotGrid.transform.position, quaternion.identity);//创建slot对象
        newItem.gameObject.transform.SetParent(slotGrid.transform); //设置位置
        newItem.slotItem = item;
        newItem.soltImage.sprite = item.itemImage;
    }

    public void AddItemToBagByID(int id)
    {
        var temp = Resources.Load<Item>($"Inventory/Items/{id}");
        if (temp != null)
        {
            Item item = Instantiate(temp);
            CreateNewItem(item);
        }
        
    }
    public  void RefreshItem()//重新加载背包
    {
        for (int i = 0; i < slotGrid.transform.childCount; i++)//在刷新背包UI前，先清空背包UI里的物品
        {
            if (slotGrid.transform.childCount == 0)
            {
                break;
            }
            Destroy(slotGrid.transform.GetChild(i).gameObject);
            
        }

        int count = mybag.itemList.Count;
        for (int i = 0; i < count; i++)//从后端baglist重新加载背包UI的物品
        {
            CreateNewItem(mybag.itemList[i]);
        }
    }
}
