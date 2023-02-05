using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ThreeDISevenZeroR.SensorKit;
public class PlayerController : MonoBehaviour
{
    //  [SerializeField] private BoxOverlapSensor sensor;

    public int score;


    public AudioManager SFX;
    public float Mana = 100;
    public Slider ManaBar;
    //AudioSource DeadMeow; 
    Rigidbody rb;
    public float speed = 10.0f;
    public GameObject Fireball;
    public GameObject AttackArea;
    float RotationY;
    [SerializeField] float Sens;
    public Transform FirePoint;
    private bool SkillCooldownC = true;
    public bool isDead;
    private bool Attacking;
    private bool CanMove;
    public Animator anim;

    public GroundMove Ground;
    private void Start()
    {
        AttackArea.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        isDead = false;
        score = 0;
        //DeadMeow = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        //    sensor.UpdateSensor();
        if (CanMove && Horizontalzone() && VerticalZone())
        {
            MovementInput();
        }
        CheckPosition();

    }

    private bool VerticalZone()
    {
        return (transform.position.x <= 10f && transform.position.x >= -10f);
    }

    private bool Horizontalzone()
    {
        return (transform.position.z >= 4.9f && transform.position.z <= 20f);
    }

    private void Update()
    {
        ManaBar.value = Mana;
        if (!isDead)
        {
            AttackInput();
            RotationInput();
        }

        ManaCheck();

        if (isDead)
        {
            CanMove = false;
        }
        else
        {
            CanMove = true;
        }
    }

    private void ManaCheck()
    {
        if (Attacking)
        {
            if (Mana > 0)
            {
                MagicOn();
            }
            else
            {
                MagicOff();
            }
            if (Mana >= 0)
                Mana -= 50 * Time.deltaTime;
        }
        else
        {
            if (Mana <= 100)
                Mana += 30 * Time.deltaTime;
        }
        if (Mana < 0)
        {
            Mana = 0;
        }
        if (Mana > 100)
        {
            Mana = 100;
        }
    }

    void CheckPosition()
    {
        if (transform.position.z < 5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 5);
        }
        if (transform.position.z > 20f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 20);
        }
        if (transform.position.x > 10f)
        {
            transform.position = new Vector3(10, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -10f)
        {
            transform.position = new Vector3(-10, transform.position.y, transform.position.z);
        }
    }
    void MovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        rb.MovePosition(transform.position + new Vector3(horizontal, 0, vertical) * speed * Time.fixedDeltaTime);
        //transform.position = transform.position + new Vector3(horizontal, 0, vertical) * speed * Time.fixedDeltaTime;
    }

    void AttackInput()
    {
        //Basic Attack
        // if (Input.GetMouseButtonDown(0))
        // {
        //     print("Attack");
        //     if (!sensor.HasHit) return;
        //     Debug.Log("Hit Something");
        //     sensor.HitCollider.GetComponent<Enemy>().Damaged();
        // }

        if (Input.GetButtonDown("Fire1"))
        {
            SFX.Play("CatAttack");
            Attacking = true;

            SFX.Play("MagicArea");


        }
        if (Input.GetButtonUp("Fire1"))
        {
            Attacking = false;
            MagicOff();
        }

        if (Input.GetKeyDown(KeyCode.C) && SkillCooldownC)
        {
            StartCoroutine(SkillC());
        }
    }

    void RotationInput()
    {
        float mouseY = Input.GetAxisRaw("Mouse X") * Sens * Time.deltaTime;

        RotationY += mouseY;
        RotationY = Mathf.Clamp(RotationY, -45, 45);

        transform.rotation = Quaternion.Euler(transform.up * RotationY);
    }


    void MagicOn()
    {
        AttackArea.gameObject.SetActive(true);
    }

    void MagicOff()
    {
        AttackArea.gameObject.SetActive(false);
        SFX.Stop("MagicArea");
    }
    IEnumerator SkillC()
    {
        GameObject FireballGO = Instantiate(Fireball, FirePoint.transform.position, Quaternion.identity);
        SkillCooldownC = false;
        Destroy(FireballGO, 3f);
        yield return new WaitForSeconds(2f);
        SkillCooldownC = true;
    }

    public void Dead()
    {
        isDead = true;
        anim.SetBool("Died", true);
        SFX.Play("DeathCat");
        rb.isKinematic = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("DEAD");
    }
}
