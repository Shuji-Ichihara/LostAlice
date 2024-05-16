using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalZone : MonoBehaviour
{
    private bool _doOnce = false;

    private void Start()
    {
        _doOnce = false;
    }

    public async UniTask RotateGoalZone()
    {
        do
        {
            transform.RotateAround(Vector3.zero, Vector3.forward, 90.0f * Time.deltaTime / 3);
            await UniTask.Yield();
        }
        while (transform.position.y > 0.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(DeadZone.PlayerTag))
        {
            if (_doOnce == false)
            {
                GameSceneManager.Instance.ClearStage();
                _doOnce = true;
            }
        }
    }
}
