using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Represents the Marine's GameObject.
    public GameObject player;

    // The locations the aliens will spawn from, declared as an array
    // because there are multiple locations.
    public GameObject[] spawnPoints;

    // Represents the alien prefab.
    public GameObject alien;

    // Determines how many aliens can be on screen at one time.
    public int maxAliensOnScreen;

    // Represents the total amount of aliens the player must defeat in order
    // to win the game.
    public int totalAliens;

    // Determines how many aliens spawn per spawining event.
    public int aliensPerSpawn;

    // Controls the rate at which the aliens spawn at.
    public float minSpawnTime;

    // Same as above.
    public float maxSpawnTime;

    // Will track total number of aliens on screen.
    private int aliensOnScreen = 0;

    // Will track time between spawn events.
    private float generatedSpawnTime = 0;

    // Track the milliseconds since the last spawn.
    private float currentSpawnTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Accumulates the amount of time that's passed between each
        // frame update.
        currentSpawnTime += Time.deltaTime;

        // Spawn time randomizer.
        if (currentSpawnTime > generatedSpawnTime)
        {
            // Resets the timer after a spawn occurs.
            currentSpawnTime = 0;

            // This is the spawn time randomizer.
            generatedSpawnTime = UnityEngine.Random.Range(minSpawnTime, maxSpawnTime);
        }

        // Determines whether to spawn.
        if (aliensPerSpawn > 0 && aliensOnScreen < totalAliens)
        {
            // Creates an array to keep track of where the aliens are spawned
            // each wave, stops it from double spawning the aliens.
            List<int> previousSpawnLocations = new List<int>();

            // Limits the number of aliens that can be spawned by the number 
            // of spawn points.
            if (aliensPerSpawn > spawnPoints.Length)
            {
                aliensPerSpawn = spawnPoints.Length - 1;
            }
            // Makes sure thatif aliens exceeds the maximum, the amount of spawns 
            // will be reduced.
            aliensPerSpawn = (aliensPerSpawn > totalAliens) ?
                aliensPerSpawn - totalAliens : aliensPerSpawn;

            // This is the actual spawning code.
            // Loop iterates once for each spawned alien.
            for (int i = 0; i < aliensPerSpawn; i++)
            {
                // Checks to see if the amount of aliens on the screen is less
                // than the maximum, if so it increments the total screen amount.
                if (aliensOnScreen < maxAliensOnScreen)
                {
                    aliensOnScreen += 1;
                    // 1: Generated spawn point number, set to -1 to indicate the spawn point
                    // has not yet been selected.
                    int spawnPoint = -1;

                    // 2: This loop runs until it finds a spawn point or the spawn is no longer -1.
                    while (spawnPoint == -1)
                    {
                        // 3: Produces a random number as possible spawn points.
                        int randomNumber = UnityEngine.Random.Range(0, spawnPoints.Length - 1);

                        // 4: Checks previous spawn points for an active spawn point, if there is no
                        // match, then you have your spawn point.
                        if (!previousSpawnLocations.Contains(randomNumber))
                        {
                            previousSpawnLocations.Add(randomNumber);
                            spawnPoint = randomNumber;
                        }
                    }
                    // Grabs spawn point based on index.
                    GameObject spawnLocation = spawnPoints[spawnPoint];

                    // Creates an instance of any prefab passed into it.
                    GameObject newAlien = Instantiate(alien) as GameObject;

                    // Positions the alien at the spawn point.
                    newAlien.transform.position = spawnLocation.transform.position;

                    // This gives a reference to the Alien script.
                    Alien alienScript = newAlien.GetComponent<Alien>();

                    // Sets target to Space Marine's current position.
                    alienScript.target = player.transform;

                    // Rotates the alien towards the Hero using the alien's y-axis position.
                    Vector3 targetRotation = new Vector3(player.transform.position.x,
                        newAlien.transform.position.y, player.transform.position.z);
                    newAlien.transform.LookAt(targetRotation);
                }
            }
        }
      
    }
}
