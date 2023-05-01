using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryMan : MonoBehaviour
{
    public float Speed;
    public float addTime = 2;
    public Vector3 LastSpeed;
    public Vector3 direction;
    public string BrandName;
    public bool isNotKind;
    public bool giveABuff;
    public bool hasBeThrowned;
    [SerializeField] private GameObject StarsParticles;
    [SerializeField] private GameObject ExplosionParticles;
    // Start is called before the first frame update
    void Start()
    {
       // StartCoroutine(DelayBeforeStop());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Death")
        {
            if (!isNotKind)
            {
                GameManager.Instance.BreakTheCombos(true);
            }
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "RelayPoint" )
        {
            if (isNotKind)
            {
                GameManager.Instance.Points = -5;
                GameManager.Instance.times = -10;
                GameManager.Instance.BreakTheCombos(false);
            }
            else if(BrandName == other.GetComponent<RelayPoint>().BrandName)
            {
                if (giveABuff)
                {
                    GameManager.Instance.Points = 5;
                }
                GameManager.Instance.Points = 1;
                GameManager.Instance.AddTimes();
                GameManager.Instance.Combos += 1;
                GameManager.Instance.CombosMultiplicator();
            }
            else
            {
                GameManager.Instance.Points = -1;
            }
            GameManager.Instance.BreakTheCombos(false);
            GameManager.Instance.ListDeliveryMan.Remove(gameObject);
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Barrier")
        {
            if (!isNotKind)
            {
                GameManager.Instance.times = -5;
            }
            GameManager.Instance.BreakTheCombos(false);
            GameManager.Instance.ListBarrières.Remove(other.gameObject);
            Destroy(other.gameObject);
            Explosion();
        }
        else if (other.gameObject.tag == "Bonus")
        {
            GameManager.Instance.Combos += 1;
            GameManager.Instance.CombosMultiplicator();
            other.gameObject.GetComponent<Bonus>().GetBonus();
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            
        }
        else if (other.gameObject.tag == "Malus")
        {
            GameManager.Instance.BreakTheCombos(true);
            other.gameObject.GetComponent<Malus>().GetMalus();
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<CapsuleCollider>().enabled = false;

        }
    }

    private void Explosion()
    {
        Instantiate(ExplosionParticles, transform.position, Quaternion.identity);
        GameManager.Instance.ListDeliveryMan.Remove(gameObject);
        Destroy(gameObject);
    }
  /*  IEnumerator DelayBeforeStop()
    {
        yield return new WaitForSeconds(3f);
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }*/
}
