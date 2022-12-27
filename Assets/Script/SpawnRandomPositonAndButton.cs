using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnRandomPositonAndButton: MonoBehaviour
{
    [SerializeField] GameObject house1;
    [SerializeField] GameObject houses;
    [SerializeField] TextMeshProUGUI fullText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] Image image;
    [SerializeField] Button timerButton;

    public List<GameObject> blocks = new List<GameObject> ();

    private Component[] isAvailableForSpawns;

    private float elapsedTime = 0f;
    private float timer = 10f;

    private bool isSpawn;
    private bool isClicked = false;

    private void Awake()
    {
        isAvailableForSpawns = GetComponentsInChildren<BlockIsAvailableForSpawn>();
    }

    private void Start()
    {
        timerButton.onClick.AddListener(TimerButtonTrue);
        StartCoroutine(SpawnHouse1());
    }


    private void Update()
    {
        foreach (BlockIsAvailableForSpawn isAvailableForSpawn in isAvailableForSpawns)
        {
            if (isAvailableForSpawn.IsAvailable && !blocks.Contains(isAvailableForSpawn.gameObject))
            {
                blocks.Add(isAvailableForSpawn.gameObject);
            }

            if (!isAvailableForSpawn.IsAvailable)
            {
                blocks.Remove(isAvailableForSpawn.gameObject);
            }
        }

        StartButton();
    }

    IEnumerator SpawnHouse1()
    {   
        while(true)
        {
            yield return new WaitUntil(() => image.fillAmount >= 1);

            if (blocks.Count > 0 && isSpawn)
            {
                GameObject block = blocks[Random.Range(0, blocks.Count)];
                Vector3 House1Position = new Vector3(block.transform.position.x,
                                                             -9.01f, block.transform.position.z);
                GameObject spawnHouse = Instantiate(house1, House1Position, Quaternion.identity);
                spawnHouse.transform.parent = houses.transform;
                isSpawn = false;
            }
        }
    
    }

    void TimerButtonTrue()
    {
        isClicked = true;
    }

    public void StartButton()
    {
        if(isClicked)
        {
            float offsetTime = 1f;
            elapsedTime = elapsedTime + offsetTime;
            timer = timer - offsetTime;
            isClicked = false;
        }

        else
        {
            if (blocks.Count > 0)
            {
                fullText.enabled = false;
                timerText.enabled = true;

                if (image.fillAmount >= 1)
                {
                    image.fillAmount = 0;
                    elapsedTime = 0;
                    timer = 10f;
                }

                else
                {
                    isSpawn = true;
                    elapsedTime += Time.deltaTime;
                    image.fillAmount = elapsedTime / 10;

                    timer -= Time.deltaTime;
                    timerText.text = timer.ToString("F0");
                }
            }

            else
            {
                isSpawn = false;
                fullText.enabled = true;
                timerText.enabled = false;
                image.fillAmount = 1f;
            }
        }
    }
}
