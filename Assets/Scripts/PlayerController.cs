using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Transform[] Way_points;      //Tạo mảng các điểm là các waypoint trên đường
    public float Move_Speed = 10f;      //Tốc độ di chuyển
    public float Rotation_Speed = 100f;     //Tốc độ quay của xe

    public int CurrentWaypoint_Index;       //Điểm waypoint hiện tại

    public DriveMode mode = DriveMode.Manual;
    public bool Allow_Input = true;

    // Start is called before the first frame update

   public enum DriveMode
    {
        Manual,
        Automatic
    }

    void Start()
    {
        CurrentWaypoint_Index = 0;
    }

    // Update is called once per frame
    void Update()
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
            float horizontal_Input = Input.GetAxis("Horizontal");
            float vertical_Input = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(horizontal_Input, 0, vertical_Input);
            transform.Translate(movement * Move_Speed * Time.deltaTime);
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

}
