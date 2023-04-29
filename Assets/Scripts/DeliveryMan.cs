using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryMan : MonoBehaviour
{
    public float Speed;
    public Vector3 direction;
    public string BrandName;
    public bool isNotKind;
    public bool giveABuff;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayBeforeStop());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RelayPoint" )
        {
            if (isNotKind)
            {

            }
            if(BrandName == other.GetComponent<RelayPoint>().BrandName)
            {
                if (giveABuff)
                {

                }
                GameManager.Instance.Points = 1;
            }
            else
            {
                GameManager.Instance.Points = -1;
            }
           
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
    IEnumerator DelayBeforeStop()
    {
        yield return new WaitForSeconds(3f);
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }
}
