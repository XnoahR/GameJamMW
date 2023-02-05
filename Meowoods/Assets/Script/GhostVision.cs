using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostVision : MonoBehaviour
{
    EnemyAI enemy;
    public GameObject ghost;
    // Start is called before the first frame update
    private void Awake() {
        enemy = ghost.GetComponent<EnemyAI>();
    }
  

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            Debug.Log("Detected");
            enemy.Detected = true;
        }
    }
}
