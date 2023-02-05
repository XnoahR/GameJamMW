using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerController player;
    TextMeshProUGUI ScoreText;
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        ScoreText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = player.score.ToString();
    }
}
