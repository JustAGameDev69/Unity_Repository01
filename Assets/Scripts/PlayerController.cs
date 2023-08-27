using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private WheelCollider FrontRightWheel;
    [SerializeField] private WheelCollider FrontLeftWheel;
    [SerializeField] private WheelCollider RearRightWheel;
    [SerializeField] private WheelCollider RearLeftWheel;

    public float Acceleration = 500f;
    public float Breaking_force = 300f;
    public float Max_turnAngle = 30f;
    public TextMeshProUGUI player_Coin;
    public Slider healthSlider;

    private float Current_accleration = 0f;
    private float Current_breakforce = 0f;
    private float Current_turnAngle = 0f;

    private int player_current_health;
    private int Player_health = 100;
    private int Player_fuel = 100;
    private int Player_coin = 0;




    public Transform[] Way_points;      //Tạo mảng các điểm là các waypoint trên đường
    public float Move_Speed = 10f;      //Tốc độ di chuyển
    public float Rotation_Speed = 100f;     //Tốc độ quay của xe

    public int CurrentWaypoint_Index;       //Điểm waypoint hiện tại

    public DriveMode mode = DriveMode.Manual;
    public bool Allow_Input = true;

    [SerializeField] private float force_power = 10f;

    private Rigidbody rb;





    // Start is called before the first frame update


    public enum DriveMode
    {
        Manual,
        Automatic,
        Physic
    }

    void Start()
    {

        CurrentWaypoint_Index = 0;

        rb = GetComponent<Rigidbody>();

        player_current_health = Player_health;
        HealthBar();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (mode == DriveMode.Automatic)            //Nếu mode là automatic thì tự động chạy bằng waypoint.
        {
            MoveTo_Waypoint();
            if (Is_Waypoint())
            {


                CurrentWaypoint_Index++;
                if (CurrentWaypoint_Index >= Way_points.Length)
                {
                    CurrentWaypoint_Index = 0;
                }
            }
        }
        else if (mode == DriveMode.Manual && Allow_Input)
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

            HealthBar();
        }
        else if (mode == DriveMode.Physic)
        {
            Physic_Move();
        }
    }

    void MoveTo_Waypoint()          //Di chuyển đến waypoint tiếp theo trên đường
    {
        Vector3 Target_Pos = Way_points[CurrentWaypoint_Index].position;
        Vector3 Direction = Target_Pos - transform.position;

        transform.Translate(Direction.normalized * Move_Speed * Time.deltaTime, Space.World);

        //Quay xe 
        Quaternion Target_Rotation = Quaternion.LookRotation(Direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Target_Rotation, Rotation_Speed * Time.deltaTime);
    }


    private bool Is_Waypoint()
    {
        Vector3 Target_Pos = Way_points[CurrentWaypoint_Index].position;
        float Distance = Vector3.Distance(transform.position, Target_Pos);

        return Distance < 0.1f;
    }

    void Physic_Move()
    {
        float horizontal_Input = Input.GetAxis("Horizontal");
        float vertical_Input = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal_Input, 0, vertical_Input);

        rb.AddForce(movement * force_power);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Damage"))
        {
            player_current_health = player_current_health - 10;       //10% damage take in everytime collide
            if (player_current_health == 0)
            {
                Debug.Log("Car are explode!");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HealthBonus"))
        {
            if (player_current_health > 90)
            {
                player_current_health += (100 - player_current_health);
                other.gameObject.SetActive(false);
            }
            else
            {
                player_current_health += 10;
                other.gameObject.SetActive(false);
            }
        }
        else if (other.gameObject.CompareTag("Treasure"))
        {
            Player_coin += 10;
            PlayerCoin();
            Destroy(other.gameObject);
        }
    }

    void PlayerCoin()
    {
        player_Coin.text = Player_coin.ToString();
    }

    void HealthBar()
    {
        healthSlider.value = player_current_health;
    }



}
