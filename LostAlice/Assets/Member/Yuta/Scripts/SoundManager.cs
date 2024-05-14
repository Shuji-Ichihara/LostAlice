using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //音楽リスト
    [SerializeField] List<AudioClip> SoundNumber = new List<AudioClip>();

    //オーディオソース
    [SerializeField] AudioSource SeAudio;

    //Seの設定と再生
    private void Se(AudioClip clip)
    {
        SeAudio.clip = clip;

        if (clip == null)
        {
            return;
        }

        SeAudio.PlayOneShot(clip);
    }

    //音楽再生メソッド
    public void PlaySound(Name name)
    {
        //貰った名前を数字に変換
        Se(SoundNumber[(int)name]);
    }

    //オブジェクトが途中で破棄されない用にする処理
    void Start()
    {
        //オブジェクトが途中で破棄されない用にする処理
        //(同じタグのオブジェクトがあったら破棄する用になってる。)
        GameObject soundManager = CheckOtherSoundManager();
        bool checkObject = soundManager != null && soundManager != gameObject;

        if (checkObject)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    //シーン内に、同じタグのオブジェクトが無いかを調べるメソッド
    GameObject CheckOtherSoundManager()
    {
        return GameObject.FindGameObjectWithTag("SoundManager");
    }
}
