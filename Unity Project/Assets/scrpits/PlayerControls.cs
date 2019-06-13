using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    Text ScoreText;
    [SerializeField]
    GameObject Heart1,Heart2,Heart3,HealthBar1,HealthBar2,HealthBar3,UI;
    [SerializeField]
    float speed,xMin=-182;
    bool att,isRunning,isAttacking,isFacingRight = true;
    public int Score,lives = 3;
    int i=0;
    bool death,Appear,Attack;

    // Start is called before the first frame update
    void Start()
    {
        isRunning = false;
        att = false;
    }

    // Update is called once per frame
    void Update()
    {
        death = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("death");
        Appear = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("appear");
        Attack = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack");
        if(transform.position.x < xMin)
            transform.position=new Vector3(xMin,transform.position.y,0);
        if(!death && !Appear && !Attack)
            Movement();
        if(lives == 0)
        {
           if(!isRunning){StartCoroutine(Death());}
        }
        if(lives == 2)
        {
            Heart1.SetActive(false);
        }
        if(lives == 1)
        {
            Heart2.SetActive(false);
        }
        att=true;
        attack();
        ScoreText.text = ""+Score;
    }

    public IEnumerator Death()
    {
        isRunning=true;
        Heart3.SetActive(false);
        GetComponent<Animator>().SetInteger("animate", 3);
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 0.1f);
        Time.timeScale=0;
        UI.SetActive(true);
        isRunning = false;
    }

    void attack()
    {
        if (Input.GetButtonDown("Jump"))
		{
            GetComponent<Animator>().SetInteger("animate", 2);
		}
        if(att==false)
        {
            GetComponent<Animator>().SetInteger("animate", 0);
            isAttacking = false;
        }
    }
    void Movement()
    {
        if (Input.GetAxis("Horizontal")>0) 

        {
			gameObject.transform.Translate(speed * Time.deltaTime, 0, 0);
            gameObject.GetComponent<Animator>().SetInteger("animate",1);
		}
		else if (Input.GetAxis("Horizontal") < 0 )
		{
			gameObject.transform.Translate(-speed * Time.deltaTime, 0, 0);
             gameObject.GetComponent<Animator>().SetInteger("animate",1);
        }
        else
        { gameObject.GetComponent<Animator>().SetInteger("animate",0);}

        if (isFacingRight && Input.GetAxis("Horizontal") < 0)
		{
			gameObject.GetComponent<SpriteRenderer>().flipX = false;
			isFacingRight = false;
		}
		else if (!isFacingRight && Input.GetAxis("Horizontal") > 0)
		{
			gameObject.GetComponent<SpriteRenderer>().flipX = true;
			isFacingRight = true;
		}
    }
    void Attacking()
    {
        isAttacking = true;
    }
    void EndAttack()
    {
        isAttacking = false;
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(isAttacking)
        {
            if(other.tag=="Vampire")
            {
                other.GetComponent<Animator>().SetInteger("Anim",3);
                Score+=200;
                other.GetComponent<Collider2D>().enabled = false;
            }
            else if(other.tag == "Trpas")
            {
                other.GetComponent<Animator>().SetInteger("Anim",3);
                Score+=50;
                other.GetComponent<Collider2D>().enabled = false;
            }
            else if(other.tag == "Boss")
            {
                i++;
                if(i==1)
                    HealthBar1.SetActive(false);
                if(i==2)
                    HealthBar2.SetActive(false);
                if(i==3)
                {
                    HealthBar3.SetActive(false);
                    other.GetComponent<Animator>().SetInteger("Anim",3);
                    Score+=500;
                    other.GetComponent<Collider2D>().enabled = false;
                }
            }
            else
            {
               other.GetComponent<Animator>().SetInteger("Anim",3);
               Score+=100;
               other.GetComponent<Collider2D>().enabled = false;
            }
            
        }
    }
}
