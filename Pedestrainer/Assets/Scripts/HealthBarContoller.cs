using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarContoller : MonoBehaviour
{
    public GameController gameController;
    public Image healthBar;
    int healthBarLength = 543;
    int lastLength = 543;
    int currentHP;
    int maxHP;
    Vector3 origin;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = gameController.currentHP();
        maxHP = gameController.getMaxHP();
        origin = healthBar.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        currentHP = gameController.currentHP();
        maxHP = gameController.getMaxHP();
        int length = 543*currentHP/maxHP;
        var theBarRectTransform = healthBar.transform as RectTransform;
        theBarRectTransform.sizeDelta = new Vector2 (length, theBarRectTransform.sizeDelta.y);
        if(lastLength != length){
            changeHealthBar();
            lastLength = length;
        }
    }
    public void changeHealthBar() {
        float margin = -2.5f*(maxHP-currentHP);
        healthBar.transform.position = origin + new Vector3(margin, 0, 0);
    }
}
