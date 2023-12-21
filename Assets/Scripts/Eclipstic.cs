using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Unity.Mathematics;
using UnityEngine;

public class Eclipstic : MonoBehaviour
{
    public bool ready = true;
    public bool istest = false;
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct Test
    {
        [MarshalAs(UnmanagedType.I2)]
        public short perrity;
        [MarshalAs(UnmanagedType.I2)]
        public short material;
        [MarshalAs(UnmanagedType.I2)]
        public short speed;
        [MarshalAs(UnmanagedType.I2)]
        public short block;
        [MarshalAs(UnmanagedType.I2)]
        public short  empty_bit;
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
        yield return new WaitForSecondsRealtime(0.12f);
        //Debug.Log("start corutine");
        ready = true;
    }

    public SerialController serialController;
    public Controller controller;
    public EnvironmentTextureController textureController;
    // Start is called before the first frame update
    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        controller = GameObject.Find("PlayerControl").GetComponent<Controller>();
        textureController = GameObject.Find("TextureControl").GetComponent<EnvironmentTextureController>();
        test.material = 0;
        test.speed = 0;
        test.block = 0;
        test.empty_bit = 0;
        test.perrity = 302;
        ready = true;
        istest = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ready)
        {
            ready = false;

            test.material = (short)textureController.texint;
            test.speed = (short)(controller.velocity*40);
            test.speed = (short)((test.speed > 100) ? 100 : test.speed);
            test.block = (short)textureController.obs;

            if (istest)
            {
                test.material = 3;
                test.speed = 25;
                test.block = 125;
            }

            //Debug.Log(" texture : " + test.material + "speed : " + test.speed);
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
                StartCoroutine(waitSec());
                return;
            }


            // Check if the message is plain data or a connect/disconnect event.
            if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
                Debug.Log("Connection established");
            else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
                Debug.Log("Connection attempt failed or disconnection detected");
            else
            {
                Debug.Log("arduino : " + message + "  || unity's texture : " + test.material + " , speed : " + test.speed);
                if(istest)
                {
                    string deb = "";
                    foreach (char c in tstr)
                    {
                        deb += ((byte) c + ", ");
                    }

                    Debug.Log(deb);
                }
                while (serialController.ReadSerialMessage() != null) ;
            }
            StartCoroutine(waitSec());
            //StartCoroutine((IEnumerator)WaitForIt());
            return;
        }
        return;
    }
}
