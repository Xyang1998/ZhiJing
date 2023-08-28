using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using Fungus;
using Fungus.EditorUtils;
using Ideafixxxer.CsvParser;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Menu = Fungus.Menu;

[Serializable]
public class NPCState
{
    public float a;
    public float b;

    NPCState(float a_,float b_)
    {
        a = a_;
        b = b_;
    }
    

}

[Serializable]
public class TalkBase : NPC
{
    public NPCState state;
    public string Name;
    public Flowchart _flowchart;
    public string talkmessage;
    public string DailogPath; //对话文本
    public int StartID; //对话开始ID
    public int Chapter;

    private Character _character;

    public Character GetCharacter()
    {
        if (!_character)
        {
            TaskSystem.GetTaskSystem().GetCharacter(Name);
        }

        return _character;
    }


    public virtual void Start()
    {
        Load();
        SystemMediator.Instance.eventSystem.saveaction += Save;
        TaskSystem.GetTaskSystem().AddNPC(this);
    }

    public void SetIDAndTalk(int id)
    {
        StartID = id;
        StartTalk();
    }
    public virtual void StartTalk()
    {
        _flowchart.ExecuteBlock(StartID.ToString());
    }

    public virtual bool PlayerItemsCheck(List<int> list) //检查玩家是否有物品
    {
        return SystemMediator.Instance.playerController.ItemsCheck(list);
    }

    public virtual void PlayerItemsUse(List<int> list) //检查玩家是否有并使用物品
    {
        SystemMediator.Instance.playerController.ItemsUse(list);
    }

    public void Load() //读取，下次对话ID，位置，好感度等
    {
        StartCoroutine(CreateCharacter());
    }

    public void Save() //保存
    {
        
    }

    public IEnumerator CreateCharacter()
    {
        _character=Undo.AddComponent<Character>(gameObject);
        _character.SetStandardText(Name);
        StartCoroutine(CreateFlowChart());
        yield return null;
    }

