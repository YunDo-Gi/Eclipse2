using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //���� �� ���� ���θ� check�ϴ� ������ �Լ�
    public delegate void CarReachedEnd(GameObject car);
    public event CarReachedEnd OnCarReachedEnd;

    private float roadEndZ;

    //��ȣ��� ��ü �浹 ���� ó���� ���� �ڵ�
    public float stopDistance = 4f; //��ü���� �Ÿ��� �Ӱ���

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
        // �ڵ����� ���� ���� �����ϸ� �̺�Ʈ ȣ��
        // �ڵ����� ���� �Ʒ� ���� 2������ ����, roadEndZ ��ġ�� �ٸ��� ��Ȳ�� �°� event ȣ��
        if ((transform.position.z <= roadEndZ && roadEndZ < 0) || (transform.position.z >= roadEndZ && roadEndZ >= 0))
        {
            OnCarReachedEnd?.Invoke(gameObject);
        }


        //�����ҷ� ���� ����� �ϴ� ����
        //������ crosswalk�� tag���� �� �޾Ƽ� ray�� �Ÿ� �����ؼ� �����ϰ� �Ϸ��� �ߴµ�, �� �� �ż� �ϴ� �������� Ư�� ��ġ�� ���� �������� ������
        bool redFlag;
        if (roadEndZ < 0)
        {
            redFlag = trafficLight.currentColor == TrafficLight.LightColor.Red && (transform.position.z <= 8 && transform.position.z >= 2);
        }
        else
        {
            redFlag = trafficLight.currentColor == TrafficLight.LightColor.Red && (transform.position.z <= 2 && transform.position.z >= -4);
        }
        

        //�ڵ��� ������ ���� ���� �˻�
        if (canMove)
        {
            //������ Ȥ�� �浹 ��ü�� �����ϸ� stop
            if (redFlag || IsNearObstacle())
            {
                StopCar();
            }
        }
        else
        {
            //�����ҵ� �ƴϰ� �浹�Ҹ��� ��ü�� ������ move
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
        // �ڵ����� ���濡 Ray�� ��� ��ü���� �浹�� ����
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, stopDistance))
        {
            // �浹�� ��ü�� ������ true ��ȯ
            return true;
        }

        // �浹�� ��ü�� ������ false ��ȯ
        return false;
    }
}