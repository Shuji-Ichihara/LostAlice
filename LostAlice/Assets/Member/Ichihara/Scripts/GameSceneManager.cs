using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : SingletonMonoBehaviour<GameSceneManager>
{
    [SerializeField]
    private Transform _spawnPlayerPoint = null;
    [SerializeField]
    private GameObject _playerObject = null;

    private GameObject _playerObjectInGame = null;
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
    private bool _isGameClear = false;
    //
    private bool _isGameOver = false;

    new private void Awake()
    {
        base.Awake();
        // シーンが読み込まれた時、フラグをゲーム開始時の状態にする、
        _isStartedGame = false;
        _isGameClear = false;
        _isGameOver = false;
        _gameStageCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // 全てのステージをクリアしたら、ゲーム終了フラグを true にする
        if (_gameStageCount == _gameStageMax)
            _isGameClear = true;
        else if (_gameStageCount < _gameStageMax && _isGameOver == true)
            MySceneManager.Instance.ChangeScene(SceneType.GameOver);

        if (_isGameClear == true)
        {
            MySceneManager.Instance.ChangeScene(SceneType.EndScene);
        }
    }

    /// <summary>
    /// ステージをクリアした時に現在プレイしているステージを更新する
    /// </summary>
    public void ClearStage()
    {
        _gameStageCount++;
    }

    /// <summary>
    /// プレイヤーを生成する
    /// </summary>
    public void PlayerSpawn()
    {
        _playerObjectInGame = Instantiate(_playerObject, _spawnPlayerPoint.position, Quaternion.identity);
        _isStartedGame = true;
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
