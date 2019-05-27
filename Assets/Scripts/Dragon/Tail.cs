﻿using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tail : MonoBehaviour
{
    private bool hasTarget;
    private Transform _target;
    public Transform target
    {
        private get => _target;
        set
        {
            if (hasTarget) throw new Exception("Tail already has a target!");
            
            _target = value;
            GetComponent<DistanceJoint2D>().connectedBody = _target.GetComponent<Rigidbody2D>();
            _target.transform.parent.GetComponent<Dragon>().OnDestroyTail += DropEdible;
            hasTarget = true;
        }
    }

    private static void DropEdible(Dragon dragon)
    {
        if (dragon.tails.Count > 0 && dragon.dropOnDeath != null && Random.Range(0f, 1f) > 0.6f)
        {
            Instantiate(dragon.dropOnDeath,
                dragon.tails[dragon.tails.Count - 1].transform.position,
                Quaternion.identity);
        }
    }

    private void Update()
    {
        if (hasTarget == false) return;

        Transform t = transform;
        Vector3 pos = t.position;
        Vector3 targetPos = target.position;

        t.right = targetPos - pos;

        float angle = Mathf.Atan2(targetPos.y - pos.y, targetPos.x - pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f + angle));
    }
}
