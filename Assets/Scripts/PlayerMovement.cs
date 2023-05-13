using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private float _xAxis;
    [SerializeField] private float jumpVelocity = 12f;
    [SerializeField] private float moveVelocity = 10f;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
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
        _rigidbody2D.velocity = new Vector2(_xAxis * moveVelocity, _rigidbody2D.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpVelocity);
        }
    }

    private void HandleAnimations()
    {
        if (_xAxis > 0f)
        {
            _spriteRenderer.flipX = false;
            //_animator.SetBool(PlayerAnimTag.Running.AsTag(), true);
        }
        else if (_xAxis < 0f)
        {
            _spriteRenderer.flipX = true;
            //_animator.SetBool(PlayerAnimTag.Running.AsTag(), true);
        }
        else
        {
            //_animator.SetBool(PlayerAnimTag.Running.AsTag(), false);
        }
    }

}

internal enum AnimationState
{
    Idle,
    Running,
    Jumping,
    Falling,
}

/**
 * Helper method to encapsulate changes to the player animation tags 
 */
 /*
internal static class PlayerAnimTagExtensions 
{
    // TODO compile time checking does not seem to be possible, unit test should be written instead
    public static string AsTag(this PlayerAnimTag tag)
    {
        return tag switch
        {
            PlayerAnimTag.Running => "Running",
            _ => throw new ArgumentOutOfRangeException(nameof(tag), tag, null)
        };
    }
}
*/
