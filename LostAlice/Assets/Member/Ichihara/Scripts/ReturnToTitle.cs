using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnToTitle : MonoBehaviour
{
    [SerializeField]
    private Button _button = null;

    private bool _doOnce = false;

    private void Start()
    {
        _doOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        _button.onClick.AddListener(
             delegate
             {
                 MySceneManager.Instance.ChangeScene(SceneType.Title);
                 _doOnce = true;
             });
    }
}
