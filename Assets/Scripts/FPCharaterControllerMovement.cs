using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCharaterControllerMovement : MonoBehaviour
{
    public float speed;             //�ƶ��ٶ�
    public float walkspeed = 10f;   //�����ٶ�
    public float runspeed = 20f;    //�����ٶ�
    private float Gravity = 9.8f;   //����
    private CharacterController characterController;    //��ҿ���
    private Transform characterTransform;               //���transform
    private Vector3 movementDirection;                  //�˶�ʸ��
    private float JumpHeight = 2f;                      //��Ծ��
    private float CrouchHeight = 1f;                     //�¶׸߶�

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded)
        {
            float X = Input.GetAxis("Horizontal");  //��ȡˮƽx�������ֵ,Horizontal = A/D
            float Y = Input.GetAxis("Vertical");    //��ȡ��ֱy�������ֵ,Vertical = W/S

            // λ�Ʒ���
            // ����ֱ��new Vector3(h, 0, v).normalized����ʱ�ο�ϵ����������ϵ
            // movementDirection = (characterTransform.right * X + characterTransform.forward * Y); //��CharacterController������ϵΪ׼,
            movementDirection = characterTransform.TransformDirection(X, 0, Y); //�ڶ��ַ�������API����������ϵת��Ϊ��CharacterController������ϵ

            if(Input.GetButtonDown("Jump")) //Ĭ������Jump = �ո��
            {
                movementDirection.y = JumpHeight;   //����y�������Ծ�߶�(��Ծ����,��ͬ���ܲ�ʱ���ٶ�
            }
            speed = Input.GetKey(KeyCode.LeftShift) ? runspeed : walkspeed; //��Ԫ��������ж��Ƿ���Shift�����ı��ٶ�

            if(Input.GetKeyDown(KeyCode.LeftControl))
            {
                //Ϊʲô����Ҫ��Э��??? ���� ��Ϊ���Ǹ���һ�����������ƶ����������Ƕ׿��ǵ�����ײ��ĸı䣬����ײ�岻���������ı��С
                //Э����ʲô???
                StartCoroutine(DoCrouch());
            }

        }
        movementDirection.y -= Gravity * Time.deltaTime;    //���y���������ע��������һ������ֵ����������ʽF=1/2*g*t^2
        characterController.Move( movementDirection * speed * Time.deltaTime);    //��CharacterController�Դ���Move��������ƶ�
    }

    private IEnumerator DoCrouch()
    {
        while(true) //10:00
        {

        }
    }
}
