using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAMan : MonoBehaviour
{
    public GameObject TheManToThrowAway;
    private Vector3 oldMousePosition;
    public float ThrowSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            oldMousePosition = Input.mousePosition;
            Debug.Log(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000))
            {
                if (hit.transform.gameObject.tag == "DeliveryMan")
                {
                    TheManToThrowAway = hit.transform.gameObject;
                    TheManToThrowAway.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    TheManToThrowAway.GetComponent<Animator>().SetBool("IsFloating", enabled);
                    TheManToThrowAway.transform.rotation = Quaternion.Euler(50, 180, 0);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log(Input.mousePosition);
            ThrowSomeone();
        }
    }



    void ThrowSomeone()
    {
        Vector3 direction = Input.mousePosition - oldMousePosition;
        TheManToThrowAway.GetComponent<Rigidbody>().velocity = new Vector2(direction.x, direction.y).normalized * ThrowSpeed;
    }
}
