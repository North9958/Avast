using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleHitTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Mole;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision other)
    {
        Mole.GetComponent<MoleController>().hitTriggered = true;
    }
}
