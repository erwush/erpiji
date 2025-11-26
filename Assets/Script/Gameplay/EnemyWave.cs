using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class EnemyWave : MonoBehaviour
{
    public float wave;
    public LootHandler loot;
    public Vector3 enemySpawn;
    public WaveTimer timer;
    public float round;
    public bool isSpawning;
    public GameObject[] enemies;
    public int enemyKind;
    public float lootHealAmount;
    public bool buffChoosing;
    public int totalEnemy;
    public PlayerHealth pleyerh;
    public BuffManager controller;
    public List<GameObject> activeEnemies = new List<GameObject>();
    public TextMeshProUGUI roundText;
    public float healDropChance;

    void Start()
    {
        StartCoroutine(StartRound());
        pleyerh = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        controller = GetComponent<BuffManager>();
        timer = GetComponent<WaveTimer>(); 

    }

    void Update()
    {
        // if (buffChoosing)
        // {
        //     StartCoroutine(SetTime());
        // }
        // else
        // {
        //     Time.timeScale = 1;
        // }
        roundText.text = "Round: " + round + " / Wave: " + wave;
    }

    public IEnumerator SetTime()
    {
        yield return new WaitForSeconds(0.25f);
        Time.timeScale = 0;
    }
    public IEnumerator EndWave()
    {
        controller.buffList.Clear();
        yield return new WaitForSeconds(0.1f);
        buffChoosing = true;
        pleyerh.HealthChange(pleyerh.attr.maxHealth * 0.15f);
        SceneManager.LoadSceneAsync("WaveBuff", LoadSceneMode.Additive);
        yield return new WaitUntil(() => buffChoosing == false);
        SceneManager.UnloadSceneAsync("WaveBuff");
        wave += 1;

    }

    public IEnumerator EndRound()
    {
        controller.buffList.Clear();
        yield return new WaitForSeconds(0.1f);
        pleyerh.HealthChange(pleyerh.attr.maxHealth * 0.70f);
        SceneManager.LoadSceneAsync("RoundBuff", LoadSceneMode.Additive);
        yield return new WaitUntil(() => buffChoosing == false);
        SceneManager.UnloadSceneAsync("RoundBuff");
        wave = 0;
    }

    public IEnumerator StartRound()
    {
        wave = 0;
        round += 1;
        enemyKind += 1;
        totalEnemy += 10;
        yield return new WaitForSeconds(0.1f);
        StartTimer();
    }
    
    public void StartTimer()
    {
        timer.StartCoroutine(timer.Timer());
    }


    public IEnumerator StartWave()
    {
        yield return new WaitForSeconds(0.1f);
        wave += 1;
        StartCoroutine(EnemySpawning());
    }

    public IEnumerator EnemySpawning()
    {
        activeEnemies.Clear();

        for (int i = 0; i < totalEnemy; i++)
        {
            enemySpawn = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            GameObject en = Instantiate(enemies[Random.Range(0, enemyKind)], enemySpawn, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            activeEnemies.Add(en);

            en.GetComponentInChildren<EnemyHealth>().waveScr = this;
        }
    }

    public void EnemyDied(GameObject enemy)
    {
        loot = enemy.GetComponent<LootHandler>();
        float r = Random.value;
        if (r <= (healDropChance/100f))
        {
            Debug.Log(r);
            loot.DropLoot("Heal");
        }
        activeEnemies.Remove(enemy);
        Debug.Log("removed");

        if (activeEnemies.Count == 0)
        {
            Debug.Log("Cleared");
            StartCoroutine(EndWave());
        }
    }
}
