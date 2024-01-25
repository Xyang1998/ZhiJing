using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using Cysharp.Threading.Tasks;
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
            SystemMediator.Instance.taskSystem.GetCharacter(Name);
        }

        return _character;

    }


    public override void Start()
    {
        Load();
        SystemMediator.Instance.eventSystem.saveaction += Save;
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
        CreateCharacter();
    }

    public void Save() //保存
    {
        
    }

    public void CreateCharacter()
    {
        _character=Undo.AddComponent<Character>(gameObject);
        _character.SetStandardText(Name);
        CreateFlowChart().Forget();

    }

    public async UniTaskVoid CreateFlowChart() //字符串解析 
    {
        string path = Application.streamingAssetsPath + "/Chapter" + Chapter;
        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        FileInfo[] files = directoryInfo.GetFiles("*");
        List<string> match=new List<string>(); 
        foreach (var file in files)
        {
            if (file.Name.StartsWith(Name) && !file.Name.EndsWith(".meta"))
            {
                match.Add(file.Name);
            }
        }
        
        _flowchart = gameObject.AddComponent<Flowchart>();
        ExcelLoader<Dial> loader = new ExcelLoader<Dial>();
        foreach (var file in match)
        {
            Block block = _flowchart.CreateBlock(new Vector2(0, 0));
            string blockname = file.Split("_")[1].Split(".xls")[0];
            block.BlockName = blockname;
            loader.LoadFromPath(path+"/"+file);
            List<Dial> datas = loader.list;
            for (int index = 0; index < datas.Count; index++)
            {
                CreateCommand.CreateCommandByTypeName(this,_flowchart,block,datas[index]);
            }

        }
        Block[] blocks = _flowchart.GetComponents<Block>();
        LinkCommand.LinkBlocksInFlowChart(blocks);
        await UniTask.Yield();
    }
}




