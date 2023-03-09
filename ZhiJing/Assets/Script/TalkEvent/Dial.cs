using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Dial
{
    public int ID;
    public string dial;
    public int NextID;
    public string extra;

    public Dial(int _id,string _dial,int _nextid,string _extra)
    {
        ID = _id;
        dial = _dial;
        NextID = _nextid;
        extra = _extra;
    }

}
