using UnityEngine;

public class EffectController : MonoBehaviour
{
    private void Start()
    {
        Dragon player = GameObject.FindGameObjectWithTag("Player").GetComponent<Dragon>();
        
        player.OnGrowTail += TailGrowEffect;
        player.OnDestroyTail += TailDestroyEffect;
    }
    
    private static void TailGrowEffect(Dragon dragon)
    {
        Instantiate(dragon.tailGrowEffect,
            dragon.tails[dragon.tailSize - 1].transform.position, 
            Quaternion.identity);
    }

    private static void TailDestroyEffect(Dragon dragon)
    {
        Instantiate(dragon.tailDestroyEffect, 
            dragon.tails[dragon.tailSize - 1].transform.position,
            Quaternion.identity);
    }
}
