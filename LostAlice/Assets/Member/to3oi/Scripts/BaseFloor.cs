using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFloor : MonoBehaviour
{
    [SerializeField] private MeshRenderer _hightGameObject;
    [SerializeField] private MeshRenderer _widthGameObject;
    
    [SerializeField] [Range(0,9)] private int _hight; 
    [SerializeField] [Range(0,9)] private int _width;
    
    void Awake()
    {
    }

    public void InitAnimation()
    {
        
    }
    
    
}
