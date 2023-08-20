using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRoadLimit : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Road"))
        {
            rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
            rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Road"))
        {
            rb.constraints |= ~RigidbodyConstraints.FreezePositionX;
            rb.constraints |= ~RigidbodyConstraints.FreezePositionZ;
        }
    }
}
