using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4.0f;

    private Player _player;

    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.Log("Player is Null!");
        }

        _anim = GetComponent<Animator>();
        if(_anim == null)
        {
            Debug.Log("Animator is Null!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y <= -6.0f)
        {
            float randomX = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(randomX, 8.0f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (_player != null)
            {
                _player.Damage();
            }

            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.8f);
        }
        if (other.tag == "Laser")
        {

            _anim.SetTrigger("OnEnemyDeath"); 
            _speed= 0;
            Destroy(this.gameObject, 2.8f);
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.AddScore(10);
            }
        }
    }
}
