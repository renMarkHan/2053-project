using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameController : MonoBehaviour
{
    private int healthPoint= 20;
    public int maxHP;
    public Text endText;
    private Canvas pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        healthPoint= 20;
        endText.text = "";
    }

    public void startGame(){
        SceneManager.LoadScene("Pedestrian");
        healthPoint= 20;
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pauseMenu = GameObject.Find("PauseMenu").GetComponent<Canvas>();
            pauseMenu.enabled = false;
        }
        SceneManager.UnloadSceneAsync("GameController");
    }

    public void FirstStartGame(){
        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        VideoPlayer player = GameObject.Find("Main Camera").GetComponent<VideoPlayer>();
        canvas.enabled = false;
        player.Play();
        StartCoroutine(jumpIntoFirstSceneAfterVideo());
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
        if(healthPoint<20){
            healthPoint++;
        }
        
    }

    public void recoverHP(int hp)
    {
        if(hp>20){
            hp = 20;
        }
        healthPoint += hp;
        Debug.Log("Current HP: " + healthPoint);
    }

    public void gameLost()
    {
        SceneManager.LoadScene("Pedestrian");
        healthPoint= 20;
        SceneManager.UnloadSceneAsync("GameController");
    }

    public void GameOver(){
        endText.text = "Game Over!!! You win!!!!!";
    }
    public void pause()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pauseMenu = GameObject.Find("PauseMenu").GetComponent<Canvas>();
            pauseMenu.enabled = false;
        }
        else
        {
            Time.timeScale = 0;
            pauseMenu = GameObject.Find("PauseMenu").GetComponent<Canvas>();
            pauseMenu.enabled = true;
        }

    }

    IEnumerator jumpIntoFirstSceneAfterVideo()
    {
        yield return new WaitForSecondsRealtime(58);
        SceneManager.LoadScene("Pedestrian");
        healthPoint= 20;
        SceneManager.UnloadSceneAsync("GameController");
    }
    

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            GameOver();
        }
    }
}
