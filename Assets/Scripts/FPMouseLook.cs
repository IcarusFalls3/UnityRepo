using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMove : MonoBehaviour
{   
    public float cameraSensitive=100f;   //摄像机旋转灵敏度
    public Vector3 cameraRotation; // 用于存储摄像机俯仰角度
    private Transform cameraTransform;  //摄像机的transform
    public Transform playerTransform;   //玩家的transform

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = transform;
        Cursor.lockState = CursorLockMode.Locked;   //隐藏光标
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * cameraSensitive * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSensitive * Time.deltaTime;

        cameraRotation.x -= mouseY; //俯仰:camera的x对应鼠标输入世界坐标轴的Y
        cameraRotation.y += mouseX; //左右:camera的y对应鼠标输入世界坐标轴的X
        cameraRotation.x = Mathf.Clamp(cameraRotation.x, -90f, 90f); // 限制俯仰角度防止倒转
        cameraTransform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, 0f);
        playerTransform.rotation = Quaternion.Euler(0f, cameraRotation.y, 0f);  //让玩家的左右也跟着旋转
    }
}
