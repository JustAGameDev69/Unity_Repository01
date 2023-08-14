using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour

{
    public Transform target;    //Đối tượng được set để camera theo dõi
    public float smoothSpeed = 1f;      //Tốc độ di chuyển của camera
    public Vector3 offSet;      //Khoảng cách camera tới object

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPos = target.position + offSet;      //Vị trí của đối tượng mà camera cần đến

            Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);        //Tính toán vị trí của camera, dùng Lerp để làm mượt

            transform.position = smoothedPos;       //Cật nhật vị trí mới

            transform.LookAt(target);       //Hàm này giúp camera luôn nhìn về target
        }
    }
}
