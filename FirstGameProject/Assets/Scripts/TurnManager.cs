using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{   
    private static TurnManager instance;
    private int currentPlayerIndex = 1;
    public float time;
    public float turntime = 10;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public bool IsItPlayerTurn(int index)
    {
        return index == currentPlayerIndex;
    }

    public static TurnManager GetInstance()
    {
        return instance;

    }

    public void ChangeTurn()
    {
        if ( currentPlayerIndex == 1)
        {
            currentPlayerIndex = 2;
        }
        else if (currentPlayerIndex == 2)
        {
            currentPlayerIndex = 1;
        }
    }

    void Update()
    {
        time += Time.deltaTime;
        if(time >= turntime)
        {
            ChangeTurn();
            time = 0;
        }
    }
}
