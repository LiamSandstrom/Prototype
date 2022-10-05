using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lerp : MonoBehaviour
{
    public Transform playertransform;
    public int interpolationFramesCount = 45;
    int elapsedFrames = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float interpolationRatio = (float)elapsedFrames/interpolationFramesCount;
        Vector3 transformthis = Vector3.Lerp(Vector3.up,Vector3.forward, interpolationRatio);
        elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);
        transform.position = transformthis;
    }
}
