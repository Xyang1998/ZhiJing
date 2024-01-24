using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

//将场景中的物品添加到背包中
//弹出提示框
public class ItemInScene : MonoBehaviour
{
    public Item thisItem;//物品的信息
    public Inventory playerBag; //存放在哪个背包
    public GameObject hint;//提示框
    private Text hinttext;//提示框的文字
    private Image hintimage;//提示框的图片

    private void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = thisItem.itemImage;//将场景中的物品图片替换为背包中的物品图片
    }

    //物品碰撞后拾取
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AddNewItem();
             Displaytext();
            Destroy(this.gameObject);
        }
    }
    //物品点击后拾取
    public void OnMouseDown()
    {
        AddNewItem();
        Displaytext();
        Destroy(this.gameObject);
    }
    

    public void AddNewItem()//在背包里面添加新物品
    {
        if (!playerBag.itemList.Contains(thisItem))
        {
            //playerBag.itemList.Add(thisItem);//向后端添加物品
            SystemMediator.Instance.inventoryManager.CreateNewItem(thisItem);//从后端获取物品信息，传送给UI
        }
    }

    public void Displaytext()//显示拾取提示
    {
        string message = "你获得了" + thisItem.itemName;
        string information = "物品信息：  " + thisItem.itemInfo;
        
        hinttext = hint.transform.Find("text").GetComponent<Text>();
        hinttext.text =   message + "\n" + information;
        hintimage = hint.transform.Find("ItemImage").GetComponent<Image>();
        hintimage.sprite = thisItem.itemImage;
        hint.SetActive(true);
        
    }

}
