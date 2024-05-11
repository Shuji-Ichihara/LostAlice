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
        _nowStage.text = "第" + _stageCount + "ステージ";
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
            _nowStage.text = "第" + _stageCount + "ステージ";
        }
        
        if (_times/*本来は壁が止まった判定*/)
        {
            _time -= Time.deltaTime;
            _WallTime.text = "" + _time.ToString("f0");
            if (_time <= 0f)
            {
                _times = false;
                _time = 0f;
                _WallTime.text = "" + _time;
                /*本来は壁が止まった判定をfalse*/
            _time = _maxtime;
                /*壁が動き出す判定をture*/
            }
        }

    }
}
