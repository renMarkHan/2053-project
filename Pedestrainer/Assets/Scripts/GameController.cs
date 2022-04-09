using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int healthPoint;
    public int maxHP;

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

    public void loseHP(int hp)
    {
        healthPoint -= hp;
        Debug.Log("Current HP: "+healthPoint);
    }
    public int currentHP()
    {
        return healthPoint;
    }
    public int getMaxHP()
    {
        return maxHP;
    }
    public void recoverHP(int hp)
    {
        healthPoint += hp;
        Debug.Log("Current HP: " + healthPoint);
    }
}
