using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

public class StageAnimationManager : MonoBehaviour
{
    //SingleTone
    public static StageAnimationManager Instance
    {
        get
        {
            if(Instance == null)
            {
                var obj = new GameObject("StageAnimationManager");
                Instance = obj.AddComponent<StageAnimationManager>();
            }
            return Instance;
        }
        private set => Instance = value;
    }
    
    [SerializeField]
    private  Book _book;
    
    [SerializeField]
    private float _waitTime = 15.0f;
    
    [SerializeField]
    private List<StageInfo> _stageInfos = new List<StageInfo>();

    async void Start()
    {
        _book.WaitTime = _waitTime;
        
        await UniTask.Delay(TimeSpan.FromSeconds(3));
        await _book.BookOpen();
        await next();
    }

    [ContextMenu("next")]
    public async UniTask next()
    {
        await _book.NextPageInit();
        await _book.NextPageWait();
        
        List<UniTask> tasks = new List<UniTask>();  
        tasks.Add(_book.NextPageEnd());
        /*
        tasks.Add(
             Task.Run(() =>
            {
                foreach (var floorType1 in _stageInfos[0].FloorType1s)
                {
                    floorType1.gameObject.SetActive(false);
                }
            }).AsUniTask());
            */

        await tasks;
    }
}

[Serializable]
public class StageInfo
{
    public FloorType1[] FloorType1s;
}