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

    public GameObject selectedMole;
    public GameObject selectedMole2;

    public bool[] selectedmoles;
    //public int[] tempMole;
    public List<int> tempMole = new List<int>();


    public float timeLeft = 10f;
    public int amountOfPopUps = 1; // how many pop up at a time

    public Material moleSkin;
    public Material setMaterial; //replace with mesh renderer material for 3d renders


    // Start is called before the first frame update
    void Start()
    {

        Array.Resize(ref selectedmoles, molePrefab.Length);
        //tempMole = new int[amountOfPopUps];
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

                    //molePrefab[tempMole].GetComponent<MoleController>().Hit(); // activate HIT() from MoleXontroller resetting opsition
                    ////molePrefab[tempMole].tag = "SelectedMole";
                    //Debug.Log("hit: " + hit.collider.tag);
                    //UpdateSelectedMole();
                    foreach (int moleIndex in tempMole)
                    {
                        molePrefab[moleIndex].GetComponent<MoleController>().Hit();
                    }
                    // Optionally, you can update other game logic here related to hitting the moles.
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

            //    tempMole[] = UnityEngine.Random.Range(0, selectedmoles.Length - 1);

            //    if (selectedmoles[tempMole] == false)
            //    {
            //        selectedmoles[tempMole] = true;

            //        popUp(molePrefab[tempMole]);
            //        molePrefab[tempMole].GetComponent<Renderer>().material = setMaterial;//notify visually which mole is selected

            //        foreach (var mole in selectedmoles)
            //        {
            //            Debug.Log($"Mole " + tempMole + " has been set to " + selectedmoles[tempMole]);
            //        }
            //    }

            int randomMole;
            do
            {
                randomMole = UnityEngine.Random.Range(0, selectedmoles.Length);
            } while (selectedmoles[randomMole]);

            selectedmoles[randomMole] = true;
            tempMole.Add(randomMole); // Store the selected mole's index in the list

            popUp(molePrefab[randomMole]);
            molePrefab[randomMole].GetComponent<Renderer>().material = setMaterial;


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


    public void popUp(GameObject Mole)
    {
        Debug.Log("Run popup script for" + Mole);

        Mole.GetComponent<MoleController>().Popup();
    }

    public void UpdateSelectedMole()
    {
        //for (int i = 0; i < amountOfPopUps; i++)
        //{
        //    molePrefab[tempMole].GetComponent<Renderer>().material = moleSkin;
        //    ////selectedMole.tag = "moles";
        //    ////SelectMole();
        //    selectedmoles[tempMole] = false;
        //    SelectNextMole();
        //}
        for (int i = 0; i < amountOfPopUps; i++)
        {
            if (i < tempMole.Count)
            {
                int moleIndex = tempMole[i];
                // Perform your operations using moleIndex
                molePrefab[moleIndex].GetComponent<Renderer>().material = moleSkin;
                selectedmoles[moleIndex] = false;
            }
        }

        tempMole.Clear(); // Clear the list to indicate that no moles are currently selected.

        SelectNextMole();

    }

    void CountDown()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            if (amountOfPopUps >= 2)
            {
                amountOfPopUps = 2;
            }
            else
            {
                amountOfPopUps++;
            }
            timeLeft += 10f;
        }
    }

}
