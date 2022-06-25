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

    public bool ceilingEnabled = true;
    public bool floorProtection = false;

    public float speed = 1f;
    private Rigidbody2D rigidbody;

    public GameObject gameOver;
    
    // Start is called before the first frame update
    void Start(){
        stream.Open();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        strReceived = stream.ReadLine(); 
        strData = strReceived.Split(',');

        if (ceilingEnabled){
            if (rigidbody.transform.position.y >= 1) {
                float xPos = rigidbody.transform.position.x;
                Vector2 input = new Vector2(0, 0);

                rigidbody.transform.position = new Vector2(xPos, 1);
                rigidbody.velocity = input;
            }
        }

        if (floorProtection){
            if (rigidbody.transform.position.y <= -0.5f) {
                float xPos = rigidbody.transform.position.x;
                Vector2 input = new Vector2(0, 0);

                rigidbody.transform.position = new Vector2(xPos, -0.5f);
                rigidbody.velocity = input;
            }
        }

        if (Input.GetMouseButtonDown(0)){
            rigidbody.velocity = Vector2.up * speed;
        }

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

            if (az >= 3) {
                Vector2 input = new Vector2(0, speed);
                rigidbody.velocity = input;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }
}
