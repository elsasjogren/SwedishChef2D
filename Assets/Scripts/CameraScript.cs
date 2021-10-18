using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject Player; // reference to player

    // bounds for the camera position
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;

    // Start is called before the first frame update
    void Awake()
    {
        // set camera to left most position
        transform.position = new Vector3(minX, Player.transform.position.y, -10);
    }

    // Update is called once per frame
    void Update()
    {
        // follow the player unless player exceeds bounds
        if(Player.transform.position.x > minX && transform.position.x < maxX && Player.transform.position.y > minY && transform.position.y < maxY)
        {
            transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -10);
        } else {
            
            float newX = Player.transform.position.x;
            float newY = Player.transform.position.y;

            // prevent camera from moving beyond bounds
            if (Player.transform.position.x <= minX)
            {
                newX = minX;
            }
            else if (Player.transform.position.x >= maxX)
            {
                newX = maxX;
            }

            if (Player.transform.position.y <= minY)
            {
                newY = minY;
            }
            else if (Player.transform.position.y >= maxY)
            {
                newY = maxY;
            }

            // set new position depending on location relative to bounds
            transform.position = new Vector3(newX, newY, -10);
        }
    }

    // draw bounding box
    private void OnDrawGizmosSelected()
    {
        Vector3 bottomleft = new Vector3(minX, minY, -1);
        Vector3 topleft = new Vector3(minX, maxY, -1);
        Vector3 bottomright = new Vector3(maxX, minY, -1);
        Vector3 topright = new Vector3(maxX, maxY, -1);

        Debug.DrawLine(bottomleft, bottomright, Color.cyan);
        Debug.DrawLine(bottomright, topright, Color.cyan);
        Debug.DrawLine(topright, topleft, Color.cyan);
        Debug.DrawLine(topleft, bottomleft, Color.cyan);

    }

}
