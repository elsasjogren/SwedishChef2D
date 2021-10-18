using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // loads the level (Play button on Title Screen)
    public void LoadLevel()
    {
        SceneManager.LoadScene("Level1");
    }
}
