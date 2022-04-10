using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int healthPoint;
    public int maxHP;
    public Text endText;

    // Start is called before the first frame update
    void Start()
    {
        endText.text = "";
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

    public void setCurrentHP(){
        healthPoint++;
    }

    public void recoverHP(int hp)
    {
        healthPoint += hp;
        Debug.Log("Current HP: " + healthPoint);
    }

    public void gameLost()
    {
        Debug.Log( "Game lost!!!!!!");
    }

    public void GameOver(){
        endText.text = "Game Over!!! You win!!!!!";
    }
    public void pause()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            GameOver();
        }
    }
}
