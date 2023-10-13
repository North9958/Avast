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
    public int moleIndex;

    Random rand = new Random();

    public Material moleSkin;
    public Material setMaterial;

    Ray ray;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        moleIndex = 0;
        pastSelectedMole = molePrefab[moleIndex];
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
    public void UpdateSelectedMole()
    {
        pastSelectedMole = molePrefab[moleIndex];
        selectedMole.GetComponent<Renderer>().material = moleSkin;
        selectedMole.tag = "moles";
        moleIndex = Random.Range(0, molePrefab.Length);
        SelectMole();
    }


}
