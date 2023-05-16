using System;
using UnityEngine;

[Serializable]
public class MovableGuy : MonoBehaviour, IButtonAction
{
    private bool _moveAnimTriggered;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Vector3 targetPos;

    private Animator _animator;
    private static readonly int WakeUp = Animator.StringToHash("wakeUp");

    public void ExecuteAction()
    {
        _animator.SetBool(WakeUp, true);
    }

    public void TriggerMoveAnim()
    {
        _moveAnimTriggered = true;
    }
    
    private void Update()
    {
        if (_moveAnimTriggered)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPos,
                moveSpeed * Time.deltaTime
            );

            // Goal reached
            if (Vector2.Distance(
                    transform.position,
                    targetPos) < .1f
               )
            {
                _moveAnimTriggered = false;
            }
        }
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
}
