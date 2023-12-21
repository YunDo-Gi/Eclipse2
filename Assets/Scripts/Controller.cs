using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using OVR;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    private Vector3 previousPosition;
    public float velocity;
    public GameObject Canvas;
    public GameObject GameOverCanvas;
    public GameObject CenterEyeObj;

    OVRScreenFade OFade;
    void Start()
    {
        OFade = CenterEyeObj.transform.GetComponent<OVRScreenFade>();
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
        StartGameListener();
        GameOverListener();
    }

    void ButtonDownListener()
    {
        // X버튼을 누르면
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            // 여기에 음향신호기 넣으면 됨
            Debug.Log("X버튼을 눌렀습니다.");
        }
    }

    void StartGameListener()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (Canvas.activeSelf)
            {
                Canvas.SetActive(false);
                OFade.FadeOut();
            }
        }
    }

    void GameOverListener()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (GameOverCanvas.activeSelf)
            {
                GameOverCanvas.SetActive(false);
                SceneManager.LoadScene("MainScene");
                Time.timeScale = 1f;
            }
        }
    }
}

