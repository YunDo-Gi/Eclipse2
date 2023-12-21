using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform을 연결할 변수

    void Update()
    {
        // 플레이어의 위치를 따라가도록 설정
        transform.position = player.position + new Vector3(-8, 5, -4);
        transform.LookAt(player.position);
    }
}