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
        else if (other.gameObject.tag == "Bonus")
        {
            other.gameObject.GetComponent<Bonus>().GetBonus();
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            
        }
        else if (other.gameObject.tag == "Malus")
        {
            other.gameObject.GetComponent<Malus>().GetMalus();
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<CapsuleCollider>().enabled = false;

        }
    }

    private void Explosion()
    {
        Destroy(gameObject);
    }
}
