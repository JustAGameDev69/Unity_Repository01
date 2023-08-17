using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public GameObject CameraFolder;
    public Transform[] camlocation;
    public int LocationIndicator = 2;

    [Range(0, 1)] public float smoothTime = .5f;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        CameraFolder = target.transform.Find("CAMERA").gameObject;
        camlocation = CameraFolder.GetComponentsInChildren<Transform>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {

        }
    }
}