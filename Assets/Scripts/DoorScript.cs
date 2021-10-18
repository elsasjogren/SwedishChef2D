using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    [SerializeField] GameObject player; // keep to tell player when can move
    private ChefScript chefScript;

    // animation vars
    [SerializeField] Sprite[] openDoor; // sprites for opening/closing the door
    [SerializeField] Sprite[] closeDoor;
    [SerializeField] AudioClip openCloseSound; // sounds for ^

    private Transform speechBubble; // reference to intro speech bubble
    [SerializeField] float bubbleTime; // time bubble is on screen
    
    private SpriteRenderer mySpriteRenderer;
    private AudioSource myaudio;
    private Transform doorTop; // reference to door top (is a separate sprite)


    // Start is called before the first frame update
    void Start()
    {
        // disable player movement + init vars
        chefScript = player.GetComponent<ChefScript>();
        chefScript.movement = false;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myaudio = GetComponent<AudioSource>();
        doorTop = transform.GetChild(0);
        speechBubble = transform.GetChild(1);
        speechBubble.gameObject.SetActive(false); // make invisible at start
        StartCoroutine(IntroAnimation()); // begin intro
    }

    // opens/closes the door
    public void DoorStatusChange(bool open)
    {
        // play sound and change sprite accordingly
        myaudio.PlayOneShot(openCloseSound);
        if (open)
        {
            mySpriteRenderer.sprite = openDoor[0];
            doorTop.GetComponent<SpriteRenderer>().sprite = openDoor[1];
        } else {
            mySpriteRenderer.sprite = closeDoor[0];
            doorTop.GetComponent<SpriteRenderer>().sprite = closeDoor[1];
        }
    }

    // player wins when passing through the open door
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && mySpriteRenderer.sprite == openDoor[0])
        {
            // stop the player from moving once game is complete
            chefScript.movement = false;
            chefScript.rb2d.velocity = Vector3.zero;
            chefScript.inMotion = false;

            // display win screen
            UIScript.WinGame();
        }
    }

    // animation for the start of the game
    private IEnumerator IntroAnimation()
    {
        // access chef's sprite
        SpriteRenderer chefRenderer = player.GetComponent<SpriteRenderer>();

        // wait to turn on speech bubble
        yield return new WaitForSeconds(0.65f);

        // display bubble and open the door
        Transform speechBubble = transform.GetChild(1);
        DoorStatusChange(true);
        speechBubble.gameObject.SetActive(true);
        chefRenderer.flipX = true; // face the door

        // remove bubble and close door
        yield return new WaitForSeconds(bubbleTime);
        DoorStatusChange(false);
        speechBubble.gameObject.SetActive(false);

        // let the player move now
        chefScript.movement = true;
        chefRenderer.flipX = false;
    }

    // display winning thought bubble 
    public IEnumerator playerSucceeds() {
        Transform winBubble = player.transform.GetChild(1);
        winBubble.gameObject.SetActive(true);

        yield return new WaitForSeconds(2.0f);
        winBubble.gameObject.SetActive(false);
    }

}
