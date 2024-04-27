using System;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                Type t = typeof(T);
                _instance = (T)FindObjectOfType(t);
                if(_instance == null)
                {
#if UNITY_EDITOR
                    Debug.LogError($"{t}���A�^�b�`���Ă���I�u�W�F�N�g�͂���܂���B");
#endif
                }
            }
            return _instance;
        }
    }

    /// <summary>
    /// �p����̃N���X���� base.Awake �ŌĂяo��
    /// </summary>
    protected void Awake()
    {// ���̃Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă��邩�𒲂ׂ�B
        // �A�^�b�`����Ă���ꍇ�͔j������B
        if (this != Instance)
        {
            Destroy(this);
#if UNITY_EDITOR
            Debug.LogWarning(
                $"{typeof(T)} �͊��ɑ��̃Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă���ׁA�R���|�[�l���g��j�����܂����B" +
                $"���݃A�^�b�`����Ă���Q�[���I�u�W�F�N�g�́A{Instance.gameObject.name} �ł��B");
#endif
            return;
        }
    }
}
