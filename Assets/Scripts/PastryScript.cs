using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastryScript : MonoBehaviour
{
    // movement variables
    private float startY; // store init pos to base movement around
    [SerializeField] float bobSpeed = 2.0f; // frequency of bobs / second
    [SerializeField] float bobHeight = 0.25f; // amplitude of bobs

    // collection parameters
    [SerializeField] float collectionDelay = 1.5f; // time between another item can be picked up
    [SerializeField] bool beingCollected = false; // true if player is collecting the pastry

    private AudioSource myaudio;
    [SerializeField] AudioClip collectionSound;

    void Awake()
    {
        // init vars
        myaudio = GetComponent<AudioSource>();
        startY = Mathf.RoundToInt(transform.position.y); // round current pos to an int
    }

    private void Start()
    {
        // allow for some delay when spawned in to avoid instant pick up
        StartCoroutine(delayPickup());
    }

    // Update is called once per frame
    void Update()
    {
        // make the pastry bounce up n down
        Vector3 bob = new Vector3(transform.position.x, startY + bobHeight*Mathf.Sin(bobSpeed*Time.realtimeSinceStartup), 0);
        transform.position = bob;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // collect the item
        if (collision.CompareTag("Player") && !beingCollected)
        {
            beingCollected = true;
            StartCoroutine(getCollected());
        }
    }

    // item gets collected 
    private IEnumerator getCollected()
    {
        // make sound and increment score
        myaudio.PlayOneShot(collectionSound);
        UIScript.IncreaseScore();

        // make it disapear while giving time to play audio before destroying
        GetComponent<SpriteRenderer>().sprite = null;
        yield return new WaitForSeconds(collectionDelay);
        Destroy(gameObject);
    }

    // prevent player from collecting item on spawn
    private IEnumerator delayPickup()
    {
        beingCollected = true;
        yield return new WaitForSeconds(collectionDelay);
        beingCollected = false;
    }

}
