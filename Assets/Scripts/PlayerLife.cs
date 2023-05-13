using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private static readonly int Dead = Animator.StringToHash("dead");

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Spike"))
        {
            _rb.bodyType = RigidbodyType2D.Static;
            _animator.SetTrigger(Dead);
        }
    }

    private void RestartLevel()
    {
        Debug.Log("Restarting Level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
