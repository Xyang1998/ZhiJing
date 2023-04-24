using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEditor;
using UnityEngine;

public class Chapter0 : TalkBase
{

    public void Start()
    {
        base.Start();
        StartCoroutine(Chapter0Start());
    }

    public  IEnumerator Chapter0Start()
    {
        Player.Lock();
        /*_flowchart = gameObject.AddComponent<Flowchart>();
        Dial dial = new Dial(1,"a","我是被创建的dial",2);
        Debug.Log("Start");
        Block _block =_flowchart.CreateBlock(new Vector2(0, 0));
        _block.BlockName = "0";
        MySay say = Undo.AddComponent<MySay>(_flowchart.FindBlock("0").gameObject);
        say.Init(0,dial);
        _flowchart.FindBlock("0").CommandList.Insert(0,say);
        _flowchart.ExecuteBlock("0");
        BaseTask baseTask = Resources.Load<BaseTask>("quest");
        Debug.Log(baseTask.description);*/
        yield return new WaitForSeconds(3);
        StartTalk();
    }

    public override void StartTalk()
    {
        
        EyesOpen eyesOpen = Undo.AddComponent<EyesOpen>(gameObject);
        Block block = _flowchart.FindBlock("20");
        eyesOpen.ParentBlock = block;
        eyesOpen.ItemId = _flowchart.NextItemId();
        eyesOpen.OnCommandAdded(block);
        block.CommandList.Insert(1,eyesOpen);
        base.StartTalk();

    }
    
}
