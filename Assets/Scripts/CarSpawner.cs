using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs;  // �ڵ��� prefabs �迭
    public Transform[] lanes;  // �� ���� ��ġ�� �� ���� �迭
    public float spawnInterval = 3f;  // �ڵ��� ���� �ֱ�
    public float carSpeed = 5f;  // �ڵ��� �̵� �ӵ�
    public float[] roadEndZArray = { -14f, 18f };  // ���� �� ����
    private int spawnIndex = 0; // spawn ����
    

    private float timer = 0f;

    private List<GameObject> spawnedCars = new List<GameObject>();  // ���� �����ϴ� �ڵ��� List

    //�����̸� �ڵ��� ������ ���߱� ���� trafficLight ������
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

        //list�� �����ϴ� �ڵ������� ���¸� update
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
        // ���̸� �� �浹�� ����
        Ray ray = new Ray(spawnPosition, rotation);
        float rayDistance = 10f; // ������ �ִ� ����

        // �浹 ������ ��ü�� ������ �����ϴ� ����
        RaycastHit hit;

        // Physics.Raycast �Լ��� true�� ��ȯ�ϸ� �浹�� ������ ����
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            // �浹�� ��ü�� �±װ� "Car"�� ��쿡�� true ��ȯ
            return hit.collider.CompareTag("Car");
        }

        // �浹�� �������� ���� ��쿡�� false ��ȯ
        return false;
    }

    void SpawnCar(Vector3 position, float roadEndZ, Quaternion rotation)
    {
        // �����ϰ� �ڵ��� ���� �� ���� ����
        GameObject selectedCarPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];

        // �ڵ��� ���� �� �ʱ�ȭ
        GameObject newCar = Instantiate(selectedCarPrefab, position, rotation);
        Rigidbody carRigidbody = newCar.GetComponent<Rigidbody>();
        carRigidbody.velocity = rotation * Vector3.forward * carSpeed;

        // �ڵ����� CarController ��ũ��Ʈ �߰�
        CarController carController = newCar.AddComponent<CarController>();
        carController.Initialize(roadEndZ);

        // �ڵ����� ���� ���� �����ϸ� ����
        carController.OnCarReachedEnd += DestroyCar;

        // ������ �ڵ����� List�� �߰�
        spawnedCars.Add(newCar);
    }

    void DestroyCar(GameObject car)
    {
        // �ڵ����� ���� ���� �����ϸ� List���� ����
        spawnedCars.Remove(car);
        Destroy(car);
    }

    void UpdateCars()
    {
        // List���� null�� �׸��� ����
        spawnedCars.RemoveAll(car => car == null);

        // �� �ڵ����� canMove ������ Ȯ���Ͽ� velocity�� ����
        foreach (GameObject car in spawnedCars)
        {
            if (car != null)
            {
                CarController carController = car.GetComponent<CarController>();

                if (carController != null)
                {
                    Rigidbody carRigidbody = car.GetComponent<Rigidbody>();

                    // canMove ���ο� ���� �ӵ� ����
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
