using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleController : MonoBehaviour
{

    public GameObject selectMole;
    public float moveDistance = 5f;
    [SerializeField] private float moveSpeed = .5f;
    float downTimer = 0;
    float upTimer = 0;
    int randoLimit = 5;
    [SerializeField] private int randoLimitMax = 7;
    [SerializeField] private float maxUpTime = 5;
    private float moveTimer = 0;
    [SerializeField] private float moveTimeLimit = .75f;
    float randoLimitTimer = 0;
    bool canMove;
    private bool isHit = true;
    private bool isUp = false;
    private bool isMovingUp = false;
    private Vector3 originalPosition;


    Ray ray;
    RaycastHit hit;

    // Start is called before the first frame update

    void Start()

    {

        this.tag = "moles";
        originalPosition = transform.position;

    }

    // Update is called once per frame

    void Update()
    {        
        
        //randoLimitTimer += Time.deltaTime;

        //SetRandoLimit();

        TickTimers();
        if(isUp)
        {
            isMovingUp = false;
            if (downTimer >= maxUpTime)

            {

                MoveDown();

                isUp = false;

                downTimer = 0;

            }
        }
        else if (this.tag == "SelectedMole" || this.tag == "SelectedMole2")
        {

            Popup();
            isMovingUp = true;
            if (moveTimer >= moveTimeLimit)

            {

                isMovingUp = false;

                isUp = true;

                moveTimer = 0;

            }

        }
        else if (this.tag == "moles")
        {
            isMovingUp = false;
        }
        
    }

    private void FixedUpdate()

    {

    }

    public void Popup()

    {
        Debug.Log("Popup script STARTED for " + this);
        transform.position = Vector3.Lerp(this.transform.position, this.transform.position + new Vector3(0, moveDistance, 0), moveSpeed * Time.deltaTime);

    }

    private void MoveDown()

    {

        transform.position = originalPosition;

    }

    //private void SetRandoLimit()

    //{

    // if(randoLimitTimer >= 1)

    // {

    // randoLimitTimer = 0;

    // randoLimit = Random.Range(2, randoLimitMax);

    // }

    //}

    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Hammer"&& this.tag == "SelectedMole")
        {
            Hit();
            selectMole.GetComponent<SelectPopupMole>().UpdateSelectedMole();
            
        }
        else if(other.gameObject.tag == "Hammer" && this.tag == "SelectedMole2")
        {
            Hit();
            selectMole.GetComponent<SelectPopupMole>().UpdateSelectedMole2();
        }
    }

    private void TickTimers()

    {

        if (isMovingUp)

        {

            moveTimer += Time.deltaTime;

        }

        else if (isUp)

        {

            downTimer += Time.deltaTime;

        }

    }

    public void Hit()
    {
        isHit = true;
        isUp = false;
        //give the palyer a point?
        MoveDown();
        isMovingUp = false;
        downTimer = 0;
        upTimer = 0;
        moveTimer = 0;
        this.tag = "moles";
    }

   
}
