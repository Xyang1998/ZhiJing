using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ComputeTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float a = 1.0f;
        object o = new DataTable().Compute($"{a}>=0.5", "");
        Debug.Log(o);
    }

    
}
