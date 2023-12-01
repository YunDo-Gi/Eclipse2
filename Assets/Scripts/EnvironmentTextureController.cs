using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentTextureController : MonoBehaviour
{
    public String texture;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateCoroutine());
    }

    private IEnumerator UpdateCoroutine()
    {
        while (true)
        {
            if (texture == "asphalt")
            {
                Debug.Log("asphalt");
            }
            else if (texture == "crosswalk")
            {
                Debug.Log("crosswalk");
            }
            else if (texture == "sidewalk")
            {
                Debug.Log("sidewalk");
            } else if (texture == "obstacle")
            {
                Debug.Log("obstacle");
            } else if (texture == "whitecrosswalk")
            {
                Debug.Log("whitecrosswalk");
            } else if (texture == "braille")
            {
                Debug.Log("braille");
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
}
