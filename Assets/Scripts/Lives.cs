using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lives : MonoBehaviour
{
    static public float hearts;
    public Sprite[] lives;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void Damaged()
    {
        hearts--;
        if (hearts == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
