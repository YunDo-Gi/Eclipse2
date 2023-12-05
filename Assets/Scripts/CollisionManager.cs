using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject gameOverCanvas;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            // 자동차와 충돌 시 게임 종료 처리
            GameOver();
        }
    }

    void GameOver()
    {
        // 시간 멈추기
        Time.timeScale = 0f;

        // Game Over Canvas 활성화
        gameOverCanvas.SetActive(true);
    }
}