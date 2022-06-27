using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Points : MonoBehaviour{
    public GameController controller;
    public AudioSource happyHornSFX;

    void Start(){
        controller = FindObjectOfType<GameController>();
        happyHornSFX = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collider){
        controller.score++;
        controller.scoreText.text = controller.score.ToString();
        if(!happyHornSFX.isPlaying){
            happyHornSFX.Play();
        }
    }
}
