using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public bool isSecret = false;
    public int scenePosition;
    public Vector2 newPlayerPosition;
    public VectorValue playerStorage;

    void Start(){
        
    }



    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger && !isSecret && (this.gameObject.name == "escape"))
        {
            playerStorage.initalValue = newPlayerPosition;
            scenePosition = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scenePosition + 1);
        }

        if(other.CompareTag("Player") && !other.isTrigger && !isSecret && (this.gameObject.name == "transition")) 
        {
            isSecret = true;
            playerStorage.initalValue = newPlayerPosition;
            SceneManager.LoadScene("SecretScene");

        }

        if(other.CompareTag("Player") && !other.isTrigger && isSecret && (this.gameObject.name == "EscapeDoor"))
        {
            isSecret = false;
            playerStorage.initalValue = newPlayerPosition;
            SceneManager.LoadScene("Level5");

        }
    }
}
