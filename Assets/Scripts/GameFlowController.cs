using UnityEngine.SceneManagement;
using UnityEngine;

public class GameFlowController : MonoBehaviour
{
    public GameObject canvas;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            print("Game Restarted");
        }
    }
}
