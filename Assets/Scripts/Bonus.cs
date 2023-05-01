using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using System.Linq;

public class Bonus : MonoBehaviour
{
    public string BonusType;
    public List<string> BonusList = new List<string> { "ReduceTime", "GainReduceTime", "DoublePoints", "RemoveObstacle", "StopTime" };
    // Start is called before the first frame update
    void Start()
    {
        /*    System.Random rnd = new System.Random();
            int indexAleatoire = rnd.Next(0, BonusList.Count);

            BonusType = BonusList[indexAleatoire];
            GetComponent<MeshFilter>().mesh = MeshBonus[indexAleatoire];*/
        StartCoroutine(DelayDCD());
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
        foreach (GameObject AllBarrier in GameManager.Instance.ListBarrières.ToList())
        {
            Destroy(AllBarrier);
            GameManager.Instance.ListBarrières.Remove(AllBarrier);
        }


    }
    public void StopTime()
    {
        foreach (GameObject AllBarrier in GameManager.Instance.ListBarrières)
        {
            if (AllBarrier != null)
            {
                Debug.Log(AllBarrier);
                if (AllBarrier.GetComponent<Barrier>().Speed == 0)
                {

                }
                else
                {
                    AllBarrier.GetComponent<Barrier>().LastSpeed = AllBarrier.GetComponent<Rigidbody>().velocity;
                    AllBarrier.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    AllBarrier.GetComponent<Animator>().enabled = false;
                }
                
            }
        }
        StartCoroutine(TimeFrozen(GameManager.Instance.ListBarrières));
        foreach (GameObject AllDeliveryMan in GameManager.Instance.ListDeliveryMan)
        {
            if (AllDeliveryMan != null)
            {
                if (AllDeliveryMan.GetComponent<DeliveryMan>().Speed == 0)
                {

                }
                else
                {
                    AllDeliveryMan.GetComponent<DeliveryMan>().LastSpeed = AllDeliveryMan.GetComponent<Rigidbody>().velocity;
                    AllDeliveryMan.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    AllDeliveryMan.GetComponent<Animator>().speed = 0;
                }
                
            }
        }
        StartCoroutine(TimeFrozen(GameManager.Instance.ListDeliveryMan));
        Destroy(gameObject);
    }
    public void GetBonus()
    {
        if (BonusType == "ReduceTime")
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
    IEnumerator TimeFrozen(List<GameObject> EntityFrozen)
    {
        yield return new WaitForSeconds(10f);
        foreach (GameObject Entity in EntityFrozen)
        {
            if (Entity != null)
            {
                if (Entity.GetComponent<DeliveryMan>() != null)
                {
                    Entity.GetComponent<Rigidbody>().velocity = Entity.GetComponent<DeliveryMan>().LastSpeed;

                }
                else if (Entity.GetComponent<Barrier>() != null)
                {

                    Entity.GetComponent<Rigidbody>().velocity = Entity.GetComponent<Barrier>().LastSpeed;
                }
            }
        }
    }
    IEnumerator DelayDCD()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
