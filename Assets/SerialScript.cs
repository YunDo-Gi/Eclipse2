using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;
using System.Threading;

public class SerialScript : MonoBehaviour
{
    // Start is called before the first frame update
    SerialPort sp = new SerialPort();

    void Start()
    {
        sp.PortName = "/dev/cu.usbmodem101 Serial Port (USB)";   // 아두이노 포트이름
        sp.BaudRate = 9600; // 아두이노 보레이트
        sp.DataBits = 8;
        sp.Parity = Parity.None;
        sp.StopBits = StopBits.One;

        sp.Open(); // 포트를 연다. 열면 닫힐 때까지 시리얼 모니터 사용 불가 (여기서 점유하고 있으므로)
    }

    // Update is called once per frame
    void Update()
    {
        switch (Input.inputString)
        {
            case "W":
            case "w":
                Debug.Log("press w");
                sp.WriteLine("w");
                break;

            case "A":
            case "a":
                Debug.Log("press a");
                sp.WriteLine("a");
                break;

            case "S":
            case "s":
                Debug.Log("press s");
                sp.WriteLine("s");
                break;

            case "D":
            case "d":
                Debug.Log("press d");
                sp.WriteLine("d");
                break;
        }
    }

    private void OnApplicationQuit()
    {
        sp.Close();  // 꺼질 때 소켓을 닫아준다
    }
}
