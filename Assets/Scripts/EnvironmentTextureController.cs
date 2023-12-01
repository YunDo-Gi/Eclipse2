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
            yield return new WaitForEndOfFrame();
        }
    }

    public void OnAsphalt()
    {
        texture = "asphalt";
        Debug.Log("On asphalt");
    }
}
