using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscEvents
{
    public event Action onCoinCollected;

    public void coinsCollected()
    {
        if (onCoinCollected != null)
        {
            onCoinCollected();
        }
    }

    public event Action onGemCollected;

    public void GemCollected()
    {
        if (onGemCollected != null)
        {
            onGemCollected();
        }
    }
}