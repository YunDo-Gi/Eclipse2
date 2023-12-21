using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentTextureController : MonoBehaviour
{
    public String texture;
    public int texint;
    public int obs;

    // Start is called before the first frame update
    void Start()
    {
        texint = 0;
        obs = 0;
        StartCoroutine(UpdateCoroutine());
    }

    private IEnumerator UpdateCoroutine()
    {
        while (true)
        {
            if (texture == "asphalt")
            {
                texint = 1;
                obs = 0;
                //Debug.Log("asphalt");
            }
            else if (texture == "crosswalk")
            {
                texint = 1;
                obs = 0;
                //Debug.Log("crosswalk");
            }
            else if (texture == "sidewalk")
            {
                texint = 0;
                obs = 0;
                //Debug.Log("sidewalk");
            } else if (texture == "obstacle")
            {
                obs = 1;
                //Debug.Log("obstacle");
            } else if (texture == "whitecrosswalk")
            {
                texint = 2;
                obs = 0;
                //Debug.Log("whitecrosswalk");
            } else if (texture == "braille")
            {
                texint = 3;
                obs = 0;
                //Debug.Log("braille");
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void OnAsphalt()
    {
        if (texture != "asphalt")
        {
            texture = "asphalt";
            Debug.Log("On asphalt");
        }
    }

    public void Oncrosswalk()
    {
        if (texture != "crosswalk")
        {
            texture = "crosswalk";
            Debug.Log("On crosswalk");
        }
    }

    public void Onwhitecrosswalk()
    {
        if (texture != "whitecrosswalk")
        {
            texture = "whitecrosswalk";
            Debug.Log("On whitecrosswalk");
        }
    }

    public void Onsidewalk()
    {
        if (texture != "sidewalk")
        {
            texture = "sidewalk";
            Debug.Log("On sidewalk");
        }
    }

    public void Onobstacle()
    {
        if (texture != "obstacle")
        {
            texture = "obstacle";
            Debug.Log("On obstacle");
        }
    }

    public void OnBraille()
    {
        if (texture != "braille")
        {
            texture = "braille";
            Debug.Log("On braille");
        }
    }

    public void TrafficClick()
    {
        Debug.Log("TrafficClick");
    }
}
