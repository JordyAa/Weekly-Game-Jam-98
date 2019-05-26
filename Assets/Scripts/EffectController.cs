﻿using UnityEngine;

public class EffectController : MonoBehaviour
{
    public static void Init(Dragon dragon)
    {
        dragon.OnGrowTail += TailGrowEffect;
        dragon.OnDestroyTail += TailDestroyEffect;
    }
    
    private static void TailGrowEffect(Dragon dragon)
    {
        Instantiate(dragon.tailGrowEffect,
            dragon.tails[dragon.tailSize - 1].transform.position, 
            Quaternion.identity);
    }

    private static void TailDestroyEffect(Dragon dragon)
    {
        if (dragon.tailSize < 1) return;
        
        Instantiate(dragon.tailDestroyEffect, 
            dragon.tails[dragon.tailSize - 1].transform.position,
            Quaternion.identity);
    }
}