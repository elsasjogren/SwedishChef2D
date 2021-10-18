using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] GameObject player; // keep to tell player when can move
    private ChefScript chefScript;

    [SerializeField] Sprite[] openDoor; // sprites for opening/closing the door
    [SerializeField] Sprite[] closeDoor;
    [SerializeField] AudioClip openCloseSound;
    [SerializeField] float bubbleTime;

    private SpriteRenderer mySpriteRenderer;
    private AudioSource myaudio;
    private Transform doorTop;
    private Transform speechBubble;


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

        speechBubble.gameObject.SetActive(false);
        StartCoroutine(IntroAnimation());
    }

    // true if want to open, false if closing
    public void DoorStatusChange(bool open)
    {
        myaudio.PlayOneShot(openCloseSound);
        if (open)
        {
            mySpriteRenderer.sprite = openDoor[0];
            doorTop.GetComponent<SpriteRenderer>().sprite = openDoor[1];
        }
        else
        {
            mySpriteRenderer.sprite = closeDoor[0];
            doorTop.GetComponent<SpriteRenderer>().sprite = closeDoor[1];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && mySpriteRenderer.sprite == openDoor[0])
        {
            chefScript.movement = false;
            UIScript.WinGame();
        }
    }

    private IEnumerator IntroAnimation()
    {
        SpriteRenderer chefRenderer = player.GetComponent<SpriteRenderer>();

        // wait a second and turn on speech bubble
        yield return new WaitForSeconds(0.75f);
        Transform speechBubble = transform.GetChild(1);
        DoorStatusChange(true);
        speechBubble.gameObject.SetActive(true);
        chefRenderer.flipX = true; // face the bubble

        // remove bubble and close door
        yield return new WaitForSeconds(bubbleTime);
        DoorStatusChange(false);
        speechBubble.gameObject.SetActive(false);

        // let the player move now
        chefScript.movement = true;
        chefRenderer.flipX = false;
    }

    public void playerSucceds() {
        Transform winBubble = transform.GetChild(2);
        winBubble.gameObject.transform.position = player.transform.position;
        winBubble.gameObject.SetActive(true);
    }

}
