using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap2 : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameObject;

    public float _time = 0;
   
    void  OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Trap());
        }
    }
    private IEnumerator Trap()
    {
        // ‚Q•b‘Ò‹@
        yield return new WaitForSeconds(_time);
        Destroy(this.gameObject);
    }
}
