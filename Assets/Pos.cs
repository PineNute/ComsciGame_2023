using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pos : MonoBehaviour
{
    public Transform player; // Reference to the player GameObject

    void Update()
    {
        if (player != null)
        {
            // Set the Follower's position to the player's position
            transform.position = player.position;
        }
    }
}

