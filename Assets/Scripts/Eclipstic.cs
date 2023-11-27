using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Eclipstic : MonoBehaviour
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct Test
    {
        [MarshalAs(UnmanagedType.I4)]
        public int material;
        [MarshalAs(UnmanagedType.I4)]
        public int speed;
    }

    public Test test;

    private byte[] getMashalData()
    {
        int bufferSize = Marshal.SizeOf(test);
        IntPtr buffer = Marshal.AllocHGlobal(bufferSize);
        Marshal.StructureToPtr(test, buffer, false);

        byte[] data = new byte[bufferSize];
        Marshal.Copy(buffer, data, 0, bufferSize);
        Marshal.FreeHGlobal(buffer);

        return data;
    }

    public SerialController serialController;
    // Start is called before the first frame update
    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        test.material = 0;
        test.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Sending A");
            serialController.SendSerialMessage("A");
            test.material = 1;
            test.speed = 0;
            Debug.Log(getMashalData()[3]);
            serialController.SendStruct(getMashalData());
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Sending Z");
            serialController.SendSerialMessage("Z");
            test.material = 0;
            test.speed = 1;
            serialController.SendStruct(getMashalData());
        }

        //Test recv = (Test)serialController.ReadStruct();

        //Debug.Log("material : " + recv.material + " , speed : " + recv.speed);

        string message = serialController.ReadSerialMessage();

        if (message == null)
            return;

        // Check if the message is plain data or a connect/disconnect event.
        if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
            Debug.Log("Connection established");
        else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
            Debug.Log("Connection attempt failed or disconnection detected");
        else
            Debug.Log("Message arrived: " + message);

        return;

    }
}
