using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;   //�ƶ��ٶ�
    public Vector3 moveDirection;   //�ƶ�����
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()     //��Roll a Ballʹ�ø����˶���ͬ����FPS_demoʹ��CharacterController�������ƶ�(�Դ�����)
    {
        float h = Input.GetAxis("Horizontal");  //ˮƽx��
        float v = Input.GetAxis("Vertical");    //��ֱy��

        moveDirection = new Vector3(h, 0, v).normalized;

        characterController.Move(moveDirection * speed * Time.deltaTime);
    }

}