using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletLogic : MonoBehaviour
{
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            var enemyScript = other.gameObject.GetComponent<HunterLogic>();
            enemyScript.health -= damage;
            print("Enemy Health: " + enemyScript.health);

            if (enemyScript.health <= 0)
            {
                Destroy(gameObject);
            }
            Destroy(gameObject);
        }

        print("COLLIDED");
    }
}
