using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public static readonly string PlayerTag = "Player";

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(PlayerTag))
            MySceneManager.Instance.ChangeScene(SceneType.GameOver);
    }
}
