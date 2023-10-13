using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SelectPopupMole : MonoBehaviour
{
    public GameObject[] molePrefab;
    public GameObject selectedMole;
    public GameObject pastSelectedMole;
    public GameObject selectedMole2;
    public GameObject pastSelectedMole2;
    public GameObject selectedMole3;
    public GameObject pastSelectedMole3;
    public int moleIndex;
    public int moleIndex2;
    public int moleIndex3;


    Random rand = new Random();

    public Material moleSkin;
    public Material setMaterial;

    Ray ray;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        moleIndex = -1;
        moleIndex2 = -1;
        moleIndex3 = -1;
        pastSelectedMole = molePrefab[moleIndex];
        pastSelectedMole2 = molePrefab[moleIndex2];
        pastSelectedMole3 = molePrefab[moleIndex3];


        SelectMole();
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0) && hit.collider.tag == "SelectedMole")
            {
                selectedMole.GetComponent<MoleController>().Hit();
                UpdateSelectedMole();
                print(hit.collider.name);

            }
            else if((Input.GetMouseButtonDown(0) && hit.collider.tag == "SelectedMole2"))
            {
                selectedMole.GetComponent<MoleController>().Hit();
                UpdateSelectedMole();
                print(hit.collider.name);
            }
            else if ((Input.GetMouseButtonDown(0) && hit.collider.tag == "SelectedMole3"))
            {
                selectedMole.GetComponent<MoleController>().Hit();
                UpdateSelectedMole();
                print(hit.collider.name);
            }

        }
    }
    private void SelectMole()
    {
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
    }

    private void SelectMole2()
    {
        moleIndex = Random.Range(0, molePrefab.Length);
        selectedMole = molePrefab[moleIndex];
        if (selectedMole.name != pastSelectedMole.name)
        {
            selectedMole.tag = "SelectedMole2";
            selectedMole.GetComponent<Renderer>().material = setMaterial;
        }
        else
        {
            SelectMole2();
        }
    }

    private void SelectMole3()
    {
        moleIndex = Random.Range(0, molePrefab.Length);
        selectedMole = molePrefab[moleIndex];
        if (selectedMole.name != pastSelectedMole.name)
        {
            selectedMole.tag = "SelectedMole";
            selectedMole.GetComponent<Renderer>().material = setMaterial;
        }
        else
        {
            SelectMole3();
        }
    }
    public void UpdateSelectedMole()
    {
        pastSelectedMole = molePrefab[moleIndex];
        selectedMole.GetComponent<Renderer>().material = moleSkin;
        selectedMole.tag = "moles";
        moleIndex = Random.Range(0, molePrefab.Length);
        SelectMole();
    }


}
