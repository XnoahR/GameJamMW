using UnityEngine;
using System.Collections;
using ThreeDISevenZeroR.SensorKit;
public class PlayerController : MonoBehaviour
{
    //  [SerializeField] private BoxOverlapSensor sensor;
    Rigidbody rb;
    public float speed = 10.0f;
    public GameObject Fireball;
    public GameObject AttackArea;
    float RotationY;
    [SerializeField] float Sens;
    public Transform FirePoint;
    private bool SkillCooldownC = true;
    public bool isDead;
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
        if(!isDead){
        AttackInput();
        RotationInput();
        }
        if(isDead){
            CanMove = false;
        }
        else{
            CanMove = true;
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
            AttackArea.gameObject.SetActive(true);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            AttackArea.gameObject.SetActive(false);
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

    IEnumerator SkillC()
    {
        GameObject FireballGO = Instantiate(Fireball, FirePoint.transform.position, Quaternion.identity);
        SkillCooldownC = false;
        Destroy(FireballGO, 3f);
        yield return new WaitForSeconds(2f);
        SkillCooldownC = true;
    } 
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
        Destroy(gameObject);
        }
    }

    public void Dead()
    {
        isDead = true;
        anim.SetBool("Died",true);
        Debug.Log("DEAD");
    }
    // void LateUpdate()
    //     {
    //     AttackArea.transform.position = transform.position;
    //     AttackArea.transform.rotation = transform.rotation;
    //     AttackArea.transform.localScale = new Vector3(7, 1, 7);
    //     AttackArea.transform.Translate(0, 1f, 3);
    //     }
}
