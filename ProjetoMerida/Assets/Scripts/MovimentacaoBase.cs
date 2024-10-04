using UnityEngine;

public class MovimentacaoBase : MonoBehaviour
{
    public Rigidbody rb;
    public LayerMask Ground;

    private Animator anim;

    public float speed, maxSpeed, drag;
    public float rotationSpeed, forcaPulo;
    
    bool esquerda, frente, tras, direita, pulo;
    bool grounded;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        HandleUpdate();
        LimitVelocity();
        HandleDrag();
        CheckGrouded();
    }

    void CheckGrouded()
    {
        grounded = Physics.Raycast(transform.position + Vector3.up * .1f, Vector3.down, .2f, Ground);
    }

    void LimitVelocity()
    {
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if(horizontalVelocity.magnitude > maxSpeed)
        {
            Vector3 limitedVelocity = horizontalVelocity.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    void HandleRotation()
    {
        Vector3 horizontalDir = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (horizontalDir.magnitude > 0.1f)
        {
            Quaternion rotation = Quaternion.LookRotation(horizontalDir.normalized, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }


    void HandleDrag()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z) / (1+drag/100) + new Vector3(0, rb.velocity.y, 0);
    }

    void FixedUpdate()
    {
        HandleRotation();

        if (esquerda)
        {
            rb.AddForce(Vector3.left * speed);
            esquerda = false;
        }

        if (direita)
        {
            rb.AddForce(Vector3.right * speed);
            direita = false;
        }

        if (frente)
        {
            rb.AddForce(Vector3.forward * speed);
            frente = false;
        }

        if (tras)
        {
            rb.AddForce(Vector3.back * speed);
            tras = false;
        }

        if(pulo && grounded)
        {
            transform.position += Vector3.up * .1f;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.y);
            rb.AddForce(Vector3.up * forcaPulo, ForceMode.Impulse);
            pulo = false;
        }

    }

    void HandleUpdate()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("Andar", true);
            esquerda = true;
        }
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.SetBool("Andar", false);
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            anim.SetBool("Andar", true);
            tras = true;
        }
        if(Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            anim.SetBool("Andar", false);
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("Andar", true);
            direita = true;
        }
        if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetBool("Andar", false);
        }
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetBool("Andar", true);
            frente = true;
        }
        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            anim.SetBool("Andar", false);
        }
        if(Input.GetKey(KeyCode.Space) && grounded)
        {
            anim.SetBool("Andar", true);
            pulo = true;
        }
        if(Input.GetKeyUp(KeyCode.Space) && grounded)
        {
            anim.SetBool("Andar", false);
        }
        
    }
}
