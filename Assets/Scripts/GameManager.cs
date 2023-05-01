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
    public List<GameObject> ListDeliveryMan = new List<GameObject>();
    public TextMeshProUGUI RemainingTime;
    public TextMeshProUGUI PointsText;
    public GameObject DefeatUI;
    [SerializeField] private TextMeshProUGUI HighscoreUI;
    public TextMeshProUGUI CombosUI;

    public float PointX2;
    public int Combos;
    private int PointsMultipliactor = 1;
    public int CounterToBreakCombos;

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
            if (PointX2 > 0 && value > 0)
            {
                points = value * 2 * PointsMultipliactor + points;
            }
            else
            {
                if(value > 0)
                {
                    points = value * PointsMultipliactor + points;
                }
                else
                {
                    points = value + points;
                }
               
            }
            PointsText.text = "" + points;
            if (Round == 1 && points >= 50)
            {
                NewRound();
            }
            else if (Round == 2 && points >= 100)
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
    private float addTime = 2;
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
        Points = 0;
       
    }

    // Update is called once per frame
    void Update()
    {
        timeBeforeLast += Time.deltaTime;
        if (timeBeforeLast > timeMultiplator)
        {
            if(PointX2 > 0)
            {
                PointX2 = PointX2 - timeMultiplator;
                if (PointX2 < 0)
                {
                    PointX2 = 0;
                }

            }
            timeBeforeLast = 0;
            times = -1;
        }

    }
    void Defeat()
    {
        DefeatUI.SetActive(true);
        RemainingTime.gameObject.SetActive(false);
        PointsText.gameObject.SetActive(false);
        if(PlayerPrefs.GetFloat("Highscore") < points)
        {
            PlayerPrefs.SetFloat("Highscore", points);
        }
        HighscoreUI.text = "" + PlayerPrefs.GetFloat("Highscore");


    }

    void NewRound()
    {
        Round++;
        if (Round == 2)
        {
            spawn.TimeBeforeSpawnDeliveryMan = 1;
            spawn.TimeBeforeSpawnBarrier = 6;
            addTime = 1;
            AddBrand("UTerEats");
        }
        if (Round == 3)
        {
            spawn.TimeBeforeSpawnDeliveryMan = 0.5f;
            spawn.TimeBeforeSpawnBarrier = 4f;
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

    public void AddTimes()
    {
         times = addTime;
    }

    public void CombosMultiplicator()
    {
        switch (Combos)
        {
            case 5:
                points = points + 10;
                times = +5;
                break;
            case 20:
                points = points + 100;
                PointsMultipliactor = 2;
                break;
            case 35:
                points = points + 200;
                PointsMultipliactor = 3;
                times = +10;
                break;
            case 50:
                points = points + 500;
                PointsMultipliactor = 5;
                times = +20;
                break;
            case 69:
                points = points + 690;
                times = +69;
                break;
            case 75:
                points = points + 1000;
                PointsMultipliactor = 10;
                break;
            case 100:
                points = points + 10000;
                PointsMultipliactor = 100;
                times = +100;
                break;
            default:
                break;
        }
        PointsText.text = "" + points;
    }

    public void BreakTheCombos(bool Need2ToBreak)
    {
       
        if (CounterToBreakCombos >=2 || !Need2ToBreak)
        {
            Combos = 0;
            CounterToBreakCombos = 0;
            PointsMultipliactor = 1;
        }
        else
        {
            CounterToBreakCombos++;
        }
        
    }
}
