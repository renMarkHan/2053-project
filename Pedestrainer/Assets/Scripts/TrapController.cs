using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    public GameController gameobject;
   

    private void OnCollisionEnter2D(Collision2D collision)
        
    {
        if (collision.collider.CompareTag("Player"))
        {
            gameobject.loseHP(1);
        }
    }
}
