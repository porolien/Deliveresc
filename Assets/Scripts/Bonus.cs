using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Bonus : MonoBehaviour
{
    public string BonusType;
    public List<string> BonusList = new List<string> { "ReduceTime", "GainReduceTime", "DoublePoints", "RemoveObstacle", "StopTime"};
    [SerializeField] private List<Mesh> MeshBonus = new List<Mesh>();
    // Start is called before the first frame update
    void Start()
    {
    /*    System.Random rnd = new System.Random();
        int indexAleatoire = rnd.Next(0, BonusList.Count);

        BonusType = BonusList[indexAleatoire];
        GetComponent<MeshFilter>().mesh = MeshBonus[indexAleatoire];*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReduceTimeBonus()
    {
        GameManager.Instance.timeMultiplator = 2F;
        StartCoroutine(DelaiBonus());
    }
    public void GainTimeBonus()
    {
        GameManager.Instance.times = 7;
        Destroy(gameObject);
    }
    public void DoublesPoints()
    {
        GameManager.Instance.PointX2 = 20;
        Destroy(gameObject);
    }
    public void RemoveBarrire()
    {
        for(int i = 0; i < GameManager.Instance.ListBarrières.Count; i++)
        {
            Destroy(GameManager.Instance.ListBarrières[i]);
            GameManager.Instance.ListBarrières.Remove(GameManager.Instance.ListBarrières[i]);
        }
        
    }
    public void StopTime()
    {

    }
    public void GetBonus()
    {
        if(BonusType == "ReduceTime")
        {
            ReduceTimeBonus();
        }
        if (BonusType == "GainReduceTime")
        {
            GainTimeBonus();
        }
        if (BonusType == "DoublePoints")
        {
            DoublesPoints();
        }
        if (BonusType == "RemoveObstacle")
        {
            RemoveBarrire();
        }
        if (BonusType == "StopTime")
        {
            StopTime();
        }
    }
    IEnumerator DelaiBonus()
    {
        yield return new WaitForSeconds(7f);
        GameManager.Instance.timeMultiplator = 1F;
        Destroy(gameObject);
    }
}
