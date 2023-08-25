using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName = "Inventory/New Item",order = 1)]
public class Item : ScriptableObject//使用ScriptableObject存储背包物品信息
{
    public string itemName;
    public int itemId;
    public Sprite itemImage;
    [TextArea]
    public string itemInfo;
    public bool itemAble;//判断物品是否可用
    

}
