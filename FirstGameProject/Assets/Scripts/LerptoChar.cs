using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerptoChar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]  Transform Playertolerp;
    public int interpolationFramesCount = 45; // Number of frames to completely interpolate between the 2 positions
    int elapsedFrames = 0;

    // Update is called once per frame
    void Update()
    {
        float interpolationRatio = (float)elapsedFrames/interpolationFramesCount;
        Vector3 transformthis = Vector3.Lerp(transform.position, Playertolerp.position, interpolationRatio);
        elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);
        transform.position = transformthis;
    }
}
