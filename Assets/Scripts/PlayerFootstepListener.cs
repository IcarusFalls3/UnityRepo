using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepListener : MonoBehaviour
{
    public FootStepAudioDate footstepAudioDate;
    public AudioSource footstepAudioSource;

    private CharacterController characterController;
    private Transform footstepTransform;
    private float nextPlayTime;

    //��μ���Ƿ񷢳����������� ��ɫ�˶����Ƚϴ�
    //��μ������Ƿ��ƶ��� ���� Physical API���
    //��η�����Ӧ���ʵ������� ���� Physical API���

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        footstepTransform = transform;
    }

    private void FixedUpdate()
    {
        if (characterController.isGrounded)
        {
            if(characterController.velocity.normalized.magnitude > 0.1f)  //����Ƿ��ƶ�
            {
                nextPlayTime += Time.deltaTime; //�ۼ��´β�����Ƶ��ʱ��
                bool isHit = Physics.Linecast(footstepTransform.position,   //����ҵ�����
                    footstepTransform.position + Vector3.down * (characterController.height / 2 + characterController.skinWidth - characterController.center.y),          //���¾���
                    out RaycastHit tmp_HitInfo);                            //�����ײ��

                //Debug.DrawLine(footstepTransform.position,
                //    footstepTransform.position + Vector3.down * (characterController.height / 2 + characterController.skinWidth - characterController.center.y),
                //    Color.red,
                //    0.25f);

                //����ҵ�λ������Χ�����ƶ�����
                if (isHit)
                {
                    //����Ƶ��Ϣ���Ҷ�Ӧ��Ԫ��
                    foreach(var tmp_AudioElement in footstepAudioDate.FootstepAudios)
                    {
                        if(tmp_HitInfo.collider.CompareTag(tmp_AudioElement.Tag))   //����������:�����ײ���tag����Ƶ�б����ж�Ӧ��tag
                        {
                            if (nextPlayTime >= tmp_AudioElement.Delay) //���ﵽ��һ�β�����Ƶ���ӳ�ʱ���Ų���,���ﻹ���ԸĽ���Running/Crouchingʹ�ò�ͬ��Delay(δ�����)
                            {
                                //TODO:���Ŷ�Ӧ������
                                int tmp_AudioCount = tmp_AudioElement.AudioClips.Count; //��ȡ��Ӧtag��Clips��
                                int tmp_AudioIndex = UnityEngine.Random.Range(0, tmp_AudioCount);   //��0-Count�������ѡһ��
                                AudioClip tmp_footstepAudioClip = tmp_AudioElement.AudioClips[tmp_AudioIndex];  //��ȡ���ѡȡ��Clip
                                footstepAudioSource.clip = tmp_footstepAudioClip;   //����ƵClip��ֵ��AudioSource
                                footstepAudioSource.Play(); //����AudioSource
                                nextPlayTime = 0;   //���ʱ�����
                                break;  //��������ѭ��
                            }
                        }
                    }
                }
            }
        }
    }

}
