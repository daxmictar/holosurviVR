using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    private float timer;
    public float gapTime;
    public float minRange;
    public float maxRange;
    public float spawnRadius = 10f;
    public float minSpawnRadius = 6f;
    public GameObject[] monsters;
    public Transform playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        gapTime = Random.Range(minRange, maxRange);
        timer = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > gapTime)
        {
            gapTime = Random.Range(minRange, maxRange);
            timer = 0f;
            var monster = monsters[Random.Range(0, 3)];

            var randomPosition = RandomPointInsideCircle(spawnRadius);
            var position = new Vector3(playerPosition.position.x + randomPosition.x, playerPosition.position.y, playerPosition.position.z + randomPosition.y);

            while(Vector3.Distance(position, playerPosition.position) < minSpawnRadius){
                randomPosition = RandomPointInsideCircle(spawnRadius);
                position = new Vector3(playerPosition.position.x + randomPosition.x, playerPosition.position.y, playerPosition.position.z + randomPosition.y);
            }

            var mon = Instantiate(monster, position, Quaternion.Euler(0, 0, 0));

            //var effect = Instantiate(animation, transform.position, Quaternion.Euler(0, 0, 0));

            //mon.effect = effect;
            //mon.playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
            //Debug.Log(mon);
        }
    }

    Vector2 RandomPointInsideCircle(float r){
        float angle = Random.Range(0, Mathf.PI * 2); // Random angle
        float distance = Mathf.Sqrt(Random.Range(0, 1f)) * r; // Random distance from the center

        float x = distance * Mathf.Cos(angle);
        float y = distance * Mathf.Sin(angle);

        return new Vector2(x, y);
    }
}
