using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.ListBarrières.Add(gameObject);
        GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 0) * Speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Death")
        {
            Destroy(gameObject);
        }
    }
}
