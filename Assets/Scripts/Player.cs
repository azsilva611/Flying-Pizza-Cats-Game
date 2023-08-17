using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;

    void Start()
    {
        transform.position = new Vector3(0, -1.8f, 0);
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null )
        {
            Debug.LogError("The Spawn Manager is Null");
        }
    }

    void Update()
    {
        MoveSelf();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
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

    void FireLaser()
    {
        Vector3 laserOffset = new Vector3(transform.position.x, transform.position.y + .95f, 0);
        //firing cooldown
        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, laserOffset, Quaternion.identity);
    }

    public void Damage()
    {
        _lives--;
        if (_lives == 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
    /*private void OnTriggerEnter(Collider other)
    {
        
    }*/
}
