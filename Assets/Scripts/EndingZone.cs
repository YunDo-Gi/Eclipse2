using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class EndingZone : MonoBehaviour
{
    // �÷��̾� ���� ���� ��ġ
    public Transform endingStopPosition1;
    public Transform endingStopPosition2;

    // �÷��̾� ��ü
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

        // �� ���� ���� ���� �Ÿ��� ���
        float distanceToStop1 = Vector3.Distance(playerTransform.position, endingStopPosition1.position);
        float distanceToStop2 = Vector3.Distance(playerTransform.position, endingStopPosition2.position);

        // ���� �� ���� �� �ϳ��� �����ϸ� ���� ����
        if (distanceToStop1 < 2.0f || distanceToStop2 < 2.0f) // ���ϴ� �Ÿ��� ����
        {
            GameOver();
        }
    }

    void GameOver()
    {
        OFade.FadeOut();
    }
}