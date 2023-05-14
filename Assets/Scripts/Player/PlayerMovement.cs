using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private BoxCollider2D _coll;
    private IPlayerMovement _playerMovement;
    private PlayerIndex _playerIndex;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private LayerMask[] jumpableLayers;

    [SerializeField] private AudioSource runningSFX;
    [SerializeField] private AudioSource jumpingSFX;
    
    private static readonly int AnimStateTag = Animator.StringToHash("state");
    private enum AnimationState
    {
        Idle = 0,
        Running = 1,
        Jumping = 2,
        Falling = 3,
    }

    private void Start()
    {
        _coll = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _playerIndex = GetComponent<PlayerIndex>();

        _playerMovement = InitializePlayerMovement();
    }


    private void Update()
    {
        HandleMovement();
        AnimatePlayer();
    }

    private void HandleMovement()
    {
        _rb.velocity = new Vector2(_playerMovement.GetXAxis() * moveSpeed, _rb.velocity.y);
        
        if (_playerMovement.GetJump() && IsGrounded())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            jumpingSFX.Play();
        }
    }


    private void AnimatePlayer()
    {
        var animState = AnimationState.Idle;
        if (_playerMovement.GetXAxis() > 0f)
        {
            _spriteRenderer.flipX = false;
            animState = AnimationState.Running;
            if(!runningSFX.isPlaying && IsGrounded()) runningSFX.Play();
        }
        else if (_playerMovement.GetXAxis() < 0f)
        {
            _spriteRenderer.flipX = true;
            animState = AnimationState.Running;
            if(!runningSFX.isPlaying && IsGrounded()) runningSFX.Play();
        }
        
        if (_rb.velocity.y > .1f)
        {
            animState = AnimationState.Jumping;
        } 
        else if (_rb.velocity.y < -.1f)
        {
            animState = AnimationState.Falling;
        }
        
        _animator.SetInteger(AnimStateTag, (int) animState);
    }

    private bool IsGrounded()
    {
        foreach (var layer in jumpableLayers)
        {
            if (Physics2D.BoxCast(_coll.bounds.center, _coll.bounds.size, 0f, Vector2.down, .1f, layer))
            {
                return true;
            }
        }

        return false;
    }
    
    private IPlayerMovement InitializePlayerMovement()
    {
        switch (_playerIndex.GetPlayerIndex())
        {
            case 0:
                return new Player1Movement();
            case 1:
                return new Player2Movement();
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

internal class Player1Movement : IPlayerMovement
{
    public float GetXAxis()
    { 
        return Input.GetAxisRaw("Horizontal");
    }

    public bool GetJump()
    {
        return Input.GetButtonDown("Jump");
    }
}

internal class Player2Movement : IPlayerMovement
{
    /**
     * The axis is handled this way so if the player presses both buttons at the same time
     * he does not move instead of moving left or right
     */
    public float GetXAxis()
    {
        var curAxis = 0;
        if (Input.GetKey("j"))
        {
            curAxis--;
        }
        if (Input.GetKey("l"))
        {
            curAxis++;
        }

        return curAxis;
    }

    public bool GetJump()
    {
        return Input.GetKeyDown("i");
    }
}

internal interface IPlayerMovement
{
    float GetXAxis();

    bool GetJump();
    
}