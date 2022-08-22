using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public RawImage videoImage;
    public VideoPlayer videoScene;
    // Start is called before the first frame update
    void Start()
    {
        videoScene.loopPointReached += Check;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Check(UnityEngine.Video.VideoPlayer vp){
        VideoIsOver();
        Debug.Log("Video is Over");
    }

    void VideoIsOver(){

    }
}
