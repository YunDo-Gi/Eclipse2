using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class OVRFadeinout : MonoBehaviour
{
    public GameObject CenterEyeObj;
    OVRScreenFade OFade;
    void Start()
    {
        OFade = CenterEyeObj.transform.GetComponent<OVRScreenFade>();
    }
 
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            OFade.FadeOut();
        }
        if (Input.GetMouseButtonDown(1))
        {
            OFade.FadeIn();
        }
    }

    public void FadeOut()
    {
        OFade.FadeOut();
    }
}