    public virtual IEnumerator CreateFlowChart() //字符串解析
    {
        List<MenuData> Menulist = new List<MenuData>();
        List<FinalDial> finalDials = new List<FinalDial>();
        string path = Application.streamingAssetsPath + "/Chapter" + Chapter;
        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        // Debug.Log(Application.streamingAssetsPath+"/Chapter"+Chapter);
        FileInfo[] files = directoryInfo.GetFiles("*");
        List<string> match=new List<string>(); //保存当前符合该npc的对话文件
        foreach (var file in files)
        {
            if (file.Name.StartsWith(Name) && !file.Name.EndsWith(".meta"))
            {
                match.Add(file.Name);
            }
        }
        
        _flowchart = gameObject.AddComponent<Flowchart>();
        foreach (var file in match)
        {
            bool hasOthers = false; //是否连接其他block（是否有后续对话）
            Block block = _flowchart.CreateBlock(new Vector2(0, 0));
            string blockname = file.Split("_")[1].Split(".json")[0];
            block.BlockName = blockname;
            StreamReader reader = new StreamReader(path + "/" + file);
            string jsondata = reader.ReadToEnd();
            Debug.Log(jsondata);
            reader.Close();
            Data data = JsonUtility.FromJson<Data>(jsondata);
            foreach (var dial in data.Dials)
            {
                Debug.Log(dial.speaker);
                Debug.Log(dial.dial);
                if(dial.speaker=="选项")
                {
                    hasOthers = true;
                    MySay say = Undo.AddComponent<MySay>(gameObject);
                    string text = dial.dial.Split("#TextEnd")[0];
                    GameObject character = TaskSystem.GetTaskSystem().GetCharacter(dial.speaker); //待优化
                    Dial MenuDial = new Dial(dial.ID, dial.speaker, text, "0");
                    say.Init(ID, MenuDial,character); //待优化
                    block.GetFlowchart().AddSelectedCommand(say);  
                    say.ParentBlock = block;
                    say.ItemId = _flowchart.NextItemId();
                    say.OnCommandAdded(block);
                    block.CommandList.Add(say);
                    //Debug.Log("打印选项");
                    //Debug.Log(text);
                    string[] menus = dial.dial.Split("#TextEnd")[1].Split("#Menu");
                    string[] jumps = dial.NextID.Split("#");
                    for (int i = 0; i < menus.Length - 1; i++)
                    {
                        Debug.Log(menus[i]);
                        Debug.Log(jumps[i]);
                        MenuData myMenuData = new MenuData(blockname,jumps[i],menus[i]);
                        Menulist.Add(myMenuData);
                    }

                }
                else if (dial.speaker == "任务")
                {
                    GivePlayerTask givePlayerTask = Undo.AddComponent<GivePlayerTask>(gameObject);
                    givePlayerTask.Init(dial.dial);
                    block.GetFlowchart().AddSelectedCommand(givePlayerTask);  
                    givePlayerTask.ParentBlock = block;
                    givePlayerTask.ItemId = _flowchart.NextItemId();
                    givePlayerTask.OnCommandAdded(block);
                    block.CommandList.Add(givePlayerTask);
                }
                else
                {
                    MySay say = Undo.AddComponent<MySay>(gameObject);
                    GameObject character = TaskSystem.GetTaskSystem().GetCharacter(dial.speaker); //待优化
                    say.Init(ID, dial,character); //待优化
                    block.GetFlowchart().AddSelectedCommand(say);  
                    say.ParentBlock = block;
                    say.ItemId = _flowchart.NextItemId();
                    say.OnCommandAdded(block);
                    block.CommandList.Add(say);

                }
                

            }

            if (!hasOthers)
            {
                int max = block.CommandList.Count - 1;
                MySay finalsay = block.CommandList[max] as MySay;
                while (finalsay == null)
                {
                    max--;
                    if (max < 0) break;
                    finalsay=block.CommandList[max] as MySay;
                }
                
                if (finalsay.dial.NextID.Trim() != "0")
                {
                    FinalDial finalDial = new FinalDial(blockname,
                        finalsay.dial);
                    finalDials.Add(finalDial);
                }
                else
                {
                    TalkEnd end = Undo.AddComponent<TalkEnd>(gameObject);
                    end.ParentBlock = block;
                    end.ItemId = _flowchart.NextItemId();
                    end.OnCommandAdded(block);
                    block.CommandList.Add(end);
                }

            }


        }

        foreach (var menu in Menulist)
        {
            MyMenu Fmenu = Undo.AddComponent<MyMenu>(gameObject);
            Block block = _flowchart.FindBlock(menu.blockID);
            Block targetBlock = _flowchart.FindBlock(menu.nextBlockID);
            Fmenu.SetStandardText(menu.text);
            Fmenu.SetTargetBlock(targetBlock);
            Fmenu.ParentBlock = block;
            Fmenu.ItemId = _flowchart.NextItemId();
            Fmenu.OnCommandAdded(block);
            //block.CommandList.RemoveAt(block.CommandList.Count-1);
            block.CommandList.Insert(block.CommandList.Count,Fmenu);

        }

        foreach (var finaldial in finalDials)
        {
            Block block = _flowchart.FindBlock(finaldial.blockID);
            Block targetBlock = _flowchart.FindBlock(finaldial.dial.NextID);
            MyCall call = Undo.AddComponent<MyCall>(gameObject);
            call.SetTargetBlock(targetBlock);
            call.ParentBlock = block;
            call.ItemId = _flowchart.NextItemId();
            call.OnCommandAdded(block);
            block.CommandList.Insert(block.CommandList.Count,call);
            
        }
        
        yield return null;


    }
    
    
}

[Serializable]
public class Data
{
    public Dial[] Dials;
}

[Serializable]
public class MenuData
{
    public string blockID; //附着Block ID
    public string nextBlockID; //跳转Block ID
    public string text; //选项文本

    public MenuData(string id1,string id2,string text_)
    {
        blockID = id1;
        nextBlockID = id2;
        text = text_;

    }
}

[Serializable]
public class FinalDial
{
    public string blockID;
    public Dial dial;

    public FinalDial(string id, Dial dial_)
    {
        blockID = id;
        dial = dial_;
    }
}