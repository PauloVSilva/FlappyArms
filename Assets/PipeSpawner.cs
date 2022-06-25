using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{

    public GameObject pipe;
    public float height;
    public float maxTime;
    private float timer = 0f;

    public bool allowSpawning = true;

    // Start is called before the first frame update
    void Start(){
        //GameObject newPipe = Instantiate(pipe);
        //newPipe.transform.position = transform.position + new Vector3(0, Random.Range(-height, height), 0);
        //Destroy(newPipe, 7f);
    }

    // Update is called once per frame
    void Update(){
        if (allowSpawning){
            if (timer > maxTime){
                GameObject newPipe = Instantiate(pipe);
                newPipe.transform.position = transform.position + new Vector3(0, Random.Range(-height, height), 0);
                Destroy(newPipe, 7f);
                timer = 0;
            }
            timer += Time.deltaTime;
        }
    }
}
