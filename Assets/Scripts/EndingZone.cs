using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class EndingZone : MonoBehaviour
{
    // 플레이어 종료 지점 위치
    public Transform endingStopPosition1;
    public Transform endingStopPosition2;

    // 플레이어 객체
    public Transform playerTransform;

    public GameObject CenterEyeObj;
    OVRScreenFade OFade;

    void Start()
    {
        OFade = CenterEyeObj.transform.GetComponent<OVRScreenFade>();
    }

    void Update()
    {
        if (playerTransform == null)
        {
            Debug.LogWarning("Player transform not assigned in EndingZone script.");
            return;
        }

        // 각 종료 지점 간의 거리를 계산
        float distanceToStop1 = Vector3.Distance(playerTransform.position, endingStopPosition1.position);
        float distanceToStop2 = Vector3.Distance(playerTransform.position, endingStopPosition2.position);

        // 만약 두 지점 중 하나에 도달하면 게임 종료
        if (distanceToStop1 < 2.0f || distanceToStop2 < 2.0f) // 원하는 거리로 조절
        {
            GameOver();
        }
    }

    void GameOver()
    {
        OFade.FadeOut();
    }
}