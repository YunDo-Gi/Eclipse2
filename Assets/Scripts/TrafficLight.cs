using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public Material[] lightMaterials;

    // 각각의 신호등 지속시간 (차 기준)
    public float greenDuration = 12f;
    public float redDuration = 20f;
    public float yellowDuration = 3f;

    private Renderer trafficLightRenderer;
    private float timer = 0f;

    private AudioSource audioRedLight1;
    private AudioSource audioGreenLight;
    private AudioSource audioRedLight2;

    // 버튼이 눌렸을 때 true로 바꿔줌 
    public bool isButton = false; 

    // 현재 신호등 색 (도로기준) 
    public enum LightColor
    {
        Red,
        Yellow,
        Green
    }

    // 시작값을 green으로 설정 
    public LightColor currentColor = LightColor.Green;
    int isRed = 0, isYellow = 0, isGreen = 1;
    bool redPlay = false, greenPlay1 = false, greenPlay2 = false;

    void Start()
    {
        trafficLightRenderer = GetComponent<Renderer>();
        SetMaterials();

        AudioSource[] audioSources = GetComponents<AudioSource>();
        audioRedLight1 = audioSources[0];
        audioGreenLight = audioSources[1];
        audioRedLight2 = audioSources[2];
    }

    void SetMaterials()
    {
        //currnetColor에 맞춰서  rendering
        switch (currentColor)
        {
            case LightColor.Red:
                isRed = 1; isYellow = 0; isGreen = 0;
                break;
            case LightColor.Yellow:
                isRed = 0; isYellow = 1; isGreen = 0;
                break;
            case LightColor.Green:
                isRed = 0; isYellow = 0; isGreen = 1;
                break;

        }
        trafficLightRenderer.materials = new Material[] {
            lightMaterials[0],
            lightMaterials[3 + isRed],
            lightMaterials[5 + isYellow],
            lightMaterials[1 + isGreen],
            lightMaterials[3 + isGreen],
            lightMaterials[5 + isYellow],
            lightMaterials[1 + isRed]
        };
    }

    void Update()
    {
        ButtonDownListener();

        timer += Time.deltaTime;

        SoundPlayer();

        // 자동차 기준 : 적색 -> 초록색 -> 황색 -> 적색으로 변화
        // 사람 기준: 녹색 -> 빨간색
        if (currentColor == LightColor.Red && timer >= redDuration)
        {
            currentColor = LightColor.Green;
            SetMaterials();
            timer = 0f;
            if (isButton)
            {
                isButton = false;
                redPlay = false; greenPlay1 = false; greenPlay2 = false;
            }
        }
        else if (currentColor == LightColor.Green && timer >= greenDuration)
        {
            currentColor = LightColor.Yellow;
            SetMaterials();
            timer = 0f;
        }
        else if (currentColor == LightColor.Yellow && timer >= yellowDuration)
        {
            currentColor = LightColor.Red;
            SetMaterials();
            timer = 0f;
        }
    }

    void SoundPlayer()
    {
        Debug.Log(timer);
        if (isButton)
        {
            if ((currentColor == LightColor.Green || currentColor == LightColor.Yellow) && !redPlay)
            {
                audioGreenLight.Play();
                redPlay = true;
            }
            if (currentColor == LightColor.Red && timer <= 10 && !greenPlay1)
            {
                audioGreenLight.Stop();
                audioRedLight1.Play();
                greenPlay1 = true;
            }
            if (currentColor == LightColor.Red && timer > 10 && !greenPlay2)
            {
                audioGreenLight.Stop();
                audioRedLight1.Play();
                greenPlay2 = true;
            }
        }
    }

    void ButtonDownListener()
    {
        //test code
        if (Input.GetKeyDown(KeyCode.C))
        {
            isButton = true;
        }
        
        /*
        // X버튼을 누르면
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            isButton = true;
            // 여기에 음향신호기 넣으면 됨
            Debug.Log("X버튼을 눌렀습니다.");
        }
        */
    }
}
