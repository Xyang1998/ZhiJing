using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class GivePlayerItem : Command
{
    private int itemid;
    public void Init(int id)
    {
        itemid = id;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        SystemMediator.Instance.inventoryManager.AddItemToBagByID(itemid);
        Continue();
    }
}
