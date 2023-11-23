using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public Material[] lightMaterials;

    //각각의 신호등 지속시간
    public float greenDuration = 10f;
    public float redDuration = 7f;
    public float yellowDuration = 2f;

    private Renderer trafficLightRenderer;
    private float timer = 0f;



    //현재 신호등 색(도로기준)
    public enum LightColor
    {
        Red,
        Yellow,
        Green
    }

    //시작값은 green으로 설정
    public LightColor currentColor = LightColor.Green;
    int isRed = 0, isYellow = 0, isGreen = 1;

    void Start()
    {
        trafficLightRenderer = GetComponent<Renderer>();
        SetMaterials();
    }

    void SetMaterials()
    {
        //currnetColor에 맞춰서 색 rendering
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
        timer += Time.deltaTime;

        //적색 -> 초록 -> 황색 -> 적색으로 변화
        if (currentColor == LightColor.Red && timer >= redDuration)
        {
            currentColor = LightColor.Green;
            SetMaterials();
            timer = 0f;
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
}
