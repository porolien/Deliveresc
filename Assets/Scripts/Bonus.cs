using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Bonus : MonoBehaviour
{
    public string BonusType;
    public List<string> BonusList = new List<string> { "ReduceTime", "GainReduceTime", "DoublePoints", "RemoveObstacle" };
    // Start is called before the first frame update
    void Start()
    {
        System.Random rnd = new System.Random();
        int indexAleatoire = rnd.Next(0, BonusList.Count);

        BonusType = BonusList[indexAleatoire];

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
        GameManager.Instance.Points *=2;
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
    }
    IEnumerator DelaiBonus()
    {
        yield return new WaitForSeconds(7f);
        GameManager.Instance.timeMultiplator = 1F;
        Destroy(gameObject);
    }
}
