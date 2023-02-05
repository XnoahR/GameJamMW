using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    Vector3 distance;
    public GameObject SFXobj;
    public bool Detected;
    public bool Lockmode;
    public bool isAttacking = false;
    public GameObject VFXobj;
    [SerializeField] float speed;
    public GameObject Player;
    PlayerController PlayerScript;
    private void Awake()
    {
        Player = GameObject.Find("Player");
        PlayerScript = Player.GetComponent<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Detected = false;
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerScript.isDead)
        {
            speed = 0;
        }
        //distance = Player.transform.position - transform.position;
        // Debug.Log(distance.magnitude);
        if (!Detected && !PlayerScript.isDead)
        {
            transform.Translate(Vector3.forward * 5f * Time.deltaTime);
        }
        else
        {


            if (!Lockmode)
            {
                StartCoroutine(isAWare());
            }
            else
            {
                if (!isAttacking)
                {
                    transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
                    transform.LookAt(Player.transform.position);
                }

                // if (distance.magnitude < 3)
                // {
                //     StartCoroutine(Attack());
                // }
            }

        }

    }

    // void CheckDistancePlayer()
    // {
    //     if(gameObject != null)
        
    // }




    IEnumerator Attack()
    {
        isAttacking = true;
        yield return new WaitForSeconds(2f);
        isAttacking = false;
    }
    IEnumerator isAWare()
    {

        transform.LookAt(Player.transform.position);
        yield return new WaitForSeconds(2f);
        Lockmode = true;
    }


    public void Die()
    {
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        PlayerScript.score += 1;
        //gameObject.SetActive(false);
        GameObject SFX = Instantiate(SFXobj,transform.position,Quaternion.identity);
        AudioSource poof = SFX.GetComponent<AudioSource>();
        poof.Play();
        Destroy(SFX,1.5f);
        GameObject VFX = Instantiate(VFXobj, transform.position, Quaternion.identity);
        ParticleSystem vfx = VFX.GetComponent<ParticleSystem>();
        vfx.Play();
        Destroy(VFX, vfx.main.duration);
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }

    void gameover()
    {
        speed = 0;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerScript.Dead();
        }
    }
}
