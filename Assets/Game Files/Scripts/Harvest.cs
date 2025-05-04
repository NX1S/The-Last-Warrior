using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvest : MonoBehaviour
{
    Player player;
    [SerializeField] int Points;
    void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Box"))
        {
            player.AddPoints(Points);
        }
    }
}
