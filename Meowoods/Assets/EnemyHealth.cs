using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] private float health;
    public float CurrentHealth;
    public bool onAreaAttack = false;
    // Start is called before the first frame update
    private void Awake() {
        CurrentHealth = health;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(onAreaAttack){
            CurrentHealth -= 100 * Time.deltaTime;
        }

        CheckHealth();
    }

    void Damaged(int damage){
        CurrentHealth -= damage;
    }

    void CheckHealth(){
        if(CurrentHealth <= 0){
            CurrentHealth = 0;
            Dead();
        }
    }

    void Dead(){
        Destroy(gameObject);
    }
}
