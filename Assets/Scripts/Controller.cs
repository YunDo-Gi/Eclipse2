using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{    
    private Vector3 previousPosition;
    public float velocity;

    void Start()
    {
        previousPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
    }

    void Update()
    {
        // Calculate velocity
        Vector3 currentPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        velocity = Vector3.Distance(previousPosition, currentPosition) / Time.deltaTime;
        // Debug.Log("Velocity: " + velocity);

        previousPosition = currentPosition;

        ButtonDownListener();
    }

    void ButtonDownListener()
    {
        // X버튼을 누르면
        if(OVRInput.GetDown(OVRInput.Button.Three))
        {
            // 여기에 음향신호기 넣으면 됨
            Debug.Log("X버튼을 눌렀습니다.");
        } 
    }
}
