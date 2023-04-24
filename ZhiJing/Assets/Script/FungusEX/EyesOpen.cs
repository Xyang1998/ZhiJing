using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.UI;

[CommandInfo("FungusEX","EyesOpen","睁开眼睛特效（暂用）")]
public class EyesOpen : Command
{
    public override void OnEnter()
    {
        base.OnEnter();
        StartCoroutine(eyesOpen());
        Continue();
    }

    public IEnumerator eyesOpen()
    {
        Image image = GameObject.Find("EyesOpen").GetComponent<Image>();
        double second = 2.0;
        while (image.color.a>0)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, (float)(image.color.a-(Time.deltaTime/second)));
            yield return null;
        }

    }
}
