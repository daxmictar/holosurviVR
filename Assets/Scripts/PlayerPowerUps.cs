using System.ComponentModel;
using UnityEngine;

public class PlayerPowerUps : MonoBehaviour
{
    public GameObject player;
    private PlayerStats playerStats;
    public bool splitAttack = false;
    public bool singleAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindPlayer();
        Debug.Log("Player Found -> ", player);
        playerStats = player.GetComponent<PlayerStats>();
        Debug.Log("PlayerStats found ->", playerStats);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdatePlayerPowers() 
    {

    }

    private GameObject FindPlayer() {
        var playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject == null) { return null; }
        return playerObject;
    }
}
