using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance => _instance;
    public List<GameObject> ListBarrières = new List<GameObject>();
    public TextMeshProUGUI RemainingTime;
    public TextMeshProUGUI PointsText;

    private int Round;
    public List<string> brandName = new List<string>();
    [SerializeField] private List<GameObject> DeliveryMansToAdd = new List<GameObject>();
    public Spawn spawn;

    private float points;
    public float Points
    {
        get { return points; }
        set
        {
            points = value + points;
            PointsText.text = "" + points;
            if (Round == 1 && points == 5)
            {
                NewRound();
            }
            else if (Round == 2 && points == 10)
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
    }

    // Update is called once per frame
    void Update()
    {
        timeBeforeLast += Time.deltaTime;
        if (timeBeforeLast > timeMultiplator)
        {
            timeBeforeLast = 0;
            times = -1;
        }

    }
    void Defeat()
    {
        Debug.Log("defeat");
    }

    void NewRound()
    {
        Round++;
        if (Round == 2)
        {
            AddBrand("");
        }
        if (Round == 3)
        {
            AddBrand("");
        }
    }

    void AddBrand(string brand)
    {
        brandName.Add("");
        foreach (GameObject Man in DeliveryMansToAdd)
        {
            if (Man.GetComponent<DeliveryMan>().BrandName == "")
            {
                spawn.DeliveryManList.Add(Man);

            }
        }
    }
}
