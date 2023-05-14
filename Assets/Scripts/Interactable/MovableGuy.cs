using System;
using UnityEngine;

[Serializable]
public class MovableGuy : MonoBehaviour, IButtonAction
{
    public void ExecuteAction()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + 10f);
    }
}
