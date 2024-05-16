using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class StageAnimationManager : MonoBehaviour
{
    //SingleTone
    public static StageAnimationManager Instance
    {
        get
        {
            if (Instance == null)
            {
                var obj = new GameObject("StageAnimationManager");
                Instance = obj.AddComponent<StageAnimationManager>();
            }
            return Instance;
        }
        private set => Instance = value;
    }

    [SerializeField]
    private Book _book;

    [SerializeField]
    private float _waitTime = 15.0f;

    [SerializeField]
    private List<StageInfo> _stageInfos = new List<StageInfo>();

    async void Start()
    {
        _book.WaitTime = _waitTime;

        await UniTask.Delay(TimeSpan.FromSeconds(3));
        await _book.BookOpen();

        // 市原追記
        GameSceneManager.Instance.PlayerSpawn();

        //for (int i = 0; i < 3; i++)
        //{
        //    await next();
        //}
        // 市原追記
        for (int i = 0; i < GameSceneManager.Instance.GameStageMax; i++)
        {
            await next();
        }
        await UniTask.Delay(TimeSpan.FromSeconds(1));

        //本を閉じる
        await _book.CloseAnimation();
    }

    [ContextMenu("next")]
    public async UniTask next()
    {
        Debug.Log("init");
        await _book.NextPageInit();
        Debug.Log("wait");
        await _book.NextPageWait();
        Debug.Log("end");
        await _book.NextPageEnd();
    }
}

[Serializable]
public class StageInfo
{
    public FloorType1[] FloorType1s;
}