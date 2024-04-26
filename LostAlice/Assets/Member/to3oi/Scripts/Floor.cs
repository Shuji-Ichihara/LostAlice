using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Floor : MonoBehaviour
{
    private Animator _animator;
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }
}
