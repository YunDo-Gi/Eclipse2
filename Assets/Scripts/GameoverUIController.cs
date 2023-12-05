using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverUIController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial position of the UI in front of the player
        transform.position = player.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the position of the UI to stay in front of the player
        transform.position = player.position + offset;
    }
}
