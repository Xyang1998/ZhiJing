using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
[CommandInfo("FungusEX","TalkStart","对话开始时冻结玩家")]
public class TalkStart : Command
{
    public override void OnEnter()
    {
        SystemMediator.Instance.playerController.PlayerTalkEnd();
        Continue();
    }
}
