using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform�� ������ ����

    void Update()
    {
        // �÷��̾��� ��ġ�� ���󰡵��� ����
        transform.position = player.position + new Vector3(-8, 5, -4);
        transform.LookAt(player.position);
    }
}