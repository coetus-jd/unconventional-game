using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;

public class HandDetector : MonoBehaviour
{

    WebCamTexture _webCamTexture;

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        _webCamTexture = new WebCamTexture(devices[0].name);
        _webCamTexture.Play();
    }


    void Update()
    {
        GetComponent<Renderer>().material.mainTexture = _webCamTexture;
    }

}
