using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject FirePoint;
    Vector3 Direction;
        // Start is called before the first frame update
    private void Awake() {
        FirePoint = GameObject.Find("FirePoint");
    }
    private void Start() {
        Direction = FirePoint.transform.forward;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Direction*12f*Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other) {      
        if(other.gameObject.CompareTag("Enemy")){
            Debug.Log("Hit!");
            if(gameObject != null){
                Destroy(gameObject);
            }
        }
    }
}
