using UnityEngine;

public class Test_SE : MonoBehaviour
{
    //�T�E���henum���Q��
    public Name Se_Name;

    //�T�E���h�}�l�[�W���[
    private GameObject SoundMA;

    private void Start()
    {
        SoundMA = GameObject.FindGameObjectWithTag("SoundManager");
    }

    public void SeButton()
    {
        //�w�肳�ꂽ���O�𑗂�
        SoundMA.GetComponent<SoundManager>().PlaySound(Se_Name);
    }
}
