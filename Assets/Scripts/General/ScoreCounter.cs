using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private static Dictionary<int, int> CherryByPlayer = new Dictionary<int, int>();
    private static int _overallCherries = 0;
    [SerializeField] private TextMeshProUGUI text;

    private const string CherryText = "Cherries Collected: ";

    private void Start()
    {
        text.text = CherryText + $"{_overallCherries}";
    }

    public void CherryCollected(int playerIndex)
    {
        _overallCherries++;
        if (CherryByPlayer.TryGetValue(playerIndex, out var value))
        {
            CherryByPlayer.Remove(playerIndex);
            CherryByPlayer.Add(playerIndex, value + 1);
        }
        else
        { 
            CherryByPlayer.Add(playerIndex, 1);
        }

        text.text = CherryText + $"{_overallCherries}";
    }
}
