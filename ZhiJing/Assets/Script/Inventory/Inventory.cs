using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Inventory",menuName = "Inventory/New Inventory",order = 1)]
public class Inventory : ScriptableObject//储存背包里面的数据
{
 public List<Item> itemList = new List<Item>();
 
}
