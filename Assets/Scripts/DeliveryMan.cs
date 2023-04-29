using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryMan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RelayPoint")
        {
            GameManager.Instance.Points = 1;
        }
        else if(other.gameObject.tag == "Barrier")
        {
            Destroy(other.gameObject);
            Explosion();
        }
    }

    private void Explosion()
    {
        Destroy(gameObject);
    }
}
