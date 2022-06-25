using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Bird : MonoBehaviour{
    public float speed = 1f;
    private Rigidbody2D rigidbody;
    public GameObject gameOver;
    AudioSource audioData;

    public bool ceilingEnabled = true;
    public bool floorProtection = false;

    // Start is called before the first frame update
    void Start(){
        rigidbody = GetComponent<Rigidbody2D>();
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
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

        if (rigidbody.velocity.y > 0){
            if(!audioData.isPlaying){
                audioData.Play();
            }
        }

        if (rigidbody.velocity.y <= -0.85f){
            audioData.Stop();
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }
}
