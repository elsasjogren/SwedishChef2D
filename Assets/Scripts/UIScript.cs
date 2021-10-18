using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    private static UIScript instance;

    // scoring vars
    [SerializeField] Text scoreText; // text visible
    [SerializeField] int Score; // value for the score
    [SerializeField] int winCondition; // min score to open door
    [SerializeField] GameObject door; // reference to end door

    // life vars
    static public int maxHearts = 3; // number of hits
    private int currHearts;
    [SerializeField] Image[] lives = new Image[maxHearts]; // heart icons
    [SerializeField] Sprite emptyHeart;


    //win vars
    [SerializeField] Text winText; // text visible

    void Awake()
    {
        // init vars
        currHearts = maxHearts;
        instance = this;
        Score = 0;
        scoreText.text = "Croissants: " + Score.ToString() + "/" + winCondition.ToString();
        
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
        scoreText.text = "Croissants: " + Score.ToString() + "/" + winCondition.ToString();
        checkWin();
    }

    private void _Damaged()
    {
        currHearts--;
        
        // make heart an empty heart sprite after taking damage
        lives[currHearts].sprite = emptyHeart;

        if (currHearts == 0)
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
            door.GetComponent<DoorScript>().playerSucceds();
        }
    }

    private void _WinGame()
    {
        Transform panel = transform.GetChild(2);
        panel.gameObject.SetActive(true);
        int extra = winCondition - Score;
        if (extra > 0)
        {
            winText.text = "And you collected " + Score.ToString() + " pasteries, that's " + extra.ToString() + " more than needed!";
        }
        else
        {
            winText.text = "And you collected " + Score.ToString() + " pasteries, that's just enough! Play again to see if you can collect more!";
        }
       
    }
}
