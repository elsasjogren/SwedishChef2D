using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastryScript : MonoBehaviour
{
    public float startY;
    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // make the pastry bounce up n down
        Vector3 bob = new Vector3(transform.position.x, startY + 0.35f*Mathf.Sin(1.5f*Time.realtimeSinceStartup), 0);
        //sin = Mathf.Sin(0.5f * Time.deltaTime);
        transform.position = bob;
    }
}
