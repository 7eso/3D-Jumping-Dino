using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStructureJumping : MonoBehaviour {

    public float jump;
    public float speed;
    
    float dist; 
    bool moving , jumping = false;

    private Quaternion playerRotation;
    private Vector3 targetPosition;
    private Vector3 lookAtTarget;
    Renderer mat;
    Vector3 target, startPosition ;
    Rigidbody rb;
    DataStructure tries;
    
    public Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && moving == false)
            {
                target = hit.collider.gameObject.transform.position;
                for (int i = 0; i < tries.targetPositions.Length; i++)
                {
                    if (target == tries.targetPositions[i].position )
                    {
                        lookAtTarget = new Vector3(tries.targetPositions[i].position.x, transform.position.y, tries.targetPositions[i].position.z);
                        animator.SetTrigger("Jumping");
                        transform.LookAt(lookAtTarget);
                        speed = 2.3f;
                        target.y += 0.6f;
                        startPosition = transform.position;
                        if (startPosition.y - target.y < -0.5f)
                        {
                            rb.AddForce(transform.up * jump, ForceMode.Impulse);
                            jumping = true;
                        
                            //animator.SetBool("Moving", false);
                            Invoke("MovingCondition", 0.2f);
                        }
                        else
                        {
                            speed += 1f;
                            moving = true;
                            //animator.SetBool("Moving", false);
                        }
                    }
                    else continue;

                }
                
               
            }  
        }
        if (moving == true)
        {
            
            float zAxis = Mathf.Lerp(transform.position.z, target.z, speed * Time.deltaTime);
            float xAxis = Mathf.Lerp(transform.position.x, target.x, speed * Time.deltaTime);
            Vector3 nexPosition = new Vector3(xAxis, transform.position.y, zAxis);
            transform.position = nexPosition;
            speed += 0.01f;
            dist = Mathf.Abs(Vector3.Distance(transform.position, target));
            //Debug.Log(dist);
            animator.SetBool("Moving", false);

        }

        if (dist < 0.11f && moving == true)
        {
            moving = false;
           // animator.SetBool("Moving", false);

        }
        

    }
    private void OnTriggerEnter(Collider collision)
    {

       
        mat = collision.gameObject.GetComponent<Renderer>();
        mat.material.color = Color.green;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.name == "idle2")
        {
            gameObject.SetActive(false);
        }
        else
        {
            tries = collision.gameObject.GetComponent<DataStructure>();

        }

    }
    void MovingCondition()
    {
        moving = true;
      
    }
   

}
