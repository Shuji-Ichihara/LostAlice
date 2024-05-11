using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [SerializeField]
    private Text _nowStage;
    [SerializeField]
    private Text _WallTime;
    private int _stageCount;
    public bool _goal;
    public bool _times;
    public float _maxtime;
    public float _time;
    // Start is called before the first frame update
    void Start()
    {
        _time = _maxtime;
        _stageCount++;
        _nowStage.text = "��" + _stageCount + "�X�e�[�W";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            _goal = true;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            _times = true;
        }
        if (_goal)
        {
            _goal = false;
            _stageCount++;
            _nowStage.text = "��" + _stageCount + "�X�e�[�W";
        }
        
        if (_times/*�{���͕ǂ��~�܂�������*/)
        {
            _time -= Time.deltaTime;
            _WallTime.text = "" + _time.ToString("f0");
            if (_time <= 0f)
            {
                _times = false;
                _time = 0f;
                _WallTime.text = "" + _time;
                /*�{���͕ǂ��~�܂��������false*/
            _time = _maxtime;
                /*�ǂ������o�������ture*/
            }
        }

    }
}
