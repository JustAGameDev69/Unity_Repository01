using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedSpace : MonoBehaviour
{

    private Vector3 Last_OnPos;
    // Start is called before the first frame update
    void Start()
    {
        Last_OnPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!IsOnRoad())
        {
            OutRoad();
        }
        
    }

    private bool IsOnRoad()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.2f);

        foreach (Collider collider in colliders)
        {
            if(collider.CompareTag("Road"))
            {
                Last_OnPos = transform.position;
                return true;
            }
        }

        return false;
    }

    private void OutRoad()
    {
        transform.position = Last_OnPos;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
