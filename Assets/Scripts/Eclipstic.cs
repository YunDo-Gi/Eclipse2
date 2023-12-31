using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

public class Eclipstic : MonoBehaviour
{
    public bool ready = true;
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct Test
    {
        [MarshalAs(UnmanagedType.I4)]
        public int material;
        [MarshalAs(UnmanagedType.I4)]
        public int speed;
        [MarshalAs(UnmanagedType.I4)]
        public int block;
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

    IEnumerator waitSec()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        //Debug.Log("start corutine");
        ready = true;
    }

    public SerialController serialController;
    // Start is called before the first frame update
    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        test.material = 0;
        test.speed = 0;
        test.block = 0;
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (ready)
        {
            //ready = false;

            test.material = 1;
            test.speed = 20;
            //serialController.SendStruct(getMashalData());
            String tstr = Encoding.Default.GetString(getMashalData());
            //Debug.Log(tstr.Length);
            serialController.SendSerialMessage(tstr);

            //Test recv = (Test)serialController.ReadStruct();

            //Debug.Log("material : " + recv.material + " , speed : " + recv.speed);

            string message = serialController.ReadSerialMessage();

            /*
            while (true)
            {
                if (message != null)
                    break;
            }
            */
            if (message == null)
            {
                //StartCoroutine((IEnumerator)WaitForIt());
                return;
            }


            // Check if the message is plain data or a connect/disconnect event.
            if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
                Debug.Log("Connection established");
            else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
                Debug.Log("Connection attempt failed or disconnection detected");
            else
                Debug.Log("Message arrived: " + message);
            StartCoroutine(waitSec());
            //StartCoroutine((IEnumerator)WaitForIt());
            return;
        }
        return;
    }
}
