using UnityEngine;

public class EffectController : MonoBehaviour
{
    public static void Init(Dragon dragon)
    {
        dragon.OnGrowTail += TailGrowEffect;
        dragon.OnDestroyTail += TailDestroyEffect;
        dragon.OnDeath += DeathEffect;
    }
    
    private static void TailGrowEffect(Dragon dragon)
    {
        Instantiate(dragon.tailGrowEffect,
            dragon.tails[dragon.tails.Count - 1].transform.position, 
            Quaternion.identity);
    }

    private static void TailDestroyEffect(Dragon dragon)
    {
        if (dragon.tails.Count > 0)
        {
            Instantiate(dragon.tailDestroyEffect,
                dragon.tails[dragon.tails.Count - 1].transform.position,
                Quaternion.identity);
        }
    }

    private static void DeathEffect(Dragon dragon)
    {
        Instantiate(dragon.deathEffect, 
            dragon.head.transform.position,
            Quaternion.identity);
    }
}
