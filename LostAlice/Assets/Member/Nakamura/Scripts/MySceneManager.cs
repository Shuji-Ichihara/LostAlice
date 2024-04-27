using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Scene�̎��
/// </summary>
public enum SceneType
{
    Title,
    GameScene,
    EndScene
}

[Serializable]
public class SerializableDictionary<TKey, TValue>
{
    [SerializeField] 
    private TKey sceneType;
    [SerializeField] 
    private TValue sceneAsset;

    public TKey Key => sceneType;
    public TValue Value => sceneAsset;
}

public class MySceneManager : MonoBehaviour
{
    private MySceneManager Instance;

    [SerializeField,Header("Scene�̃^�C�v��Scene�̎w�聦BuildSetting�K�{")]
    private SerializableDictionary<SceneType, SceneAsset>[] _loadSceneKeyPair;

    private Dictionary<SceneType, string> _loadScenes = new Dictionary<SceneType, string>();

    private bool _settingScene=false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            if (!_settingScene)
            {
                // _loadSceneKeyPair�̔z��̐�����_loadScenes��
                for (int i = 0; i < _loadSceneKeyPair.Length; i++)
                {
                    Debug.Log($"{_loadSceneKeyPair.Length}");
                    _loadScenes.Add(_loadSceneKeyPair[i].Key, _loadSceneKeyPair[i].Value.name);
                    Debug.Log($"SceneType:{_loadSceneKeyPair[i].Key},SceneName:{_loadSceneKeyPair[i].Value}");

                }
                _settingScene = true;
            }
        }
        else
        {
            Destroy(gameObject);
        }



    }

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        //Test�p
        if(Input.GetKeyDown(KeyCode.A))
        { ChangeScene(SceneType.Title); }
        if (Input.GetKeyDown(KeyCode.S))
        { ChangeScene(SceneType.GameScene); }
        if (Input.GetKeyDown(KeyCode.D))
        { ChangeScene(SceneType.EndScene); }

    }

    private void ChangeScene(SceneType sceneType)
    {
        if (_loadScenes[sceneType] != SceneManager.GetActiveScene().name)
        {
            SceneManager.LoadScene(_loadScenes[sceneType]);
        }
    }
}
