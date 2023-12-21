using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs;  // 자동차 prefabs 배열
    public Transform[] lanes;  // 차 생성 위치가 될 차선 배열
    public float spawnInterval = 3f;  // 자동차 생성 주기
    public float carSpeed = 5f;  // 자동차 이동 속도
    public float[] roadEndZArray = { -14f, 18f };  // 도로 끝 지점
    private int spawnIndex = 0; // spawn 지점
    

    private float timer = 0f;

    private List<GameObject> spawnedCars = new List<GameObject>();  // 현재 존재하는 자동차 List

    //적색이면 자동차 생성을 멈추기 위해 trafficLight 가져옴
    private TrafficLight trafficLight;

    private void Start()
    {
        trafficLight = FindObjectOfType<TrafficLight>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            
            if (trafficLight.currentColor != TrafficLight.LightColor.Red)
            {
                SpawnCars();
                spawnIndex = (spawnIndex + 1) % 2;
            }   
            
            timer = 0f;
        }

        //list로 관리하는 자동차들의 상태를 update
        UpdateCars();
    }

    void SpawnCars()
    {
        if (spawnIndex == 0)
        {
            if (!IsCollisionDetected(lanes[0].position + new Vector3(1f, 0.1f, 5f), Quaternion.Euler(0f, 180f, 0f) * Vector3.forward))
            {
                SpawnCar(lanes[0].position + new Vector3(1f, 0.1f, 0f), roadEndZArray[0], Quaternion.Euler(0f, 180f, 0f));
            }
            if (!IsCollisionDetected(lanes[1].position + new Vector3(1f, 0.1f, -5f), Quaternion.Euler(0f, 0f, 0f) * Vector3.forward))
            {
                SpawnCar(lanes[1].position + new Vector3(1f, 0.1f, 0f), roadEndZArray[1], Quaternion.Euler(0f, 0f, 0f));
            }
        }
        else
        {
            if (!IsCollisionDetected(lanes[0].position + new Vector3(-1f, 0.1f, 5f), Quaternion.Euler(0f, 180f, 0f) * Vector3.forward))
            {
                SpawnCar(lanes[0].position + new Vector3(-1f, 0.1f, 0f), roadEndZArray[0], Quaternion.Euler(0f, 180f, 0f));
            }
            if (!IsCollisionDetected(lanes[1].position + new Vector3(-1f, 0.1f, -5f), Quaternion.Euler(0f, 0f, 0f) * Vector3.forward))
            {
                SpawnCar(lanes[1].position + new Vector3(-1f, 0.1f, 0f), roadEndZArray[1], Quaternion.Euler(0f, 0f, 0f));
            }
        }
    }

    bool IsCollisionDetected(Vector3 spawnPosition, Vector3 rotation)
    {
        // 레이를 쏴 충돌을 감지
        Ray ray = new Ray(spawnPosition, rotation);
        float rayDistance = 10f; // 레이의 최대 길이

        // 충돌 감지된 물체의 정보를 저장하는 변수
        RaycastHit hit;

        // Physics.Raycast 함수가 true를 반환하면 충돌이 감지된 것임
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            // 충돌된 물체의 태그가 "Car"인 경우에는 true 반환
            return hit.collider.CompareTag("Car");
        }

        // 충돌이 감지되지 않은 경우에는 false 반환
        return false;
    }

    void SpawnCar(Vector3 position, float roadEndZ, Quaternion rotation)
    {
        // 랜덤하게 자동차 종류 및 차선 선택
        GameObject selectedCarPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];

        // 자동차 생성 및 초기화
        GameObject newCar = Instantiate(selectedCarPrefab, position, rotation);
        Rigidbody carRigidbody = newCar.GetComponent<Rigidbody>();
        carRigidbody.velocity = rotation * Vector3.forward * carSpeed;

        // 자동차에 CarController 스크립트 추가
        CarController carController = newCar.AddComponent<CarController>();
        carController.Initialize(roadEndZ);

        // 자동차가 도로 끝에 도달하면 제거
        carController.OnCarReachedEnd += DestroyCar;

        // 생성된 자동차를 List에 추가
        spawnedCars.Add(newCar);
    }

    void DestroyCar(GameObject car)
    {
        // 자동차가 도로 끝에 도달하면 List에서 제거
        spawnedCars.Remove(car);
        Destroy(car);
    }

    void UpdateCars()
    {
        // List에서 null인 항목을 제거
        spawnedCars.RemoveAll(car => car == null);

        // 각 자동차의 canMove 변수를 확인하여 velocity를 설정
        foreach (GameObject car in spawnedCars)
        {
            if (car != null)
            {
                CarController carController = car.GetComponent<CarController>();

                if (carController != null)
                {
                    Rigidbody carRigidbody = car.GetComponent<Rigidbody>();

                    // canMove 여부에 따라 속도 조절
                    if (!carController.canMove)
                    {
                        if (carRigidbody.velocity.magnitude > 0.5f)
                        {
                            carRigidbody.velocity -= carRigidbody.transform.forward * Time.deltaTime * 12;
                        }
                        else
                        {
                            carRigidbody.velocity = Vector3.zero;
                        }
                    }
                    else
                    {
                        if (carRigidbody.velocity.magnitude < carSpeed)
                        {
                            carRigidbody.velocity += carRigidbody.transform.forward * Time.deltaTime * 5;
                        }
                        else
                        {
                            carRigidbody.velocity = carRigidbody.transform.forward * carSpeed;
                        }
                    }
                }
            }
        }
    }
}
