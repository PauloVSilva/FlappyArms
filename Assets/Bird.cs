using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Bird : MonoBehaviour{
    public float speed = 1f;
    private Rigidbody2D rigidbody;
    public GameObject gameOver;
    public AudioSource sleighSFX;
    public AudioSource sadHornSFX;

    public bool ceilingEnabled = true;
    public bool floorProtection = false;

    void Start(){
        rigidbody = GetComponent<Rigidbody2D>();
        sleighSFX = GetComponent<AudioSource>();
        sadHornSFX = GetComponent<AudioSource>();
    }

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
            if(!sleighSFX.isPlaying){
                sleighSFX.Play();
            }
        }

        if (rigidbody.velocity.y <= -0.85f){
            sleighSFX.Stop();
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        gameOver.SetActive(true);
        if(!sadHornSFX.isPlaying){
            sadHornSFX.Play();
        }
        Time.timeScale = 0;
    }
}
