using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : SingletonMonoBehaviour<GameSceneManager>
{
    [SerializeField]
    private Transform _spawnPlayerPoint = null;
    [SerializeField]
    private GameObject _playerObject = null;

    // 本のページが垂直である秒数
    [SerializeField]
    private float _pageTopTimeCount = 0f;
    // ステージの総数
    [SerializeField]
    private int _gameStageMax = 5;
    public int GameStageMax => _gameStageMax;
    // 現在のステージのカウント
    private int _gameStageCount = 1;
    public int GameStageCount => _gameStageCount;


    // ゲーム開始の判定
    private bool _isStartedGame = false;
    // ゲーム終了の判定
    private bool _isEndGame = false;

    new private void Awake()
    {
        base.Awake();
        // シーンが読み込まれた時、フラグをゲーム開始時の状態にする、
        _isStartedGame = false;
        _isEndGame = false;
        _gameStageCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //if(/*ステージをクリアした状態を取得*/)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ClearStage();
            Debug.Log(_gameStageCount);
        }
        // 全てのステージをクリアしたら、ゲーム終了フラグを true にする
        if (_gameStageCount < _gameStageMax)
        {
            _isEndGame = true;
        }
        if (_isEndGame == true)
        {
            // ここにシーン遷移の関数を記述する
        }
    }

    /// <summary>
    /// ステージをクリアした時に現在プレイしているステージを更新する
    /// </summary>
    private void ClearStage()
    {
        _gameStageCount++;
    }

    /// <summary>
    /// 本のページが垂直である間の時間経過を行う
    /// </summary>
    public void PageTopCount()
    {
        _pageTopTimeCount -= Time.deltaTime;
    }

    /// <summary>
    /// プレイヤーを生成する
    /// </summary>
    public void PlayerSpawn()
    {
        Instantiate(_playerObject, _spawnPlayerPoint.position, Quaternion.identity);
        _isStartedGame = true;
    }
}
