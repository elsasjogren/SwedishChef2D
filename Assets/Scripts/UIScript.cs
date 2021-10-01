using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Text scoreText;

    public static int Score;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        scoreText.text = "Croissants: " + Score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // increase the score (for when crossaint collected
    public static void IncreaseScore()
    {
        Score += 1;
    }
}
