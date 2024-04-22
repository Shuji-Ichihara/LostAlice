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
    }
}

[Serializable]
public class StageInfo
{
    public BaseFloor[] _baseFloors;
}