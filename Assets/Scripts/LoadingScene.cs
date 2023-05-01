using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public Image loadingBar;
    public Image TipsText;
    public List<Sprite> Tips = new List<Sprite>();
    private float timeBeforeLast;
    // Start is called before the first frame update
    void Start()
    {
        TipsText.sprite = Tips[Random.Range(0, Tips.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        timeBeforeLast += Time.deltaTime;
        if (timeBeforeLast > 3)
        {
            SceneManager.LoadScene("AureScene 1");
        }
        loadingBar.fillAmount = timeBeforeLast/3;
    }
}
