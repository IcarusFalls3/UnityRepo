using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FPS/Footstep Audio Data")]     //???�����˵�???
public class FootStepAudioDate : ScriptableObject   //Monobehavior������ص�Object�ϣ���ScriptableObject����Ҫ���ڵ�Object��
{
    public List<FootstepAudio> FootstepAudios = new List<FootstepAudio>();
}

[System.Serializable]       //???���л�???
public class FootstepAudio
{
    public string Tag;  //��ͬ���ʵı�ǩ
    public List<AudioClip> AudioClips = new List<AudioClip>();  //��ͬ�����ϵ������б�
    public float Delay=0.3f; //���ҽ��Ⱥ�ȵ�ʱ����
}