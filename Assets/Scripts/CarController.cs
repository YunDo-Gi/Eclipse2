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

    private void Start()
    {
        trafficLight = FindObjectOfType<TrafficLight>();
    }

    public void Initialize(float endZ)
    {
        roadEndZ = endZ;
    }

    void Update()
    {
        // 자동차가 도로 끝에 도달하면 이벤트 호출
        // 자동차는 위와 아래 방향 2가지로 존재, roadEndZ 위치가 다르니 상황에 맞게 event 호출
        if ((transform.position.z <= roadEndZ && roadEndZ < 0) || (transform.position.z >= roadEndZ && roadEndZ >= 0))
        {
            OnCarReachedEnd?.Invoke(gameObject);
        }


        //빨간불로 인해 멈춰야 하는 조건
        //원래는 crosswalk에 tag같은 거 달아서 ray로 거리 측정해서 인지하게 하려고 했는데, 잘 안 돼서 일단 차선마다 특정 위치를 멈춤 조건으로 설정함
        bool redFlag;
        if (roadEndZ < 0)
        {
            redFlag = trafficLight.currentColor == TrafficLight.LightColor.Red && (transform.position.z <= 8 && transform.position.z >= 2);
        }
        else
        {
            redFlag = trafficLight.currentColor == TrafficLight.LightColor.Red && (transform.position.z <= 2 && transform.position.z >= -4);
        }
        

        //자동차 움직임 가능 여부 검사
        if (canMove)
        {
            //빨간불 혹은 충돌 물체가 존재하면 stop
            if (redFlag || IsNearObstacle())
            {
                StopCar();
            }
        }
        else
        {
            //빨간불도 아니고 충돌할만한 물체도 없으면 move
            if (!(redFlag || IsNearObstacle()))
            {
                MoveCar();
            }
        }
    }

    void StopCar()
    {
        canMove = false;
    }

    void MoveCar()
    {
        canMove = true;
    }

    bool IsNearObstacle()
    {
        // 자동차의 전방에 Ray를 쏘아 물체와의 충돌을 감지
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, stopDistance))
        {
            // 충돌힐 물체가 있으면 true 반환
            return true;
        }

        // 충돌할 물체가 없으면 false 반환
        return false;
    }
}