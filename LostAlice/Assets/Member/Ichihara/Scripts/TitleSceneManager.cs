using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleSceneManager : SingletonMonoBehaviour<TitleSceneManager>
{
    [SerializeField]
    private Button _startButton = null;

    private bool _doOnce = false;

    private void Start()
    {
        _doOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        _startButton.onClick.AddListener(
             delegate
             {
                 MySceneManager.Instance.ChangeScene(SceneType.GameScene);
                 _doOnce = true;
             });
    }
}
