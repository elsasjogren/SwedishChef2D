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
    public AudioClip ding; // food bell chime

    // life vars
    static public int hearts = 3; // number of hits
    public Image[] lives = new Image[hearts]; // heart icons
    public Sprite halfFullHeart; 
    public Sprite emptyHeart;

    void Awake()
    {
        // init vars
        instance = this;
        Score = 0;
        scoreText.text = "Croissants: " + Score.ToString();
    }

    private void Update()
    {
        // reset button
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public static void Reset()
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

    public static void WinGame()
    {
        instance._WinGame();
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
            door.GetComponent<DoorScript>().DoorStatusChange(true);
        }
    }

    private void _WinGame()
    {
        Transform panel = transform.GetChild(2);
        panel.gameObject.SetActive(true);
    }
}
