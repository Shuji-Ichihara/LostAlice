using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Animator))]
public class Book : MonoBehaviour
{
    private Animator _animator;
    
    private static readonly int _openAnim = Animator.StringToHash("OpenAnim");
    private static readonly int _nextInit = Animator.StringToHash("NextInit");
    private static readonly int _nNextWait = Animator.StringToHash("NextWait");
    private static readonly int _nextEnd = Animator.StringToHash("NextEnd");
    private static readonly int _waitSpeed = Animator.StringToHash("WaitSpeed");
    private static readonly int _closeAnim = Animator.StringToHash("CloseAnim");
    private static readonly int _end = Animator.StringToHash("End");

    private bool _isOpen = false;

    public float WaitTime = 15.0f;
    
    [SerializeField]
    private GameObject[] _pages;

    private int _pageIndex = 0;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public async UniTask BookOpen()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(5));
        _animator.SetTrigger(_openAnim);
        _isOpen = true;
        await AnimationWait();
    }

    [ContextMenu("NextPageInit")]
    public async UniTask NextPageInit()
    {
        if (!_isOpen)
        { return; }
        
        _animator.SetTrigger(_nextInit);
        for (int i = 0; i < _pages.Length; i++)
        {
            _pages[i].SetActive(i == _pageIndex);
        }
        
        await AnimationWait();
        Debug.Log("NextPageInit");
    }

    [ContextMenu("NextPageWait")]
    public async UniTask NextPageWait()
    {
        if (!_isOpen)
        { return; }

        _animator.SetFloat(_waitSpeed, 1/WaitTime);
        _animator.SetTrigger(_nNextWait);
        await AnimationWait();
        Debug.Log("NextPageWait");

    }

    [ContextMenu("NextPageEnd")]
    public async UniTask NextPageEnd()
    {
        if (!_isOpen)
        { return; }

        _animator.SetTrigger(_nextEnd);
        await AnimationWait();
        Debug.Log("NextPageEnd");

        foreach (var page in _pages)
        {
            page.SetActive(false);
        }
        _pageIndex++;
    }
    
    
    [ContextMenu("CloseAnimation")]
    public async UniTask CloseAnimation()
    {
        if (!_isOpen)
        { return; }

        _animator.SetTrigger(_closeAnim);
        await AnimationWait();
        Debug.Log("CloseAnimation");
    }
    
    
    [ContextMenu("EndSet")]
    public void EndSet()
    {
        if (!_isOpen)
        { return; }

        _animator.SetBool(_end, true);
    }

    private async UniTask AnimationWait()
    {
        //TODO: UniRx or R3インポート後に待機処理を変更
        await UniTask.WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
    }
}
