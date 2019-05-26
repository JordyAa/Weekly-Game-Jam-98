using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField] private GameObject tailGrowEffect = null;
    [SerializeField] private GameObject tailDestroyEffect = null;

    private void Start()
    {
        Player.OnGrowTail += TailGrowEffect;
        Player.OnDestroyTail += TailDestroyEffect;
    }
    
    private void TailGrowEffect(Player player)
    {
        Instantiate(tailGrowEffect,
            player.tails[player.tailSize - 1].transform.position, 
            Quaternion.identity);
    }

    private void TailDestroyEffect(Player player)
    {
        Instantiate(tailDestroyEffect, 
            player.tails[player.tailSize - 1].transform.position,
            Quaternion.identity);
    }
}
