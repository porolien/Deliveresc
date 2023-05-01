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
    [SerializeField] private GameObject GreenParticles;
    [SerializeField] private GameObject RedParticles;

    private bool canSpawnBarier = true;
    private bool canSpawnEntity = true;
    public float TimeBeforeSpawnEntity = 10f;
    public float TimeBeforeSpawnBarrier = 8f;
    public float TimeBeforeSpawnDeliveryMan = 1.5f;
    private float timeBeforeLast;
    public GameObject Bonus, Malus;
    public GameObject giletJaune;
    public List<GameObject> BarrieresList = new List<GameObject>();
    public List<GameObject> DeliveryManList = new List<GameObject>();
    public List<GameObject> BonusList = new List<GameObject>();
    public List<GameObject> MalusList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }
    void spawnEntity()
    {
        Vector3 SpawnPoint = Vector3.zero;

        if (Random.Range(0, 2) == 0)
        {
            SpawnPoint = new Vector3(Random.Range(minSides, maxSides), Random.Range(0, maxUp), 0);
            if (Random.Range(0, 2) == 0)
            {
                GameObject buff = Instantiate(BonusList[Random.Range(0, BonusList.Count)], SpawnPoint, Quaternion.identity);
            }
            else
            {
                GameObject debuff = Instantiate(MalusList[Random.Range(0, MalusList.Count)], SpawnPoint, Quaternion.identity);
            }
        }
        else
        {
            spawnBarrier(SpawnPoint);
        }
    }
    // Update is called once per frame
    void Update()
    {

        StartCoroutine(naturalEntitySpawn());
        StartCoroutine(naturalBarrierSpawn());
        timeBeforeLast += Time.deltaTime;
        if (timeBeforeLast > TimeBeforeSpawnDeliveryMan)
        {
            timeBeforeLast = 0;
            Vector3 SpawnPoint;
            float sides = -13f;
            if (Random.Range(0, 1f) < 0.5f)
            {
                sides = 13f;

            }
            SpawnPoint = new Vector3(sides, 0.25f, Random.Range(-3, -7));

            GameObject newDeliveryMan = Instantiate(DeliveryManList[Random.Range(0, DeliveryManList.Count)], SpawnPoint, Quaternion.Euler(90f, 0, 90f));
            GameManager.Instance.ListDeliveryMan.Add(newDeliveryMan);
            Vector3 direction = new Vector3(-sides - newDeliveryMan.transform.position.x, 0, 0);
            //newDeliveryMan.transform.up = direction.normalized;
            if (SpawnPoint.x < 0)
            {
                newDeliveryMan.GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 0).normalized * newDeliveryMan.GetComponent<DeliveryMan>().Speed;
                newDeliveryMan.transform.rotation = Quaternion.Euler(90f, 0, -90f);
            }
            else
            {
                newDeliveryMan.GetComponent<Rigidbody>().velocity = direction.normalized * newDeliveryMan.GetComponent<DeliveryMan>().Speed;
            }

            if (Random.Range(0, 5) == 0)
            {
                switch (Random.Range(0, 3))
                {
                    case 0:
                        newDeliveryMan.GetComponent<DeliveryMan>().isNotKind = true;
                        GameObject ParticlesNotKind = Instantiate(RedParticles, new Vector3(sides, 2, SpawnPoint.z), Quaternion.Euler(90, 0, 0));
                        ParticlesNotKind.transform.SetParent(newDeliveryMan.transform, true);
                        break;
                    case 1:
                        newDeliveryMan.GetComponent<DeliveryMan>().giveABuff = true;
                        GameObject ParticlesGiveABuff = Instantiate(GreenParticles, new Vector3(sides, 2, SpawnPoint.z), Quaternion.Euler(90, 0, 0));
                        ParticlesGiveABuff.transform.SetParent(newDeliveryMan.transform, true);
                        break;
                    case 2:
                        GameObject newGiletJaune = Instantiate(giletJaune, SpawnPoint, Quaternion.Euler(90f, 0, 90f));
                        GameManager.Instance.ListDeliveryMan.Add(newDeliveryMan);
                        direction = new Vector3(-sides - newDeliveryMan.transform.position.x, 0, 0);
                        if (SpawnPoint.x < 0)
                        {
                            newDeliveryMan.GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 0).normalized * newDeliveryMan.GetComponent<DeliveryMan>().Speed;
                            newDeliveryMan.transform.rotation = Quaternion.Euler(90f, 0, -90f);
                        }
                        else
                        {
                            newDeliveryMan.GetComponent<Rigidbody>().velocity = direction.normalized * newDeliveryMan.GetComponent<DeliveryMan>().Speed;
                        }
                        break;
                }
            }


        }
    }
    public void spawnBarrier(Vector3 SpawnPoint)
    {
        int rand = Random.Range(0, BarrieresList.Count);
        if (BarrieresList[rand].name == "TrashCan")
        {
            SpawnPoint = new Vector3(Random.Range(minSides, maxSides), 0.67f, 0);
            GameObject obj = Instantiate(BarrieresList[rand], SpawnPoint, Quaternion.identity);
            obj.transform.rotation = Quaternion.Euler(270, 0, 0);
            StartCoroutine(TrashExplosion(obj));
        }
        else
        {
            int randomNumber = (Random.Range(0, 2) == 0) ? 16 : -16;
            SpawnPoint = new Vector3(randomNumber, 0, Random.Range(2, maxUp));
            GameObject obj = Instantiate(BarrieresList[rand], SpawnPoint, Quaternion.identity);
            Debug.Log(obj.name);
            if (randomNumber == 16)
            {
                obj.GetComponent<Barrier>().Speed = -obj.GetComponent<Barrier>().Speed;
                obj.transform.rotation = Quaternion.Euler(0, -180, 0);
            }
            if (obj.name == "Women with kid(Clone)")
            {
                if (randomNumber == 16)
                {
                    obj.transform.rotation = Quaternion.Euler(0, -90, 0);
                }
                else
                {
                    obj.transform.rotation = Quaternion.Euler(0, 90, 0);
                }
            }
        }
    }
    IEnumerator naturalEntitySpawn()
    {
        if (canSpawnEntity)
        {
            canSpawnEntity = false;
            spawnEntity();
            yield return new WaitForSeconds(TimeBeforeSpawnEntity);
            canSpawnEntity = true;
        }

    }
    IEnumerator naturalBarrierSpawn()
    {
        if (canSpawnBarier)
        {
            canSpawnBarier = false;
            spawnEntity();
            yield return new WaitForSeconds(TimeBeforeSpawnBarrier);
            canSpawnBarier = true;
        }

    }
    IEnumerator TrashExplosion(GameObject Trash)
    {
        yield return new WaitForSeconds(20f);
        if (Trash != null)
        {
            GameManager.Instance.ListBarrières.Remove(Trash);
            Destroy(Trash);
        }
    }
}
