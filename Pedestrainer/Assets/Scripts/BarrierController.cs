using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BarrierController : MonoBehaviour
{
    private Tilemap map;
    private Vector3 pPos;

    // Start is called before the first frame update
    void Start()
    {
        map = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D other) {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        pPos = player.transform.position;
        pPos.y -= 0.5f;
        if(other.gameObject == player){
            map.SetTile(map.WorldToCell(pPos), null);
        }
    }
}

   
