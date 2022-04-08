using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    public GameController gameobject;
   

    private void OnCollisionEnter2D(Collision2D collision)
        
    {
        print("....................");
        if (collision.collider.CompareTag("Player"))
        {
            gameobject.loseHP(1);
            Debug.Log("NIGGGER");
        }
    }
}
