using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : ISystem
{
    public bool Onenable=true;
    public GameObject followtarget;
    public float MaxDistance = 5;
    private float CurDistance;
    public float MaxRight;
    public float MaxLeft;
    private Vector3 newposition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Onenable)
        {
            MoveCamera();
        }
    }
    void MoveCamera()
    {
        if (!(transform.position.x <= MaxRight && transform.position.x >= MaxLeft)) return;
        CurDistance = followtarget.transform.position.x - transform.position.x;
        if (!(Mathf.Abs(CurDistance) > MaxDistance)) return;
        newposition = transform.position;
        newposition.x = CurDistance > 0
            ? followtarget.transform.position.x - MaxDistance
            : followtarget.transform.position.x + MaxDistance;
        newposition.x = Mathf.Clamp(newposition.x, MaxLeft, MaxRight);
        transform.position = newposition;
  
    }
}
