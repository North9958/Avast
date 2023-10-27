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


    public bool[] selectedmoles; //create pirates and check using boolean value to determine which pirate is selected
    //public int[] tempMole;
    public List<int> tempMole = new List<int>(); // tempMole stores current selected pirate, due to chance of multiple pirates being selected tempMole is list 


    public float timeLeft = 10f; // time left untile nexty mole spawns
    public int amountOfPopUps = 1; // how many pirates pop up at a time

    public Material moleSkin; //temp material set // change to MESH
    public Material setMaterial; //replace with mesh renderer material for 3d renders


    // Start is called before the first frame update
    void Start()
    {

        Array.Resize(ref selectedmoles, molePrefab.Length);


        SelectNextMole();
    }

    // Update is called once per frame
    void Update()
    {
        CountDown();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (Input.GetMouseButton(0))
            {
                if (hit.collider.tag == "moles")
                {

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


            int randomMole;
            do
            {
                randomMole = UnityEngine.Random.Range(0, selectedmoles.Length);
            } while (selectedmoles[randomMole]);

            selectedmoles[randomMole] = true; // change value to true //random mole selected
            tempMole.Add(randomMole); // Store the selected mole's index in the list

            popUp(molePrefab[randomMole]); //run popUp function on selected mole
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
