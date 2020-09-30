using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupStackColor : MonoBehaviour
{

     public int value;

    [SerializeField]Color pickUpColor;
    public Rigidbody pickUpRB;
    public Collider pickUpCollider;


    private void OnEnable()
    {
        PlayerController.Kick += Mykick;
    }
    private void OnDisable()
    {
        PlayerController.Kick -= Mykick;
    }
    void Mykick(float forceSent)
    {
        transform.parent = null;
        pickUpCollider.enabled = true;
        pickUpRB.isKinematic = false;
        pickUpRB.AddForce(new Vector3(0, forceSent, forceSent));


    }
    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.SetColor("_Color", pickUpColor);
    }

    
  public Color GetColor()
    {
        return pickUpColor;
    }
}
