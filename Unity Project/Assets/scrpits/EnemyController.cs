using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource Att,Die;
    float xMin;
    float xMax;
    bool isFacingRight;
    bool isRunning = false;
    bool Moving = true;
    float Distance;

    bool Appear;

    [SerializeField]
    GameObject HealthBar,Player;
    void Start()
    {
        if(gameObject.tag == "Zombie")
        {
            AudioSource[] audios = GetComponents<AudioSource>();
            Att = audios[0];
            Die = audios[1];
        }
        if(gameObject.tag == "Boss")
        {
            AudioSource[] audios = GetComponents<AudioSource>();
            Att = audios[2];
        }
        if(gameObject.tag == "Vampire")
        {
            AudioSource[] audios = GetComponents<AudioSource>();
            Att = audios[1];
        }
        xMax = transform.position.x + 3;
        xMin = transform.position.x - 3;
    }

    // Update is called once per frame
    void Update()
    {
        Appear = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("appear");
        Distance = gameObject.transform.position.x-Player.transform.position.x;
        if(Moving && !isRunning && !Appear)
            Mouvement();
    }
    public IEnumerator Attack()
	{
        isRunning = true;
        GetComponent<Animator>().SetInteger("Anim", 1);
        yield return new WaitForSeconds(1.5f);
        GetComponent<Animator>().SetInteger("Anim", 2);
        Att.Play();
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        GetComponent<Animator>().SetInteger("Anim", 1);
        yield return new WaitForSeconds(0.5f);
        isRunning = false;
	}

    void Death()
    {
        if(Mathf.Abs(Distance) < 2.5f)
        {
                GetComponent<Collider2D>().enabled = false;
                Player.GetComponent<PlayerControls>().lives--;
                GetComponent<Collider2D>().enabled = true;
        }
    }

    void Dying()
    {
        Die.Play();
    }

    void Disappear()
    {
        Destroy(gameObject.GetComponent<Collider2D>());
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
            Moving = false;
        if(other.gameObject.tag == "Player" && gameObject.tag == "Boss")
        {
            HealthBar.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
            Moving = true;
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            if(gameObject.transform.position.x < other.gameObject.transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                isFacingRight = true;
            }
            if(gameObject.transform.position.x > other.gameObject.transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                isFacingRight = false;
            }
            if(!isRunning){StartCoroutine(Attack());}
        }
    }
    void Mouvement()
	{
        if (transform.position.x == xMax)
		{
			gameObject.GetComponent<SpriteRenderer>().flipX = false;
			isFacingRight = false;
		}
		if (transform.position.x == xMin)
		{
			gameObject.GetComponent<SpriteRenderer>().flipX = true;
			isFacingRight = true;
		}
		if (!isFacingRight)
		{
            GetComponent<Animator>().SetInteger("Anim", 0);
			gameObject.transform.Translate(-Time.deltaTime, 0, 0);
			Vector3 clampedPosition = transform.position;
			clampedPosition.x = Mathf.Clamp(transform.position.x, xMin, xMax);
			transform.position = clampedPosition;
		}
		if (isFacingRight)
		{
			GetComponent<Animator>().SetInteger("Anim", 0);
            gameObject.transform.Translate(Time.deltaTime, 0, 0);
			Vector3 clampedPosition = transform.position;
			clampedPosition.x = Mathf.Clamp(transform.position.x, xMin, xMax);
			transform.position = clampedPosition;
		}
	}
}
