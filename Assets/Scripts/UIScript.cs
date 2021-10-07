using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    // scoring vars
    public Text scoreText;
    public int Score;
    private static UIScript instance;

    // life vars
    static public int hearts = 3;
    public Image[] lives = new Image[3];
    public Sprite halfFullHeart;
    public Sprite emptyHeart;

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

    public static void Damaged()
    {
        instance._Damaged();
    }

    private void _IncreaseScore()
    {
        Score += 1;
        scoreText.text = "Croissants: " + Score.ToString();
    }

    private void _Damaged()
    {
        hearts--;

        /* for half hearts
         * hearts -= 0.5;
         * 
         * if x.5 hearts make sprite halfheart - Math.floor/ceiling(hearts)?
         */

        // make heart an empty heart sprite after taking damage
        lives[hearts].sprite = emptyHeart;

        if (hearts == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}
