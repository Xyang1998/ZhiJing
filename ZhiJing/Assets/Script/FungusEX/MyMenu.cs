using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("FungusEX","MyMenu","选项扩展")]
public class MyMenu : Menu
{
    public void SetTargetBlock(Block block)
    {
        targetBlock = block;
    }

    public bool CheckValues()
    {
        return true;
    }
}
