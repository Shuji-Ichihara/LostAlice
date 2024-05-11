using UnityEngine;

public class Test_SE : MonoBehaviour
{
    //サウンドenumを参照
    public Name Se_Name;

    //サウンドマネージャー
    private GameObject SoundMA;

    private void Start()
    {
        SoundMA = GameObject.FindGameObjectWithTag("SoundManager");
    }

    public void SeButton()
    {
        //指定された名前を送る
        SoundMA.GetComponent<SoundManager>().PlaySound(Se_Name);
    }
}
