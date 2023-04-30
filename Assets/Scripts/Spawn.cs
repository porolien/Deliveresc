using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Spawn : MonoBehaviour
{
    [SerializeField] private float maxUp;
    [SerializeField] private float minUp;
    [SerializeField] private float centerUp;
    [SerializeField] private float maxSides;
    [SerializeField] private float minSides;
    [SerializeField] private float minTimeToSpawn;
    [SerializeField] private float maxTimeToSpawn;
    [SerializeField] private float testHauteur;

    private bool TimeBeforeSpawnChoosen;
    private bool canSpawnBarier = true;
    private float TimeBeforeSpawn;
    private float timeBeforeLast;
    public GameObject Bonus, Malus;
    public List<GameObject> BarrieresList = new List<GameObject>();
    public List<GameObject> DeliveryManList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void spawnEntity()
    {
        Vector3 SpawnPoint;
        SpawnPoint = new Vector3(Random.Range(minSides, maxSides), Random.Range(0, maxUp), 0);
        if (Random.Range(0, 2) == 3)
        {
            if (Random.Range(0, 2) == 0)
            {
                GameObject buff = Instantiate(Bonus, SpawnPoint, Quaternion.identity);
            }
            else
            {
                GameObject debuff = Instantiate(Malus, SpawnPoint, Quaternion.identity);
            }
        }
        else
        {
            int rand = Random.Range(0, BarrieresList.Count);
            if (BarrieresList[rand].name == "TrashCan")
            {
                
                GameObject obj = Instantiate(BarrieresList[rand], SpawnPoint, Quaternion.identity);
            }
            else
            {
                int randomNumber = (Random.Range(0, 2) == 0) ? 16 : -16;
                SpawnPoint = new Vector3(randomNumber, Random.Range(0, maxUp), 0);
                GameObject obj = Instantiate(BarrieresList[rand], SpawnPoint, Quaternion.identity);
                if (randomNumber == 16)
                {
                    obj.GetComponent<Barrier>().Speed = -obj.GetComponent<Barrier>().Speed;
                    obj.transform.rotation = Quaternion.Euler(0, -180, 0);
                }


            }
        }
        
    }
    // Update is called once per frame
    void Update()
    {
      
        StartCoroutine(naturalEntitySpawn());
       if (TimeBeforeSpawnChoosen)
        {
            timeBeforeLast += Time.deltaTime;
            if (timeBeforeLast > TimeBeforeSpawn)
            {
                timeBeforeLast = 0;
                TimeBeforeSpawnChoosen = false;
                Vector3 SpawnPoint;
                    float sides = -13f;
                    if (Random.Range(0, 2) == 0)
                    {
                        sides = 13f;

                    }
                    SpawnPoint = new Vector3(sides, 0.25f, Random.Range(-3, -7));
                
                GameObject newDeliveryMan = Instantiate(DeliveryManList[Random.Range(0, DeliveryManList.Count)], SpawnPoint, Quaternion.Euler(90f, 0, 90f));
                Vector3 direction = new Vector3(-sides - newDeliveryMan.transform.position.x, 0, 0) ;
                //newDeliveryMan.transform.up = direction.normalized;
                if (SpawnPoint.x < 0)
                {
                    Debug.Log("itchanged");
                    newDeliveryMan.GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 0).normalized * newDeliveryMan.GetComponent<DeliveryMan>().Speed;
                    newDeliveryMan.transform.rotation = Quaternion.Euler(90f, 0, -90f);
                }
                else
                {
                    newDeliveryMan.GetComponent<Rigidbody>().velocity = direction.normalized * newDeliveryMan.GetComponent<DeliveryMan>().Speed;
                }
               
                if (Random.Range(0, 5) == 0)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        newDeliveryMan.GetComponent<DeliveryMan>().isNotKind = true;
                    }
                    else
                    {
                        newDeliveryMan.GetComponent<DeliveryMan>().giveABuff = true; 
                    }
                }


            }
        }
        else
        {
            TimeBeforeSpawn = Random.Range(minTimeToSpawn, maxTimeToSpawn);
            TimeBeforeSpawnChoosen = true;
        }
    }
    IEnumerator naturalEntitySpawn()
    {
        if (canSpawnBarier)
        {
            canSpawnBarier = false;
            spawnEntity();
            yield return new WaitForSeconds(10f);
            canSpawnBarier = true ;
        }
        
    }
}
