using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIndex : MonoBehaviour
{
    [SerializeField] private int playerIndex = 0;

    public int GetPlayerIndex()
    {
        return playerIndex;
    }
}
