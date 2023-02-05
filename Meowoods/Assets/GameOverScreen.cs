using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameOverScreen : MonoBehaviour
{
    PlayerController player;
    public GameObject DeadScreen;
    public GameObject ManaScreen;
    public GameObject ScoreScreen;

    private void Awake() {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Start() {
        DeadScreen.SetActive(false);
    }


    private void Update() {
        if(player.isDead){
            DeadScreen.SetActive(true);
            ManaScreen.SetActive(false);
            ScoreScreen.SetActive(false);
            
        }
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
