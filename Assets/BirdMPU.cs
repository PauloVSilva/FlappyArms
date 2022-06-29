using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class BirdMPU : MonoBehaviour{// Start is called before the first frame update
    SerialPort stream = new SerialPort("COM3", 115200);
    public string strReceived;
    public string[] strData = new string[3];
    public string[] strData_recieved = new string[3];
    public int ax, ay, az;

    public float speed = 1f;
    public float threshhold = 3f;
    private Rigidbody2D rigidbody;
    
    // Start is called before the first frame update
    void Start(){
        stream.Open();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        strReceived = stream.ReadLine(); 
        strData = strReceived.Split(',');

        //this is where the magic happens I guess
        if (strData[0] != "" && strData[1] != "" && strData[2] != ""){
            strData_recieved[0] = strData[0];
            strData_recieved[1] = strData[1];
            strData_recieved[2] = strData[2];
            ax = int.Parse(strData_recieved[0]);
            ay = int.Parse(strData_recieved[1]);
            az = int.Parse(strData_recieved[2]);

            ay = ay / 1000;
            ax = ax / 1000;
            az = az / 1000;

            if (az >= threshhold) {
                Vector2 input = new Vector2(0, speed);
                rigidbody.velocity = input;
            }
        }
    }
}
