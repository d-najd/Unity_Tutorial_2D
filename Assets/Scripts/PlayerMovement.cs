using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private float _xAxis;
    [FormerlySerializedAs("jumpVelocity")] [SerializeField] private float jumpForce = 12f;
    [FormerlySerializedAs("moveVelocity")] [SerializeField] private float moveSpeed = 10f;
    
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

        if (Input.GetButtonDown("Jump"))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
        }
    }

    private void HandleAnimations()
    {
        var animState = AnimationState.Idle;
        if (_xAxis > 0f)
        {
            _spriteRenderer.flipX = false;
            animState = AnimationState.Running;
        }
        else if (_xAxis < 0f)
        {
            _spriteRenderer.flipX = true;
            animState = AnimationState.Running;
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
}