using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    /* public Vector3 target;
      public float speed;
      public int layerMask = 1 << 8;


      Rigidbody rb;


      void Start ()
      {
          rb = GetComponent<Rigidbody>();

      }


      void Update () {

          SetTargetPosition();

      }

      IEnumerator MoveObject(Vector3 upPosition)
      {

          while (Vector3.Distance(transform.position, upPosition) > 0.2f)
          {

              transform.position = Vector3.MoveTowards(transform.position, upPosition, speed * Time.deltaTime);
              yield return null;
          }

          //
          yield return new WaitForSeconds(0.1f);
          //rb.useGravity = false;

          while (Vector3.Distance(transform.position , target) > 0.05f)
          {

              transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
              yield return null;
          }
          rb.useGravity = true;
          yield return null;
      }
      void SetTargetPosition()
      {
          if (Input.GetMouseButton(0))
          {
              RaycastHit hit;
              Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

              if (Physics.Raycast(ray, out hit, Mathf.Infinity , layerMask))
              {
                  //transform.position = new Vector3(1.2f, 1.2f, 0.5f);
                  target = hit.collider.gameObject.transform.position;

                  if (Mathf.Abs(transform.position.y - target.y) > 0.5f && Mathf.Abs(transform.position.y - target.y) < 1.5f)
                  {
                      //target.y += 0.2f;
                      //target.z += 1.5f;
                      //transform.position = target;
                      //rb.AddForce(transform.up * 1, ForceMode.Impulse);
                      Vector3 upPosition = transform.position;
                      upPosition.y += 2.0f;
                      StartCoroutine(MoveObject(upPosition));

                  }
              }
          }




      }
     /*
    private bool is_grounded = true;
    private int rot;
    public Rigidbody rb;
    public float speed = 10;
    public float jump_force;
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rot = 0;
            if (is_grounded == true)
            {
                jump();
            }
            transform.eulerAngles = new Vector3(0, rot, 0);
            rb.velocity = Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rot = 180;
            if (is_grounded == true)
            {
                jump();
            }
            transform.eulerAngles = new Vector3(0, rot, 0);
            rb.velocity = -Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rot = 90;
            if (is_grounded == true)
            {
                jump();
            }
            transform.eulerAngles = new Vector3(0, rot, 0);
            rb.velocity = Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rot = -90;
            if (is_grounded == true)
            {
                jump();
            }
            transform.eulerAngles = new Vector3(0, rot, 0);
            rb.velocity = Vector3.left * speed * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider others)
    {
        if (others.gameObject.tag == "Collider")
        {
            is_grounded = true;
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
    void jump()
    {
        //if (is_grounded == true) {
        rb.AddForce(new Vector3(0, jump_force, 0) * Time.fixedDeltaTime);
        is_grounded = false;
    }*/
    public Rigidbody rb;
    public Vector3 target;
    public float h;
    public float gravity = -18f;


    private void Start()
    {
        rb.useGravity = false;
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                target = hit.collider.gameObject.transform.position;
                Launch();
            }
           
            
        }
    }

    void Launch()
    {
        Physics.gravity = Vector3.up * gravity;
        rb.useGravity = true;
         rb.velocity = CalculateLauncherVelocity();
       // rb.gameObject.transform.position = target;
        
    }

    Vector3 CalculateLauncherVelocity()
    {
        float displacementY = target.y - rb.position.y;
        float time = Mathf.Sqrt(-2f * h / gravity) + Mathf.Sqrt(2f * (displacementY - h ) / Mathf.Abs(gravity));
        Vector3 displacementXZ = new Vector3(target.x - rb.position.x, 0, target.z - rb.position.z);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-4.5f * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;
        Debug.Log(velocityXZ);
        Debug.Log(velocityY);
        return velocityXZ + velocityY;
    }
    

    
}

