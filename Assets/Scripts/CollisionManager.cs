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
            // �ڵ����� �浹 �� ���� ���� ó��
            GameOver();
        }
    }

    void GameOver()
    {
        // �ð� ���߱�
        Time.timeScale = 0f;

        // Game Over Canvas Ȱ��ȭ
        gameOverCanvas.SetActive(true);
    }
}