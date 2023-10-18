using Google.ProtocolBuffers.Collections;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Valve.VR.SteamVR_TrackedObject;

public class SelectPopupMole : MonoBehaviour
{

    public GameObject[] molePrefab; //holds all the pirate moles

    public GameObject selectedMole; 
    public GameObject selectedMole2;

    public GameObject pastSelectedMole; 
    public GameObject pastSelectedMole2;

    public int moleIndex; // selected specific PirateMole from array molePrefabs
    public int moleIndex2;

    public float timeLeft = 10f;
    public int amountOfPopUps = 1; // how many pop up at a time

    public Material moleSkin;
    public Material setMaterial;

    bool allowTwoSpawn;

    // Start is called before the first frame update
    void Start()
    {
        //assign mole for script to start
        pastSelectedMole = molePrefab[moleIndex];  
        pastSelectedMole2 = molePrefab[moleIndex2];

        SelectMole();
    }

    // Update is called once per frame
    void Update()
    {

        CountDown();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.tag == "SelectedMole")
                {
                    selectedMole.GetComponent<MoleController>().Hit(); // activate HIT() from MoleXontroller resetting opsition
                    UpdateSelectedMole();
                }
                else if (hit.collider.tag == "SelectedMole2")
                {
                    selectedMole2.GetComponent<MoleController>().Hit();
                    UpdateSelectedMole2();
                }
            }
        }
    }
    private void SelectMole() //choose the next pirate to popup based on amountselected to popup
    {

        pastSelectedMole = molePrefab[moleIndex];
        moleIndex = Random.Range(0, molePrefab.Length);
        selectedMole = molePrefab[moleIndex];
        if (selectedMole.name != pastSelectedMole.name)
        {
            selectedMole.tag = "SelectedMole";
            selectedMole.GetComponent<Renderer>().material = setMaterial;
        }
        else
        {
            SelectMole();
        }
        if(amountOfPopUps == 2)
        {
            allowTwoSpawn = true;
            if(allowTwoSpawn == true)
            {
                pastSelectedMole2 = molePrefab[moleIndex2];
                moleIndex2 = Random.Range(0, molePrefab.Length);
                selectedMole2 = molePrefab[moleIndex2];


                if (selectedMole2.name != pastSelectedMole2.name)
                {
                    selectedMole2.tag = "SelectedMole2";
                    selectedMole2.GetComponent<Renderer>().material = setMaterial;

                }
                else
                {
                    SelectMole();
                }
                allowTwoSpawn = false;
                amountOfPopUps = 1;
            }
        }

    }
    private void SelectMole2()
    {


        pastSelectedMole2 = molePrefab[moleIndex2];
        moleIndex2 = Random.Range(0, molePrefab.Length);
        selectedMole2 = molePrefab[moleIndex2];


        if (selectedMole2.name != pastSelectedMole2.name)
        {
            selectedMole2.tag = "SelectedMole2";
            selectedMole2.GetComponent<Renderer>().material = setMaterial;

        }
        else
        {
            SelectMole2();
        }

        
    }

    public void UpdateSelectedMole()
    {

        selectedMole.GetComponent<Renderer>().material = moleSkin;
        selectedMole.tag = "moles";
        SelectMole();
    }
    public void UpdateSelectedMole2()
    {

        selectedMole2.GetComponent<Renderer>().material = moleSkin;
        selectedMole2.tag = "moles";
        SelectMole2();
    }
    void CountDown()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            amountOfPopUps++;
            timeLeft += 10f;
        }
    }
}
