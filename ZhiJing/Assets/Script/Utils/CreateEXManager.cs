using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using Fungus;
using UnityEditor;

/// <summary>
/// 根据xls表中type字段去创建对应命令，扩充type对应增加一个CreateTypeName方法
/// </summary>
public static class CreateCommand
{
    private static Type t = typeof(CreateCommand);
    
    public static void CreateCommandByTypeName(BaseCharacter myCharacter,Flowchart flowchart,Block block,Dial dial)
    {
        MethodInfo methodInfo = t.GetMethod("Create" +dial.type);
        if (methodInfo != null)
        {
            methodInfo.Invoke(null, new object[] { myCharacter, flowchart, block,dial});
        }
        else
        {
            Debug.Log($"没有找到名为Create{dial.type}的方法！");
        }
    }


    
    public static void CreateMySay(BaseCharacter myCharacter,Flowchart flowchart,Block block,ref Dial dial)
    {
        MySay say = Undo.AddComponent<MySay>(myCharacter.gameObject);
        GameObject character = SystemMediator.Instance.taskSystem.GetCharacter(dial.speaker); //待优化
        say.Init(myCharacter.ID, ref dial, character); //待优化
        block.GetFlowchart().AddSelectedCommand(say);
        say.ParentBlock = block;
        say.ItemId = flowchart.NextItemId();
        say.OnCommandAdded(block);
        block.CommandList.Add(say);
    }

    public static void CreateGivePlayerTask(BaseCharacter myCharacter,Flowchart flowchart,Block block,ref Dial dial)
    {
        GivePlayerTask givePlayerTask = Undo.AddComponent<GivePlayerTask>(myCharacter.gameObject);
        givePlayerTask.Init(dial.dial);
        block.GetFlowchart().AddSelectedCommand(givePlayerTask);
        givePlayerTask.ParentBlock = block;
        givePlayerTask.ItemId = flowchart.NextItemId();
        givePlayerTask.OnCommandAdded(block);
        block.CommandList.Add(givePlayerTask);
    }

    public static void CreateMyOption(BaseCharacter myCharacter, Flowchart flowchart, Block block, ref Dial dial)
    {
        MyMenu say = Undo.AddComponent<MyMenu>(myCharacter.gameObject);
        string text = dial.dial.Split("#TextEnd")[0];
        GameObject character = SystemMediator.Instance.taskSystem.GetCharacter(dial.speaker); //待优化
        Dial MenuDial = new Dial(dial.ID, dial.type,dial.speaker, text, "0");
        say.Init(myCharacter.ID, ref MenuDial, character); //待优化
        block.GetFlowchart().AddSelectedCommand(say);
        say.ParentBlock = block;
        say.ItemId = flowchart.NextItemId();
        say.OnCommandAdded(block);
        block.CommandList.Add(say);
        //Debug.Log("打印选项");
        //Debug.Log(text);
        string[] menus = dial.dial.Split("#TextEnd")[1].Split("#Menu");
        string[] jumps = dial.nextID.Split("#");
        List<MenuData> Menulist = new List<MenuData>();
        for (int i = 0; i < menus.Length - 1; i++)
        {
            //Debug.Log(menus[i]);
            //Debug.Log(jumps[i]);
            MenuData myMenuData = new MenuData(block.BlockName, jumps[i], menus[i]);
            Menulist.Add(myMenuData);
        }

        say.InitMenuList(Menulist);


    }

    public static void CreatePlayerChange(BaseCharacter myCharacter, Flowchart flowchart, Block block, ref Dial dial)
    {
        //TODO:改变玩家某属性
    }
    public static void CreateNPCChange(BaseCharacter myCharacter, Flowchart flowchart, Block block, ref Dial dial)
    {
        //TODO:改变NPC某属性
    }



    
    
}

public static class LinkCommand
{
    private static Type t = typeof(LinkCommand);
    
    public static void LinkBlocksInFlowChart(Block[] blocks)
    {
        foreach (var block in blocks)
        {
            if (block.CommandList.Count >= 1)
            {
                Command command = block.CommandList.Last();
                string cName = command.GetType().Name;
                MethodInfo methodInfo = t.GetMethod("Link" + cName);
                if (methodInfo != null)
                {
                    methodInfo.Invoke(null, new object[] { block });
                }
                else
                {
                    Debug.LogError($"没有找到名为Link{cName}的方法！");
                }
            }
        }
    }
    
    public static void LinkMySay(Block block)
    {
        
        Command command = block.CommandList.Last();
        Flowchart flowchart=block.GetFlowchart();
        MySay say =command as MySay;
        if (say != null)
        {
            if (!say.dial.nextID.Trim().Equals("0"))
            {
                Block targetBlock = flowchart.FindBlock(say.dial.nextID);
                MyCall call = Undo.AddComponent<MyCall>(block.gameObject);
                call.SetTargetBlock(targetBlock);
                call.ParentBlock = block;
                call.ItemId = flowchart.NextItemId();
                call.OnCommandAdded(block);
                block.CommandList.Insert(block.CommandList.Count,call);
            }
            else
            {
                TalkEnd end = Undo.AddComponent<TalkEnd>(block.gameObject);
                end.ParentBlock = block;
                end.ItemId = flowchart.NextItemId();
                end.OnCommandAdded(block);
                block.CommandList.Add(end);
            }
        }
        
    }
    
    public static void LinkMyMenu(Block block)
    {
        
        Command command = block.CommandList.Last();
        Flowchart flowchart=block.GetFlowchart();
        MyMenu myMenu = command as MyMenu;
        if (myMenu!=null)
        {
            foreach (var menuData in myMenu.menuList)
            {
                MyOption option = Undo.AddComponent<MyOption>(block.gameObject);
                Block targetBlock = flowchart.FindBlock(menuData.nextBlockID);
                option.SetStandardText(menuData.text);
                option.SetTargetBlock(targetBlock);
                option.ParentBlock = block;
                option.ItemId = flowchart.NextItemId();
                option.OnCommandAdded(block);
                block.CommandList.Insert(block.CommandList.Count, option);
            }
           
        }
        
    }
    public static void LinkGivePlayerTask(Block block)
    {
        Command command = block.CommandList.Last();
        Flowchart flowchart=block.GetFlowchart();
        TalkEnd end = Undo.AddComponent<TalkEnd>(block.gameObject);
        end.ParentBlock = block;
        end.ItemId = flowchart.NextItemId();
        end.OnCommandAdded(block);
        block.CommandList.Add(end);
       
        
    }
    //TODO:无法再对话时？
}
