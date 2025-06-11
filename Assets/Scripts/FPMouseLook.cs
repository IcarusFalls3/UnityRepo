using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMove : MonoBehaviour
{   
    public float cameraSensitive=100f;   //�������ת������
    public Vector3 cameraRotation; // ���ڴ洢����������Ƕ�
    private Transform cameraTransform;  //�������transform
    public Transform playerTransform;   //��ҵ�transform

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = transform;
        Cursor.lockState = CursorLockMode.Locked;   //���ع��
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * cameraSensitive * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSensitive * Time.deltaTime;

        cameraRotation.x -= mouseY; //����:camera��x��Ӧ������������������Y
        cameraRotation.y += mouseX; //����:camera��y��Ӧ������������������X
        cameraRotation.x = Mathf.Clamp(cameraRotation.x, -90f, 90f); // ���Ƹ����Ƕȷ�ֹ��ת
        cameraTransform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, 0f);
        playerTransform.rotation = Quaternion.Euler(0f, cameraRotation.y, 0f);  //����ҵ�����Ҳ������ת
    }
}
