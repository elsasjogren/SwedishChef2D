using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterScript : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private SpriteRenderer mySpriteRenderer;

    public Sprite[] jumpSprite = new Sprite[5];
    public AudioClip[] attackSprite = new AudioClip[6];
    public AudioClip[] destroySprite = new AudioClip[7];

    public float monsterSpeed;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
        //on collision with player should take away one of the players three/five lives 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Lives.hearts > 0)
            {
                Lives.hearts--;
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
