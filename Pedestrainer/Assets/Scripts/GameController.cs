using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void startGame(){
        SceneManager.LoadScene("Pedestrian");
        SceneManager.UnloadSceneAsync("GameController");
    }

    public void quitGame(){
        Application.Quit();
    }

    public void loadScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
