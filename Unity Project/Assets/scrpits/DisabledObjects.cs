using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisabledObjects : MonoBehaviour
{
    // Start is called before the first frame update
  
    [SerializeField]
    GameObject Trap,Zombie1,Zombie2,Spikes,Vampire1,Vampire2,Golem,Player;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Trap.transform.position.x - Player.transform.position.x < 13)
        {
            Trap.SetActive(true);
        }
        if(Zombie1.transform.position.x - Player.transform.position.x < 13)
        {
            Zombie1.SetActive(true);
        }
        if(Zombie2.transform.position.x - Player.transform.position.x < 13)
        {
            Zombie2.SetActive(true);
        }
        if(Spikes.transform.position.x - Player.transform.position.x < 13)
        {
            Spikes.SetActive(true);
        }
        if(Vampire1.transform.position.x - Player.transform.position.x < 13)
        {
            Vampire1.SetActive(true);
        }
        if(Vampire2.transform.position.x - Player.transform.position.x < 13)
        {
            Vampire2.SetActive(true);
        }
        if(Golem.transform.position.x - Player.transform.position.x < 13)
        {
            Golem.SetActive(true);
        }
        
    }
}
