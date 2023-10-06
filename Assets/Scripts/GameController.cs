using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class GameController : MonoBehaviour
{
    public int totalNumOfEnemies = 0; //total number of enemies capable of spawing at once
    public GameObject[] specialEnemies;
    public int amountOfSpecialEnemies;

    public Quaternion PlayerDir; //Directoin player is looking

    public int score = 0;
    public float Time = 0;
    public int phases = 0;

    //UI
    public TextMeshPro timeNum;


    // Start is called before the first frame update
    void Start()
    {

        timeNum.text = "2:00";
        
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (phases) 
        {
            case 0:
                default: 
                break;
            case 1:
                break;
            case 2:
                break;
        }

    }
}
