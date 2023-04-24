using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Dial
{
    public int ID;
    public string speaker;
    public string dial;
    public string NextID;
    //public string extra;

    public Dial(int _id,string _speaker,string _dial,string _nextid)
    {
        ID = _id;
        speaker = _speaker;
        dial = _dial;
        NextID = _nextid;
        //extra = _extra;
    }

}
