using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    // Start is called before the first frame update
    new AudioSource audio;
    bool isRunning;
    [SerializeField]
    GameObject Player;
    float distance;
    void Start()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        audio = audios[1];
    }

    // Update is called once per frame
    void Update()
    {
        distance = gameObject.transform.position.x-Player.transform.position.x;
    }
       public IEnumerator Attack()
	{
        isRunning = true;
        yield return new WaitForSeconds(1.5f);
        GetComponent<Animator>().SetInteger("Anim", 1);
        audio.Play();
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        GetComponent<Animator>().SetInteger("Anim", 0);
        yield return new WaitForSeconds(0.5f);
        isRunning = false;
	
	}
    void Death()
    {
        if(Mathf.Abs(distance) < 2.5f)
        {
            GetComponent<Collider2D>().enabled = false;
            Player.GetComponent<PlayerControls>().lives--;
            GetComponent<Collider2D>().enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            if(gameObject.transform.position.x < other.gameObject.transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            if(gameObject.transform.position.x > other.gameObject.transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            if(!isRunning){StartCoroutine(Attack());}
        }
    }
    void Disappear()
    {
        Destroy(gameObject.GetComponent<Collider2D>());
    }
}
