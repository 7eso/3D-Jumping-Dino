using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour {

    public float jump;
    public float speed;
    Renderer mat;
    Vector3 target , startPosition;
    Rigidbody rb;
    bool moving = false;

	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                target = hit.collider.gameObject.transform.position;
                target.y += 0.6f;
                startPosition = transform.position;
                if (Vector3.Distance(transform.position, target) > 0.5f && Vector3.Distance(startPosition, target) < 1.6f && startPosition.y - target.y < -0.5f)
                {
                    rb.AddForce(transform.up * jump, ForceMode.Impulse);
                }
               /* else if (Vector3.Distance(transform.position, target) > 0.5f && Vector3.Distance(startPosition, target) < 1.6f && startPosition.y - target.y > 0.5f)
                {
                    rb.AddForce(transform.up * 4, ForceMode.Impulse);
                }*/
               
                moving = true;
                Debug.Log(Vector3.Distance(transform.position, target));
                //Debug.Log(startPosition.y - target.y);
            }
        }
        //if (Vector3.Distance(rb.position, target) > 0.5f && moving == true)
        //
        if ( moving == true && Vector3.Distance(startPosition, target) < 1.6f && Mathf.Abs(startPosition.y - target.y) > 0.6f && moving == true)
        {
            float zAxis = Mathf.Lerp(transform.position.z, target.z, speed * Time.deltaTime);
            float xAxis = Mathf.Lerp(transform.position.x, target.x, speed * Time.deltaTime);
            Vector3 nexPosition = new Vector3(xAxis, transform.position.y, zAxis);
            transform.position = nexPosition;
            
        }
        

        else if ( Vector3.Distance(transform.position , target) < 0.1f && moving == true)
        {
            //transform.position = target;
            Invoke("SetInPosition" , 0.5f);
            
            Debug.Log("Arrived");
            
            moving = false;
        }
        //Debug.Log(Vector3.Distance(transform.position, target));

    }

    void SetInPosition()
    {
        transform.position = target;
        Debug.Log(Vector3.Distance(transform.position, target));
    }

    private void OnTriggerEnter(Collider collision)
    {
       
        mat = collision.gameObject.GetComponent<Renderer>();
        mat.material.color = Color.green;

    }
    
}
