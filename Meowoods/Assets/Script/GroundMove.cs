using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour
{

    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!player.isDead){
            Move();
        }
        else{
            Stop();
        }
        
    }

    private void Move(){
transform.Translate(new Vector3(0,0,-5*Time.deltaTime));
        if(transform.position.z <= -60f){
            transform.position = new Vector3(transform.position.x,transform.position.y,180f);
        }
    }
    private void Stop(){
        transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
    }
}
