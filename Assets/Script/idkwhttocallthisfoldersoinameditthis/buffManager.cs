using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuffManager : MonoBehaviour
{
    public List<string> buffList = new List<string>();
    public List<string> activeBless = new List<string>();
    public List<string> enemyBuff = new List<string>(); 

    public EnemyWave wavescr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 
    void Awake()
    {
        buffList = new List<string>();
    }

    void Start()
    {
        buffList = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
