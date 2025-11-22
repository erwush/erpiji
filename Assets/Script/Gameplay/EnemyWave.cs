using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class EnemyWave : MonoBehaviour
{
    public float wave;
    public Vector3 enemySpawn;
    public float round;
    public bool isSpawning;
    public GameObject[] enemies;
    public int enemyKind;
    public bool buffChoosing;
    public int totalEnemy;
    public PlayerHealth pleyerh;
    public List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        StartCoroutine(StartRound());
        pleyerh = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();

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
    }

    public IEnumerator SetTime()
    {
        yield return new WaitForSeconds(0.25f);
        Time.timeScale = 0;
    }
    public IEnumerator EndWave()
    {
        buffChoosing = true;
        pleyerh.health = pleyerh.attr.maxHealth;
        SceneManager.LoadSceneAsync("WaveBuff", LoadSceneMode.Additive);
        yield return new WaitUntil(() => buffChoosing == false);
        SceneManager.UnloadSceneAsync("WaveBuff");
        wave += 1;

    }

    public IEnumerator EndRound()
    {
        SceneManager.LoadSceneAsync("RoundBuff", LoadSceneMode.Additive);
        yield return new WaitUntil(() => buffChoosing == false);
        SceneManager.UnloadSceneAsync("RoundBuff");
        round += 1;
        StartCoroutine(StartRound());
    }

    public IEnumerator StartRound()
    {
        wave = 0;
        enemyKind += 1;
        totalEnemy += 10;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(StartWave());
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
        activeEnemies.Remove(enemy);
        Debug.Log("removed");

        if (activeEnemies.Count == 0)
        {
            Debug.Log("Cleared");
            StartCoroutine(EndWave());
        }
    }
}
