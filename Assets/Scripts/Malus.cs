using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Malus : MonoBehaviour
{

    public string MalusType;
    public List<string> MalusList = new List<string> { "AugmentTime", "Point/2", "BarrierSpawn" };

    // Start is called before the first frame update
    void Start()
    {
     /*   System.Random rnd = new System.Random();
        int indexAleatoire = rnd.Next(0, MalusList.Count);

        MalusType = MalusList[indexAleatoire];*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AugmentTimeMalus()
    {
        GameManager.Instance.timeMultiplator = 0.5F;
        StartCoroutine(DelaiMalus());
    }
    public void ReduceTime()
    {
        GameManager.Instance.times = 7;
        Destroy(gameObject);
    }

    public void GetMalus()
    {
        if (MalusType == "AugmentTime")
        {
            AugmentTimeMalus();
        }
        if (MalusType == "Point/2")
        {
            ReduceTime();
        }
        if (MalusType == "BarrierSpawn")
        {
          
        }
    }
    IEnumerator DelaiMalus()
    {
        yield return new WaitForSeconds(7f);
        GameManager.Instance.timeMultiplator = 1F;
        Destroy(gameObject);
    }
}

