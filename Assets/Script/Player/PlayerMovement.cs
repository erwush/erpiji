using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;
public class PlayerMovement : MonoBehaviour

{


    public Rigidbody2D rb;
    private Vector2 moveInput;
    private CapsuleCollider2D plCollider;
    public Animator anim;
    //public Transform scael;
    public PlayerCombat combat;

    public int facingDirection = 1;
    public PlayerAttribute attr;

    public float spd = 9f;
    public AudioSource aaudio;
    public bool canMove = true;




    private void Update()
    {

        if (Input.GetButton("Skill1"))
        {
            Debug.Log("Edan");
        }





        if (Input.GetButton("Parry"))
        {
            combat.Parry();
        }

        // if (Input.GetButtonDown("Dodge"))
        // {

        // }


        if (anim.GetBool("isParry") == true)
        {
            spd = attr.spd / 2;
        }
        else
        {
            spd = attr.spd;
        }

        if (Input.GetButton("Dash"))
        {
            Debug.Log("aa" + 10 * (1 + 1.5));
            Debug.Log("bb" + 10 * (1 + 150f / 100f));
            Debug.Log("cc" + 10 * (150f / 100f));
            Debug.Log("dd" + 10 * 1.5f);
            StartCoroutine(Dodge());
        }
    }

    public IEnumerator Dodge()
    {
        aaudio.Play();
        spd = attr.spd * 5;
        combat.iFrame = true;
        plCollider.isTrigger = true;
        yield return new WaitForSeconds(0.1f);
        plCollider.isTrigger = false;
        spd = attr.spd;
        combat.iFrame = false;
    }
    //    else if (Input.GetButtonDown("Attack") && anim.GetFloat("afterAttack") == 1 && anim.GetBool("isAttack") == true);
    //    {
    //        warriorcombat.Attack2();
    //    }
    //}


    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        attr = GetComponent<PlayerAttribute>();
        plCollider = GetComponent<CapsuleCollider2D>();
        /*        scael = GetComponent<Transform>();*/
    }

    public void Knockback(Transform enemy, float knockbackForce, float knockDuration = 0.5f)
    {
        canMove = false;
        Vector2 direction = (transform.position - enemy.position).normalized;
        if (anim.GetBool("isParry") == true)
        {
            knockbackForce = knockbackForce / 4; //reuced by 75%
        }
        rb.linearVelocity = direction * knockbackForce;
        StartCoroutine(KnockbackCounter(knockDuration));
    }

    private IEnumerator KnockbackCounter(float knockDuration)
    {

        float elapsed = 0f;

        while (elapsed < knockDuration)
        {
            // perlahan turunkan velocity menuju 0
            rb.linearVelocity = Vector2.Lerp(
                rb.linearVelocity,
                Vector2.zero,
                Time.deltaTime * 2.5f // 5f bisa diubah jadi lebih besar/lebih kecil
            );

            elapsed += Time.deltaTime;
            yield return null; // tunggu 1 frame
        }



        //     rb.linearDamping = 2f;

        //     yield return new WaitUntil(() => rb.linearVelocity.magnitude < 0.3f);
        //     rb.linearDamping = 0f;

        canMove = true;
    }

    //Fixed Update == 50x frame
    void FixedUpdate()
    {
        if (canMove)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            rb.linearVelocity = new Vector2(horizontal, vertical) * spd;
            anim.SetFloat("horizontal", Mathf.Abs(horizontal));
            anim.SetFloat("vertical", Mathf.Abs(vertical));
            if (horizontal > 0 && transform.localScale.x < 0 || horizontal < 0 && transform.localScale.x > 0)
            {
                Flip();
            }
        }

        //rb.linearVelocity = Input * speed;


    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    //public void Move(InputAction.CallbackContext context)
    //{

    //if (anim.GetFloat("InputX") != 0) {
    //    float inputX = anim.GetFloat("InputX");
    //    Vector3 currentscalex = transform.localScale;
    //    currentscalex.x = inputX;
    //    transform.localScale = currentscalex;

    //}


    //float inputY = anim.GetFloat("InputY");
    //Vector3 currentscaley = transform.localScale;
    //currentscaley.y = inputY;
    //transform.localScale = currentscaley;

    //    anim.SetBool("IsWalking", true);
    //    if (context.canceled)
    //    {
    //        anim.SetBool("IsWalking", false);
    //        anim.SetFloat("LastInputX", moveInput.x);
    //        anim.SetFloat("LastInputY", moveInput.y);
    //    }



    //    moveInput = context.ReadValue<Vector2>();
    //    anim.SetFloat("InputX", moveInput.x);
    //    anim.SetFloat("InputY", moveInput.y);
}
