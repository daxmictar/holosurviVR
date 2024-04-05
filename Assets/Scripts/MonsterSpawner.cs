using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    private float timer;
    public float gapTime;
    public float minRange;
    public float maxRange;
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

            var position = transform.position;
            position.y = 2f;
            var mon = Instantiate(monster, transform.position, Quaternion.Euler(0, 0, 0));

            //var effect = Instantiate(animation, transform.position, Quaternion.Euler(0, 0, 0));

            //mon.effect = effect;
            //mon.playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
            //Debug.Log(mon);
        }
    }
}
