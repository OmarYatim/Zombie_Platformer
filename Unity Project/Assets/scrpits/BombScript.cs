using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    float distance;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
            gameObject.GetComponent<Animator>().SetBool("Explode",true);
    }
    void Death()
    {
        distance = Mathf.Abs(transform.position.x-Player.transform.position.x);
        if(distance<2.4f){Hit();}
    }
    void Hit()
    {
        Player.GetComponent<PlayerControls>().lives--;
    }
    void Destroy(){
        Destroy(gameObject);
    }
}
