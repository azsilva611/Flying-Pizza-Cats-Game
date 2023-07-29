using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slice : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10.0f;


    void Update()
    {
        MoveSelf();
    }

    void MoveSelf()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if (transform.position.y > 8)
        {
            Destroy(gameObject);
        }
    }
}
