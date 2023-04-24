using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

[CommandInfo("FungusEX","MyCall","跳转扩展")]
public class MyCall : Call
{
    public void SetTargetBlock(Block block)
    {
        targetBlock = block;
    }
}
