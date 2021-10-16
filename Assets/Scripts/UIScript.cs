using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    private static UIScript instance;

    // scoring vars
    public Text scoreText; // text visible
    public int Score; // value for the score
    public int winCondition = 3; // min score to open door
    public GameObject door; // reference to end door

    public Sprite[] openDoor; // sprites for opening the door

    // life vars
    static public int hearts = 3; // number of hits
    public Image[] lives = new Image[hearts]; // heart icons
    public Sprite halfFullHeart; 
    public Sprite emptyHeart;

    void Awake()
    {
        instance = this;
        Score = 0;
        scoreText.text = "Croissants: " + Score.ToString();
    }

    // increase the score (for when crossaint collected
    public static void IncreaseScore()
    {
        Debug.Log("called");
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
        checkWin();
    }

    private void _Damaged()
    {
        hearts--;

        // make heart an empty heart sprite after taking damage
        lives[hearts].sprite = emptyHeart;

        if (hearts == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
    
    private void checkWin()
    {
        // open the door if score is enough
        if(Score >= winCondition)
        {
            // [INSERT SOUND PLAYING HERE]
            door.GetComponent<SpriteRenderer>().sprite = openDoor[0];
            Transform topDoor = door.transform.GetChild(0);
            topDoor.GetComponent<SpriteRenderer>().sprite = openDoor[1];
        }
    }
}
