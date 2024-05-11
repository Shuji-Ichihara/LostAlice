using Cysharp.Threading.Tasks;
using R3;
using R3.Triggers;
using Unity.VisualScripting;
using UnityEngine;

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
    private static readonly int _waitOpenDefault = Animator.StringToHash("WaitOpenDefault");
    private static readonly int _end = Animator.StringToHash("End");

    private bool _isOpen = false;
    private bool _isEnd = false;

    public float WaitTime = 15.0f;

    [SerializeField] private GameObject[] _pages;

    private int _pageIndex = 0;

    /// <summary>
    /// アニメーション実行中はtrue
    /// </summary>
    private bool _doAnimating = false;

    private bool _doBook_OpenDefaultAnimation = false;
    private bool _doBook_NextPageInitAnimation = false;
    private bool _doBook_NextPageWaitAnimation = false;
    private bool _doBook_NextPageEndAnimation = false;

    private bool _doBook_OpenAnimation = false;
    private bool _doBook_CloseAnimation = false;

    void Awake()
    {
        _animator = GetComponent<Animator>();

        ObservableStateMachineTrigger[] triggers =
            _animator.GetBehaviours<ObservableStateMachineTrigger>();

        foreach (var trigger in triggers)
        {
            // Stateの終了イベント
            trigger
                .OnStateExitAsObservable()
                .Subscribe(onStateInfo =>
                {
                    AnimatorStateInfo info = onStateInfo.StateInfo;

                    if (info.IsName("Base Layer.Book_Open") ||
                        info.IsName("Base Layer.Book_Close"))
                    {
                        _doAnimating = false;
                    }
                }).AddTo(this);

            //特定のアニメーションはアニメーション終了時にステートを移行しないので時間で判定
            trigger.OnStateUpdateAsObservable()
                .Subscribe(onStateInfo =>
                {
                    AnimatorStateInfo info = onStateInfo.StateInfo;
                    //Debug.Log($"info.normalizedTime : {info.normalizedTime}");
                    if (info.normalizedTime >= 1.0f)
                    {
                        if (info.IsName("Base Layer.Book_OpenDefault"))
                        {
                            _doBook_OpenDefaultAnimation = false;
                        }                        
                        if (info.IsName("Base Layer.Book_NextPageInit"))
                        {
                            _doBook_NextPageInitAnimation = false;
                        }

                        if (info.IsName("Base Layer.Book_NextPageWait"))
                        {
                            _doBook_NextPageWaitAnimation = false;
                        }

                        if (info.IsName("Base Layer.Book_NextPageEnd"))
                        {
                            _doBook_NextPageEndAnimation = false;
                        }
                    }
                }).AddTo(this);
        }
    }

    [ContextMenu("BookOpen")]
    public async UniTask BookOpen()
    {
        _animator.SetTrigger(_openAnim);
        _isOpen = true;
        await AnimationWait();
    }

    [ContextMenu("NextPageInit")]
    public async UniTask NextPageInit()
    {
        if (!_isOpen)
        {
            return;
        }

        await AnimationWaitOpenDefault();
        
        _animator.SetTrigger(_nextInit);
        for (int i = 0; i < _pages.Length; i++)
        {
            _pages[i].SetActive(i == _pageIndex);
        }

        await UniTask.Yield();

        await AnimationWaitNextPageInit();
    }

    [ContextMenu("NextPageWait")]
    public async UniTask NextPageWait()
    {
        if (!_isOpen)
        {
            return;
        }

        await UniTask.Yield();

        _animator.SetFloat(_waitSpeed, 1 / WaitTime);
        _animator.SetTrigger(_nNextWait);
        await AnimationWaitNextPageWait();
    }

    [ContextMenu("NextPageEnd")]
    public async UniTask NextPageEnd()
    {
        if (!_isOpen)
        {
            return;
        }

        await UniTask.Yield();

        _animator.SetTrigger(_nextEnd);
        _animator.SetBool(_waitOpenDefault, true);
        await AnimationWaitNextPageEnd();

        foreach (var page in _pages)
        {
            page.SetActive(false);
        }

        _pageIndex++;

        _animator.SetBool(_waitOpenDefault, false);
    }


    [ContextMenu("CloseAnimation")]
    public async UniTask CloseAnimation()
    {
        if (!_isOpen)
        {
            return;
        }

        _animator.SetTrigger(_closeAnim);
        await AnimationWait();
        Debug.Log("CloseAnimation");
    }


    [ContextMenu("SetEnd")]
    public void SetEnd()
    {
        if (!_isOpen)
        {
            return;
        }

        _isEnd = true;
        _animator.SetBool(_end, true);
    }

    private async UniTask AnimationWait()
    {
        _doAnimating = true;
        await UniTask.Yield();
        await UniTask.WaitUntil(() => !_doAnimating);
    }

    private async UniTask AnimationWaitOpenDefault()
    {
        _doBook_OpenDefaultAnimation = true;
        await UniTask.Yield();
        await UniTask.WaitUntil(() => !_doBook_OpenDefaultAnimation);
    }
    private async UniTask AnimationWaitNextPageInit()
    {
        _doBook_NextPageInitAnimation = true;
        await UniTask.Yield();
        await UniTask.WaitUntil(() => !_doBook_NextPageInitAnimation);
    }

    private async UniTask AnimationWaitNextPageWait()
    {
        _doBook_NextPageWaitAnimation = true;
        await UniTask.Yield();
        await UniTask.WaitUntil(() => !_doBook_NextPageWaitAnimation);
    }

    private async UniTask AnimationWaitNextPageEnd()
    {
        _doBook_NextPageEndAnimation = true;
        await UniTask.Yield();
        await UniTask.WaitUntil(() => !_doBook_NextPageEndAnimation);
    }
}