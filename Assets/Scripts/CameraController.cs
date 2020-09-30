using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform Target;
    float DeltaZ;

    void Start()
    {

        DeltaZ = transform.position.z - Target.position.z;

        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, Target.position.z + DeltaZ);
        
    }
    public void SetTarget(Transform newTarget)
    {
        Target = newTarget;
       
      
    }
}
