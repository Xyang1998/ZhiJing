using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

[CommandInfo("FungusEX","TalkEnd","对话结束后释放玩家")]
public class TalkEnd : Command
{
    public override void OnEnter()
    {
        PlayerController.GetPlayerController().PlayerTalkEnd();
        Continue();
    }
}
