using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;
    public GameObject CoinManager;
    public GameObject player;
    public bool point1trigger;
    public bool point2trigger;
    public GameObject Respawnpoint1;
    public GameObject Respawnpoint2;
    public float CoinCount;
    public float CoinCount2;
    public bool canMove;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Respawnpoint1 = GameObject.Find("Respawning Point 1");
        Respawnpoint2 = GameObject.Find("Respawning Point 2");
        CoinManager = GameObject.Find("CoinManager");
        player = GameObject.Find("Player");
        CoinCount = CoinManager.GetComponent<CoinManager>().CoinCount;
        CoinCount2 = CoinManager.GetComponent<CoinManager>().CoinCount2;

        if(player.GetComponent<Health>().CurrentHealth == 4f)
        {
            canMove = true;
        }
        
        else 
        {
            canMove = false;
        }

        if(CoinCount >= 10f & CoinCount2 < 10f)
        {
            point1trigger = true;
        }
        
        else if(CoinCount2 >= 10f)
        {
            point2trigger = true;
        }

        if(point2trigger == true & canMove == true && player.transform.position.x < 60)
        {
            point1trigger = false;
            player.transform.position = Respawnpoint2.transform.position;
        }

        else if(point1trigger == true & canMove == true && player.transform.position.x < -25)
        {
            player.transform.position = Respawnpoint1.transform.position;
        }

        else
        {
            return;
        }
    }
}
