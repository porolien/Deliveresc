using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance => _instance;
    public List<GameObject> ListBarrières = new List<GameObject>();
    public TextMeshProUGUI RemainingTime;
    public TextMeshProUGUI PointsText;

    public float PointX2;

    private int Round = 1;
    public List<string> brandName = new List<string>();
    [SerializeField] private List<GameObject> DeliveryMansToAdd = new List<GameObject>();
    public Spawn spawn;
    [SerializeField] private List<RelayPoint> relayPoints = new List<RelayPoint>();

   private float points;
    public float Points
    {
        get { return points; }
        set
        {
            if (PointX2 > 0)
            {
                points = value*2 + points;
            }
            else
            {
                points = value + points;
            }
            PointsText.text = "" + points;
            if (Round == 1 && points >= 5)
            {
                NewRound();
            }
            else if (Round == 2 && points >= 10)
            {
                NewRound();
            }
        }
    }
    private float time;
    public float times
    {
        get { return time; }
        set
        {
            time = value + time;
            RemainingTime.text = "" + time;
            if (time <= 0)
            {
                Defeat();
            }
        }
    }
    public float timeMultiplator = 1;

    private float timeBeforeLast;

    private float Highscore;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        times = 100;
        float score = PlayerPrefs.GetFloat("Highscore");
    }

    // Update is called once per frame
    void Update()
    {
        timeBeforeLast += Time.deltaTime;
        if (timeBeforeLast > timeMultiplator)
        {
            PointX2 = PointX2 - timeMultiplator;
            timeBeforeLast = 0;
            times = -1;
        }

    }
    void Defeat()
    {
        Debug.Log("defeat");
        if(PlayerPrefs.GetFloat("Highscore") < time)
        {
            PlayerPrefs.SetFloat("Highscore", time);
        }
        
    }

    void NewRound()
    {
        Round++;
        if (Round == 2)
        {
            AddBrand("UTerEats");
        }
        if (Round == 3)
        {
            AddBrand("Wcdonald");
        }
        foreach (RelayPoint relayBrand in relayPoints)
        {
            if (relayBrand.BrandName == brandName[brandName.Count - 1])
            {
                Debug.Log(relayBrand.gameObject.name);
                relayBrand.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

    void AddBrand(string brand)
    {
        brandName.Add(brand);
        foreach (GameObject Man in DeliveryMansToAdd)
        {
            if (Man.GetComponent<DeliveryMan>().BrandName == brand)
            {
                spawn.DeliveryManList.Add(Man);

            }
        }
    }
}
