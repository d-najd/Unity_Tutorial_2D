using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;
using Component = UnityEngine.Component;

public class ButtonClickable : MonoBehaviour
{
    /**
     * Must be of type IButtonAction
     * TODO find a way to add constraints or unit test every component that uses this somehow?
     */
    [SerializeField]
    private Component[] _buttonActions;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {   
            PressButton();
        }
    }

    private void PressButton()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 1f);

        foreach (var component in _buttonActions)
        {
            var buttonAction = (IButtonAction) component;
            buttonAction.ExecuteAction();
            
            //buttonAction.ExecuteAction();
        }
    }
}
