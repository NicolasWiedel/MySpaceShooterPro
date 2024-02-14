using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _trippleShotPrefab;
    [SerializeField]
    private Vector3 _laserOffset = new Vector3(0, 1.05f, 0);
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    bool _trippleShotAktive = false;

    private SpawnManager _spawnManager;

    private float _canFire = -1f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if(_spawnManager == null)
        {
            Debug.LogError("SpawnManager is NULL!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    private void CalculateMovement()
    {
        // Player movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
       
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        // Player bounds
        transform.position = new Vector3(transform.position.x,
            Mathf.Clamp(transform.position.y, -3.75f, 1), 
            0);

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    private void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_trippleShotAktive)
        {
            Instantiate(_trippleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + _laserOffset, Quaternion.identity);
        }
          
    }

    public void Damage()
    {
        _lives--;

        if (_lives < 1) 
        {
            _spawnManager.OnPlayerDead();
            Destroy(this.gameObject);
        }
    }

    public void SetTrippleShotActive()
    {
        if(!_trippleShotAktive) 
        {
            _trippleShotAktive = true;
            StartCoroutine(TrippleShotPowerDownRoutine());
        }
    }

    IEnumerator TrippleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _trippleShotAktive = false;
    }
}
