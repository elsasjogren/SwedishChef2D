using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    // singleton UIScript
    private static UIScript instance;

    // scoring vars
    [SerializeField] Text scoreText; // text visible
    [SerializeField] int Score; // value for the score
    [SerializeField] int winCondition; // min score to open door
    [SerializeField] GameObject door; // reference to end door

    // life vars
    static public int maxHearts = 3; // number of hits
    private int currHearts; // current hearts  
    [SerializeField] Image[] lives = new Image[maxHearts]; // heart icons
    [SerializeField] Sprite emptyHeart;

    //win vars
    [SerializeField] Text winText; // text visible for win

    void Awake()
    {
        // init vars
        currHearts = maxHearts;
        instance = this;
        Score = 0;
        scoreText.text = "Croissants: " + Score.ToString() + "/" + winCondition.ToString();
        
    }

    // allows play again button to reset the game
    public static void Reset()
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // increase the score (for when pastry collected)
    public static void IncreaseScore()
    {
        instance._IncreaseScore();
    }

    // player takes damage
    public static void Damaged()
    {
        instance._Damaged();
    }

    // display win screen
    public static void WinGame()
    {
        instance._WinGame();
    }

    // private method to increase score
    private void _IncreaseScore()
    {
        Score += 1;
        scoreText.text = "Croissants: " + Score.ToString() + "/" + winCondition.ToString();
        checkWin();
    }

    // remove heart icons from UI for each hit taken
    private void _Damaged()
    {
        currHearts--;
        
        // make heart an empty heart sprite after taking damage
        lives[currHearts].sprite = emptyHeart;

        // reload the game if at zero hearts
        if (currHearts == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
    
    // check that the minimum score to win has been achieved
    private void checkWin()
    {
        // open the door if score is enough
        if(Score == winCondition)
        {
            // open door and show player they have enough pastries
            door.GetComponent<DoorScript>().DoorStatusChange(true);
            StartCoroutine(door.GetComponent<DoorScript>().playerSucceeds());
        }
    }

    // display a message to the winner about their score
    private void _WinGame()
    {
        Transform panel = transform.GetChild(2);
        panel.gameObject.SetActive(true);
        int extra = Score - winCondition;
        if (extra > 0)
        {
            winText.text = "And you collected " + Score.ToString() + " pasteries, that's " + extra.ToString() + " more than needed!";
        } else {
            winText.text = "And you collected " + Score.ToString() + " pasteries, that's just enough! Play again to see if you can collect more!";
        }
       
    }
}
