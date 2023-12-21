using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //도로 끝 도달 여부를 check하는 뱐수와 함수
    public delegate void CarReachedEnd(GameObject car);
    public event CarReachedEnd OnCarReachedEnd;

    private float roadEndZ;

    //신호등과 물체 충돌 방지 처리를 위한 코드
    public float stopDistance = 4f; //물체와의 거리의 임계점

    public bool canMove = true;
    private TrafficLight trafficLight;

    private AudioSource audioSource; // 오디오 소스 추가

    private void Start()
    {
        trafficLight = FindObjectOfType<TrafficLight>();

        // 오디오 소스 컴포넌트 초기화
        audioSource = GetComponent<AudioSource>();
    }

    public void Initialize(float endZ)
    {
        roadEndZ = endZ;
    }

    void Update()
    {
        // 자동차가 도로 끝에 도달하면 이벤트 호출
        if ((transform.position.z <= roadEndZ && roadEndZ < 0) || (transform.position.z >= roadEndZ && roadEndZ >= 0))
        {
            OnCarReachedEnd?.Invoke(gameObject);
        }

        // 빨간불로 인해 멈춰야 하는 조건
        bool redFlag;
        if (roadEndZ < 0)
        {
            redFlag = trafficLight.currentColor == TrafficLight.LightColor.Red && (transform.position.z <= 8 && transform.position.z >= 2);
        }
        else
        {
            redFlag = trafficLight.currentColor == TrafficLight.LightColor.Red && (transform.position.z <= 2 && transform.position.z >= -4);
        }

        // 자동차 움직임 가능 여부 검사
        if (canMove)
        {
            // 빨간불 혹은 충돌 물체가 존재하면 stop
            if (redFlag || IsNearObstacle())
            {
                StopCar();
            }

            // 사람과의 충돌 시 소리 재생 로직 추가
            else if (IsNearObstacle("Player"))
            {
                audioSource.Play();
            }
        }
        else
        {
            // 빨간불도 아니고 충돌할만한 물체도 없으면 move
            if (!(redFlag || IsNearObstacle()))
            {
                MoveCar();
            }
        }
    }

    bool IsNearObstacle(string tag = null)
    {
        // 자동차의 전방에 Ray를 쏘아 물체와의 충돌을 감지
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, stopDistance))
        {
            //  태그가 제공되지 않았거나 태그가 일치하면 true 반환
            if (tag == null || hit.collider.CompareTag(tag))
            {
                return true;
            }
        }

        // 충돌할 물체가 없거나 해당 태그가 아니면 false 반환
        return false;
    }


    void StopCar()
    {
        canMove = false;
    }

    void MoveCar()
    {
        canMove = true;
    }
}