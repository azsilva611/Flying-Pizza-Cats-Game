using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizzacat : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.5f;
    [SerializeField]
    private GameObject _slicePrefab;
    [SerializeField]
    private float _throwRate = 0.2f;
    private float _canThrow = -1f;

    void Start()
    {
        transform.position = new Vector3(0, -1.8f, 0);
    }

    void Update()
    {
        MoveSelf();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canThrow)
        {
            ThrowSlice();
        } 
    }

    void MoveSelf()
    {
        //create direction and movement for player object based on player movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        //Determine the bounds of moving the player
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void ThrowSlice()
    {
        Vector3 sliceOffset = new Vector3(transform.position.x, transform.position.y + 0.8f, 0);
        //throwing cooldown
        _canThrow = Time.time + _throwRate;
        Instantiate(_slicePrefab, sliceOffset, Quaternion.identity);
    }
}
