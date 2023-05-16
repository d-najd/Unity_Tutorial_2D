using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private AudioSource itemCollectSFX;
    [SerializeField] private ScoreCounter scoreCounter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cherry"))
        {
            Destroy(other.gameObject);
            itemCollectSFX.Play();
            scoreCounter.CherryCollected(GetComponent<PlayerIndex>().GetPlayerIndex());
        }
    }
}
