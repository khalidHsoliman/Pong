using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class is generating pickups with random positions, random time intervals, random pickup prefab
/// </summary>
public class RandomGenerator : MonoBehaviour {

    // pickups 
    public GameObject[] pickups;

    // position range to respawn
    public float minX = -20.0f;
    public float maxX =  20.0f;
    public float minY = -10.0f;
    public float maxY =  10.0f;

    // time to respawn
    [Range(0, 10)]
    public float MinSpawnInterval = 8.0f;
    [Range(0, 10)]
    public float MaxSpawnInterval = 10.0f;

    // timer
    private float timeToRespawn = 0.0f;
    private float SpawnInterval = 0.0f; 

	// Update is called once per frame
	void Update () {
        timeToRespawn += Time.deltaTime;
        SpawnInterval = Random.Range(MinSpawnInterval, MaxSpawnInterval)- Time.timeSinceLevelLoad * 1/50; 

        if(timeToRespawn >= SpawnInterval)
        {
            timeToRespawn = 0.0f;

            // instantiating the pickup 
            GameObject pickup = Instantiate(pickups[Random.Range(0, pickups.Length)],
                new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0.0f),
                Quaternion.identity) as GameObject;

            StartCoroutine("DestroyObject", pickup); 
        }

       // Debug.Log(SpawnInterval);
	}


    /// <summary>
    /// This coroutine is destroying the pickup and make a fading effect 
    /// </summary>
    /// <param name="pickup"></param>
    /// <returns></returns>
    IEnumerator DestroyObject(GameObject pickup)
    {
        while(pickup.GetComponent<SpriteRenderer>().color.a > 0)
        {
            yield return new WaitForSeconds(0.018f);
            pickup.GetComponent<SpriteRenderer>().color -= new Color32(0, 0, 0, 1);
        }

        Destroy(pickup);
    }
}
