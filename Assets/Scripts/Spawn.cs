using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private float maxUp;
    [SerializeField] private float minUp;
    [SerializeField] private float maxSides;
    [SerializeField] private float minSides;
    [SerializeField] private float minTimeToSpawn;
    [SerializeField] private float maxTimeToSpawn;
    [SerializeField] private float testHauteur;

    private bool TimeBeforeSpawnChoosen;
    private float TimeBeforeSpawn;
    private float timeBeforeLast;

    public List<GameObject> DeliveryManList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(TimeBeforeSpawnChoosen)
        {
            timeBeforeLast += Time.deltaTime;
            if (timeBeforeLast > TimeBeforeSpawn)
            {
                timeBeforeLast = 0;
                TimeBeforeSpawnChoosen = false;
                Vector3 SpawnPoint;
                if (Random.Range(0, 2) == 0)
                {
                        SpawnPoint = new Vector3(Random.Range(minSides, maxSides), testHauteur, 0);
                }
                else
                {
                    float sides = -13f;
                    if (Random.Range(0, 2) == 0)
                    {
                        sides = 13f;
                    }
                    SpawnPoint = new Vector3(sides, Random.Range(minUp, maxUp), 0);
                }
                GameObject newDeliveryMan = Instantiate(DeliveryManList[Random.Range(0, DeliveryManList.Count)], SpawnPoint, Quaternion.identity);
                newDeliveryMan.GetComponent<DeliveryMan>().direction = new Vector3(Random.Range(-8, 8), Random.Range(-3, 0), 0);
                Vector3 DeliveryManDirection = newDeliveryMan.GetComponent<DeliveryMan>().direction - newDeliveryMan.transform.position;
                newDeliveryMan.GetComponent<Rigidbody>().velocity = DeliveryManDirection.normalized * newDeliveryMan.GetComponent<DeliveryMan>().Speed;
               // newDeliveryMan.transform.right = (new Vector3(Random.Range(-8, 8), Random.Range(-3, 0), 0) - newDeliveryMan.transform.position).normalized;
            }
        }
        else
        {
            TimeBeforeSpawn = Random.Range(minTimeToSpawn, maxTimeToSpawn);
            TimeBeforeSpawnChoosen = true;
        }
    }
}
