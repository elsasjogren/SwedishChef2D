using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastryScript : MonoBehaviour
{
    private float startY;
    [SerializeField] float bobSpeed = 2.0f;
    [SerializeField] float bobHeight = 0.25f;
    [SerializeField] float collectionDelay = 1.5f; // time between another item can be picked up
    [SerializeField] bool beingCollected = false;

    private AudioSource myaudio;
    [SerializeField] AudioClip[] collectionSound;

    void Awake()
    {
        myaudio = GetComponent<AudioSource>();
        startY = Mathf.RoundToInt(transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        // make the pastry bounce up n down
        Vector3 bob = new Vector3(transform.position.x, startY + bobHeight*Mathf.Sin(bobSpeed*Time.realtimeSinceStartup), 0);
        transform.position = bob;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // collect the item
        if (collision.CompareTag("Player") && !beingCollected)
        {
            beingCollected = true;
            StartCoroutine(getCollected());
        }
    }

    private IEnumerator getCollected()
    {
        myaudio.PlayOneShot(collectionSound);
        UIScript.IncreaseScore();

        // make it disapear while giving time to play audio before destroying
        GetComponent<SpriteRenderer>().sprite = null;
        yield return new WaitForSeconds(collectionDelay);
        Destroy(gameObject);
    }

}
