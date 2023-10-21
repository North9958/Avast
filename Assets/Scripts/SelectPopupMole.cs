using Google.ProtocolBuffers.Collections;
using System;
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

    //GameObject targetObject = GameObject.FindWithTag("moles");


    public GameObject selectedMole;
    public GameObject selectedMole2;

    /// <summary>
    public bool[] selectedmoles;
    int tempMole;
    /// </summary>


    public GameObject pastSelectedMole;
    public GameObject pastSelectedMole2;


    public int moleIndex; // selected specific PirateMole from array molePrefabs
    public int moleIndex2;

    public float timeLeft = 10f;
    public int amountOfPopUps = 1; // how many pop up at a time

    public Material moleSkin;
    public Material setMaterial; //replace with mesh renderer material for 3d renders


    // Start is called before the first frame update
    void Start()
    {

        Array.Resize(ref selectedmoles, molePrefab.Length);

        //for(int i = 0; i > molePrefab.Length; i++)
        //{
        //    selectedmoles[i] = false;

        //    Debug.Log("Mole " + i + " equals false.");
        //}


        SelectNextMole();
    }

    // Update is called once per frame
    void Update()
    {
        //SelectNextMole();
        CountDown();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.tag == "moles")
                {
                    molePrefab[tempMole].GetComponent<MoleController>().Hit(); // activate HIT() from MoleXontroller resetting opsition
                    //molePrefab[tempMole].tag = "SelectedMole";
                    Debug.Log("hit: " + hit.collider.tag);
                    UpdateSelectedMole();
                }
            }
        }
    }
    public void SelectNextMole()
    {
        Debug.Log("Ran Script - Select next mole");

        // This value runs the bool statement
        bool run = true;


        while (run)
        {
            if (CheckActiveMoles())
            {
                MoleSelect();

            }
            else
            {
                run = false;
            }
        }

    }
    public void MoleSelect()
    {
        for (int i = 0; i < amountOfPopUps; i++)
        {
            tempMole = UnityEngine.Random.Range(0, selectedmoles.Length - 1);

            if (selectedmoles[tempMole] == false)
            {
                selectedmoles[tempMole] = true;

                popUp(molePrefab[tempMole]);
                molePrefab[tempMole].GetComponent<Renderer>().material = setMaterial;//notify visually which mole is selected

                foreach (var mole in selectedmoles)
                {
                    Debug.Log($"Mole " + tempMole + " has been set to " + selectedmoles[tempMole]);
                }
            }
            else
            {
                MoleSelect();
            }
        }

    }
    public bool CheckActiveMoles()
    {

        int temp = 0;

        for (int i = 0; i < selectedmoles.Length; i++)
        {
            if (selectedmoles[i] == true)
            {
                temp = temp + 1;
            }
        }

        if (temp < amountOfPopUps)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //grabs the mole

    public void popUp(GameObject Mole)
    {
        Debug.Log("Run popup script for" + Mole);

        Mole.GetComponent<MoleController>().Popup();
    }

    private void SelectMole() //choose the next pirate to popup based on amountselected to popup
    {

        //pastSelectedMole = molePrefab[moleIndex];
        //moleIndex = UnityEngine.Random.Range(0, molePrefab.Length);
        //selectedMole = molePrefab[moleIndex];
        //if (selectedMole.name != pastSelectedMole.name)
        //{
        //    selectedMole.tag = "SelectedMole";
        //    selectedMole.GetComponent<Renderer>().material = setMaterial;
        //}
        //else
        //{
        //    SelectMole();
        //}
        //if(amountOfPopUps == 2)
        //{
        //    //allowTwoSpawn = true;
        //    //if(allowTwoSpawn == true)
        //    //{
        //    //    pastSelectedMole2 = molePrefab[moleIndex2];
        //    //    moleIndex2 = UnityEngine.Random.Range(0, molePrefab.Length);
        //    //    selectedMole2 = molePrefab[moleIndex2];


        //    //    if (selectedMole2.name != pastSelectedMole2.name)
        //    //    {
        //    //        selectedMole2.tag = "SelectedMole2";
        //    //        selectedMole2.GetComponent<Renderer>().material = setMaterial;

        //    //    }
        //    //    else
        //    //    {
        //    //        SelectMole();
        //    //    }
        //    //    allowTwoSpawn = false;
        //    //    amountOfPopUps = 1;
        //    //}
        //}

    }
    private void SelectMole2()
    {


        pastSelectedMole2 = molePrefab[moleIndex2];
        moleIndex2 = UnityEngine.Random.Range(0, molePrefab.Length);
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
        //for (int i = 0; i < amountOfPopUps; i++)
        //{
        molePrefab[tempMole].GetComponent<Renderer>().material = moleSkin;
        //selectedMole.tag = "moles";
        //SelectMole();
        selectedmoles[tempMole] = false;
        SelectNextMole();
        //}
        //for (int i = 0; i < selectedmoles.Length; i++)
        //{
        //    //if (selectedmoles[i] == true)
        //    //{
        //    //    //// Deselect the mole
        //    //    //selectedmoles[i] = false;

        //    //    //// Change the material back to the deselected state (if needed)
        //    //    //molePrefab[i].GetComponent<Renderer>().material = moleSkin;
        //    //    molePrefab[i].GetComponent<Renderer>().material = moleSkin;
        //    //    //selectedMole.tag = "moles";
        //    //    //SelectMole();
        //    //    selectedmoles[i] = false;
        //    //    SelectNextMole();
        //    //}
        //    //SelectNextMole();
        //}
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
