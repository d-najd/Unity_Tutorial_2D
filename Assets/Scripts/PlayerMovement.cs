using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private BoxCollider2D _coll;
    private float _xAxis;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private LayerMask jumpableGround;

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
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _coll = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        _xAxis = Input.GetAxisRaw("Horizontal");

        HandleMovement();
        HandleAnimations();
    }

    private void HandleMovement()
    {
        _rb.velocity = new Vector2(_xAxis * moveSpeed, _rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            jumpingSFX.Play();
        }
    }

    private void HandleAnimations()
    {
        var animState = AnimationState.Idle;
        if (_xAxis > 0f)
        {
            _spriteRenderer.flipX = false;
            animState = AnimationState.Running;
            if(!runningSFX.isPlaying && IsGrounded()) runningSFX.Play();
        }
        else if (_xAxis < 0f)
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
        return Physics2D.BoxCast(_coll.bounds.center, _coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        // return Physics2D.Raycast(_coll.bounds.center, Vector2.down, (_coll.bounds.size.y / 2f) + 0.1f, jumpableGround);
    }
}