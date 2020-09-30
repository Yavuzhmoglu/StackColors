using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Color myColor;
    [SerializeField] Renderer[] myRends;
    [SerializeField] bool isPlaying;
    [SerializeField] float forwardSpeed;
    Rigidbody myRgb;

    [SerializeField]float sideLerpSpeed;

    public Transform parentPickup;
    public Transform stackPositon;
    //int TempScore;
    bool atEnd;
    public float forwardForce;
    public float forceAdder;
    public float forceReducer;


    public static Action<float> Kick;

    public GameObject TouchText;
    public GameObject TapText;
    public GameObject Characther;
    Animator animator;
    void Start()
    {
        //PlayerPrefs.SetInt("Control", 0);

        myRgb = GetComponent<Rigidbody>();

        SetColor(myColor);

        animator = Characther.GetComponent<Animator>();
    }

    void Update()
    {
        if (isPlaying)
        {
            MoveForward();
        }
        if (atEnd)
        {
            forwardForce -= forceReducer * Time.deltaTime;

            if (forwardForce < 0)
            {
                forwardForce = 0;
            }

           
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (atEnd)
            {
                forwardForce += forceAdder;
                 
            }

             

        }
        if (Input.GetMouseButton(0))
        {
            if (atEnd)
            {
                return;
            }


            if (isPlaying == false)
            {
                TouchText.SetActive(false);
                isPlaying = true;

            }
            MoveSideways();

        }
    }
    void SetColor(Color colorIn)
    {
        myColor = colorIn;

        for (int i = 0; i < myRends.Length; i++)
        {
            myRends[i].material.SetColor("_Color", myColor);

        }
    }

    void MoveForward()
    {
        myRgb.velocity = Vector3.forward * forwardSpeed;
        
    }
    void MoveSideways()
    {
       Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, 100))
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(hit.point.x, transform.position.y, transform.position.z), sideLerpSpeed * Time.deltaTime); 
        }

    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Colorwall")
        {
            SetColor(other.GetComponent<ColorWall>().GetColor());
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FinishLineStarts")
        {
            atEnd = true;
            TapText.SetActive(true);

            animator.SetBool("Kick", true);
}
        if (other.tag == "FinishLineEnds")
        {
            myRgb.velocity = Vector3.zero;
            isPlaying = false;
            other.enabled = true;
            TapText.SetActive(false);
            /*
               if (PlayerPrefs.GetInt("Control") == 0)
               {

                   TempScore = GameController.instance.score*((Convert.ToInt32(forwardForce) / 50)-1);

                   GameController.instance.UpdateScore(TempScore);
                   Debug.Log(TempScore);
                   PlayerPrefs.SetInt("Control", 1);
               }
               */
            LaunchStack();
        }
       
        
        if (atEnd)
        { 
            other.enabled = true;
            return;
           
        }

        //Debug.Log(other.tag);
        if (other.tag == "Pickup")
        {
            
            Transform otherTransform = other.transform.parent;

            if(myColor==otherTransform.GetComponent<PickupStackColor>().GetColor())
            {
                GameController.instance.UpdateScore(otherTransform.GetComponent<PickupStackColor>().value);

            }
            else
            {
                GameController.instance.UpdateScore(otherTransform.GetComponent<PickupStackColor>().value*-1);
                //Destroy(other.gameObject);
                other.enabled = false;
                if (parentPickup != null)
                {
                    if (parentPickup.childCount > 1)
                    {
                        parentPickup.position -= Vector3.up * parentPickup.GetChild(parentPickup.childCount - 1).localScale.y;
                        Destroy(parentPickup.GetChild(parentPickup.childCount - 1).gameObject);

                    }
                    else
                    {
                        Destroy(parentPickup.gameObject);
                    }
                }
                return;
            }
          

           

            Rigidbody otherRB = otherTransform.GetComponent<Rigidbody>();
            otherRB.isKinematic = true;
            other.enabled = false;

            if (parentPickup == null)
            {
                parentPickup = otherTransform;
                parentPickup.position = stackPositon.position;
                parentPickup.parent = stackPositon;
               
            }
            else
            {
               
                parentPickup.position += Vector3.up *(otherTransform.localScale.y);
                otherTransform.position = stackPositon.position;
                otherTransform.parent = parentPickup;
               
            }

       
        }
        
    }
    void LaunchStack()
    {
      Camera.main.GetComponent<CameraController>().SetTarget(parentPickup);
        Kick(forwardForce);

    }

    
}
