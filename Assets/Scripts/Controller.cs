using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ButtonDownListener();
    }

    void ButtonDownListener()
    {
        // X버튼을 누르면
        if(OVRInput.GetDown(OVRInput.Button.Three))
        {
            Debug.Log("X버튼을 눌렀습니다.");
        } 
    }
}
