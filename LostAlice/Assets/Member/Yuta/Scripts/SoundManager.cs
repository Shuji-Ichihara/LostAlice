using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //���y���X�g
    [SerializeField] List<AudioClip> SoundNumber = new List<AudioClip>();

    //�I�[�f�B�I�\�[�X
    [SerializeField] AudioSource SeAudio;

    //Se�̐ݒ�ƍĐ�
    private void Se(AudioClip clip)
    {
        SeAudio.clip = clip;

        if (clip == null)
        {
            return;
        }

        SeAudio.PlayOneShot(clip);
    }

    //���y�Đ����\�b�h
    public void PlaySound(Name name)
    {
        //��������O�𐔎��ɕϊ�
        Se(SoundNumber[(int)name]);
    }

    //�I�u�W�F�N�g���r���Ŕj������Ȃ��p�ɂ��鏈��
    void Start()
    {
        //�I�u�W�F�N�g���r���Ŕj������Ȃ��p�ɂ��鏈��
        //(�����^�O�̃I�u�W�F�N�g����������j������p�ɂȂ��Ă�B)
        GameObject soundManager = CheckOtherSoundManager();
        bool checkObject = soundManager != null && soundManager != gameObject;

        if (checkObject)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    //�V�[�����ɁA�����^�O�̃I�u�W�F�N�g���������𒲂ׂ郁�\�b�h
    GameObject CheckOtherSoundManager()
    {
        return GameObject.FindGameObjectWithTag("SoundManager");
    }
}
