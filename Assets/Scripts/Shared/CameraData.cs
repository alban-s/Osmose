using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraData : MonoBehaviour
{
    public float camX;
    public float camY;

    public void setCamX(float x)
    {
        camX = x;
    }

    public void setCamY(float y)
    {
        camY = y;
    }

    // Start is called before the first frame update
    void Start()
    {
        camX = 3.0f;
        camY = 3.0f;
        
    }
}
