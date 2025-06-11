using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Gravity = 9.8f;
    public float speed = 10f;
    public float jumpHeight = 2f;
    private Rigidbody characterRigidBody;
    private Transform characterTransform;
    private bool isGrounded;
    private void Start()
    {
        characterTransform = transform; //��ʼ��transform������
        characterRigidBody = GetComponent<Rigidbody>(); //��ʼ������
    }
    private void FixedUpdate()
    {
        if(isGrounded)
        {
            float tmp_Horizontal = Input.GetAxis("Horizontal");   //ˮƽAD
            float tmp_Vertical = Input.GetAxis("Vertical");       //��ֱWS

            Vector3 tmp_CurrentDirection = new Vector3(tmp_Horizontal, 0, tmp_Vertical);
            tmp_CurrentDirection = characterTransform.TransformDirection(tmp_CurrentDirection); //������������ת��������������
            tmp_CurrentDirection *= speed;  //����*�ƶ��ٶ�

            //����������ֵ����һ��W��һֱ��ǰ��
            Vector3 tmp_CurrentVelocity = characterRigidBody.velocity;  //��ȡ��ǰ�����ٶ�
            Vector3 tmp_VelocityChange = tmp_CurrentDirection - tmp_CurrentVelocity;    //ʩ�ӵ��ٶ� - �����ٶ�
            tmp_VelocityChange.y = 0;   //��ֹ����y����ƶ�
            characterRigidBody.AddForce(tmp_VelocityChange, ForceMode.VelocityChange);    //���ŵ�һ�������ķ���ʩ�������������ڶ�������Ϊ��������

            if(Input.GetButtonDown("Jump"))
            {   
                //�ı�����Y���ٶ�
                characterRigidBody.velocity = new Vector3(tmp_CurrentVelocity.x, Mathf.Sqrt(2 * jumpHeight * Gravity), tmp_CurrentVelocity.z);
            }
        }
        //���뿪����ʱ����Ҹ���ʩ����������ʼ״̬�����Ǳ�ȡ���ģ�
        characterRigidBody.AddForce(new Vector3(0, -Gravity, 0));    //��FixedUpdate()�п��Բ��ó�Time.deltatime,��Ϊ�÷����ǹ̶�0.02sִ��һ�Σ���Update()������
    }

    //һֱ����ײ�尤��ʱ����
    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    //�뿪��ײ��ʱ����
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}