using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Herkenningspunt : MonoBehaviour
{
    private GameObject ARVideo;

    // Start is called before the first frame update
    void Start()
    {
        ARVideo = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
        {
            Invoke("PlayVideo", 2.5f);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
        {
            ARVideo.GetComponent<VideoPlayer>().isLooping = false;
            ARVideo.GetComponent<VideoPlayer>().Stop();
            ARVideo.SetActive(false);
        }
    }

    private void PlayVideo()
    {
        ARVideo.SetActive(true);
        ARVideo.GetComponent<VideoPlayer>().Play();
        ARVideo.GetComponent<VideoPlayer>().isLooping = true;
    }
}
