using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemCollector : MonoBehaviour
{
    private int _cherries = 0;
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        text.text = "Cherries Collected: 0";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cherry"))
        {
            Destroy(other.gameObject);
            _cherries++;
            Debug.Log($"Collected Cherries: {_cherries}");
            text.text = $"Cherries Collected: {_cherries}";
        }
    }
}
