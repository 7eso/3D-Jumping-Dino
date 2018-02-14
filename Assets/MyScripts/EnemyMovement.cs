using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float jump;
    public float speed;
   
    float dist;
    bool moving, jumping,firstTime = false;

    private Quaternion playerRotation;
    private Vector3 targetPosition;
    private Vector3 lookAtTarget;
    Vector3 target, startPosition;
    Rigidbody rb;
    private DataStructure tries = new DataStructure();

    public Transform enemy;
   // public Transform instSources;
    // Use this for initialization
    void Start()
    {

        // tries = new DataStructure();
        firstTime = true;
        rb = GetComponent<Rigidbody>();
        StartCoroutine("MyCoroutine");
    }

    // Update is called once per frame
    void Update()
    {
        //InvokeRepeating("Movement", 2f, 2f);
        if (moving == true)
        {

            float zAxis = Mathf.Lerp(transform.position.z, target.z, speed * Time.deltaTime);
            float xAxis = Mathf.Lerp(transform.position.x, target.x, speed * Time.deltaTime);
            Vector3 nexPosition = new Vector3(xAxis, transform.position.y, zAxis);
            transform.position = nexPosition;
            speed += 0.01f;
            dist = Mathf.Abs(Vector3.Distance(transform.position, target));

            if (dist < 0.11f )
            {
                moving = false;
                target = tries.targetPositions[Random.Range(0, tries.targetPositions.Length)].position;
                StartCoroutine("MyCoroutine");


            }
        }
    }
    IEnumerator MyCoroutine()
    {
        yield return new WaitForSeconds(1f);
        Movement();




    }

    void Movement()
    {
        target = tries.targetPositions[Random.Range(0, tries.targetPositions.Length)].position;
        for (int i = 0; i < tries.targetPositions.Length; i++)
        {
    

            if ((target == tries.targetPositions[i].position && moving == false) )
            {
                lookAtTarget = new Vector3(tries.targetPositions[i].position.x, transform.position.y, tries.targetPositions[i].position.z);
                transform.LookAt(lookAtTarget);
                speed = 2.3f;
                target.y += 0.6f;
                startPosition = transform.position;
          
                if (startPosition.y - target.y < -0.5f)
                {
                    Debug.Log("da5l el IF");
                    rb.AddForce(transform.up * jump, ForceMode.Impulse);
                    jumping = true;
                    moving = true;
                  

                }
                else
                {
                    speed += 1f;
                    moving = true;

                }
            }
            else continue;
        }

 

      
    }
    private void OnCollisionEnter(Collision collision)
    {
        tries = collision.gameObject.GetComponent<DataStructure>();
        Debug.Log("OnCollision");
    
    }
  
}
