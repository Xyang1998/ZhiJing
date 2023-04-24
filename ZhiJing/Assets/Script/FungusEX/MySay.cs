using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("FungusEX","MySay","对话扩展")]
public class MySay : Say
{
    public Dial dial;
    public int npcid;

    public void Init(int _npcid,Dial _dial,GameObject character_)
    {
        character = character_.GetComponent<Character>();
        npcid = _npcid;
        dial = _dial;
    }

    public override void OnEnter()
    {
        SetStandardText(dial.dial);
        StartCoroutine(DialCheck());
        base.OnEnter();
        //Continue();
    }

    public IEnumerator DialCheck()
    {
        TaskSystem.GetTaskSystem().TalkToAction(npcid,dial.ID);
        yield return null;
    }
}
