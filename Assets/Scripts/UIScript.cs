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
    [SerializeField] int winCondition = 3; // min score to open door
    [SerializeField] GameObject door; // reference to end door

    // life vars
<<<<<<< HEAD
    static public int hearts = 3; // number of hits
    public Image[] lives = new Image[hearts]; // heart icons, getting an overflow exception?
    public Sprite halfFullHeart; 
    public Sprite emptyHeart;
=======
    static public int maxHearts = 3; // number of hits
    private int currHearts;
    [SerializeField] Image[] lives = new Image[maxHearts]; // heart icons
    [SerializeField] Sprite emptyHeart;
>>>>>>> main

    void Awake()
    {
        // init vars
        currHearts = maxHearts;
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
<<<<<<< HEAD
        hearts--;
        // make heart an empty heart sprite after taking damage
        lives[hearts].sprite = emptyHeart;//getting an index out of bounds exception here when he encounters monsters after first monster encounter

        if (hearts == 0) //reset, game lost
=======
        currHearts--;
        
        // make heart an empty heart sprite after taking damage
        lives[currHearts].sprite = emptyHeart;

        if (currHearts == 0)
>>>>>>> main
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
