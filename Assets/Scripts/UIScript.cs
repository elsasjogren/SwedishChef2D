using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Text scoreText;
    private static UIScript instance;

    public int Score;
    
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
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
        instance._IncreaseScore();
    }

    public void _IncreaseScore()
    {
        Score += 1;
        scoreText.text = "Croissants: " + Score.ToString();
    }
}
