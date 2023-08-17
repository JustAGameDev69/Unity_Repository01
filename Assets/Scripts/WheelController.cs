using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] private WheelCollider FrontRightWheel;
    [SerializeField] private WheelCollider FrontLeftWheel;
    [SerializeField] private WheelCollider RearRightWheel;
    [SerializeField] private WheelCollider RearLeftWheel;

    public float Acceleration = 500f;
    public float Breaking_force = 300f;
    public float Max_turnAngle = 15f;

    private float Current_accleration = 0f;
    private float Current_breakforce = 0f;
    private float Current_turnAngle = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Current_accleration = Acceleration * Input.GetAxis("Vertical");         //Di chuyển xe đi thẳng hay đi lùi


        if (Input.GetKey(KeyCode.Space))        //Nhấn Space để phanh
        {
            Current_breakforce = Breaking_force;
        }
        else Current_breakforce = 0f;

        //Tăng tốc cho bánh trước.
        FrontRightWheel.motorTorque = Current_accleration;
        FrontLeftWheel.motorTorque = Current_accleration;

        //Phanh cho cả bốn bánh
        FrontRightWheel.brakeTorque = Current_breakforce;
        FrontLeftWheel.brakeTorque = Current_breakforce;
        RearLeftWheel.brakeTorque = Current_breakforce;
        RearRightWheel.brakeTorque = Current_breakforce;

        //Phần Hải quay xe

        Current_turnAngle = Max_turnAngle * Input.GetAxis("Horizontal");

        FrontRightWheel.steerAngle = Current_turnAngle;
        FrontLeftWheel.steerAngle = Current_turnAngle;

    }
}
