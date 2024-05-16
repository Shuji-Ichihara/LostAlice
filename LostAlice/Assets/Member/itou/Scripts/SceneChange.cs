using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] Fade fade;
    public void NextScene()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Action act = () => SceneManager.LoadScene("Clear");
            float time = 1f;

            //1秒間フェードしてからにシーン移動する
            fade.FadeIn(time, act);
        }
    }

}
