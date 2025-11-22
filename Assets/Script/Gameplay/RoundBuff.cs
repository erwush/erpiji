using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class RoundBuff : MonoBehaviour
{
    public TextMeshProUGUI buffText;
    public EnemyWave waveScr;
    public string buffName;
    public string amountText;
    public float buffAmount;
    public PlayerAttribute attr;
    public string[] attribute = { "maxhp", "atk", "atkspd", "spd", "critrate", "critdmg", "def" };
    public string buffedAttribute;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //find player tag
        attr = GameObject.FindWithTag("Player").GetComponent<PlayerAttribute>();
        buffedAttribute = attribute[Random.Range(0, attribute.Length)];
        StartCoroutine(SetBuffAmount());
        waveScr = GameObject.FindWithTag("GameController").GetComponent<EnemyWave>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator SetBuffAmount()
    {

        if (buffedAttribute == "maxhp")
        {
            buffName = "Max Health";
            float r = Random.value;
            if (r < 0.75f)
            {
                buffAmount = 10f;
                amountText = "10";
            }
            else if (r < 0.95f)
            {
                buffAmount = 20f;
                amountText = "20";
            }
            else
            {
                buffAmount = 30f;
                amountText = "30";
            }
        }
        else if (buffedAttribute == "atk")
        {
            buffName = "Attack";
            float r = Random.value;

            if (r < 0.75f)
            {
                buffAmount = 10f;
                amountText = "10%";
            }
            else if (r < 0.90f)
            {
                buffAmount = 15f;
                amountText = "15%";
            }
            else
            {
                buffAmount = 20f;
                amountText = "20%";
            }

        }
        else if (buffedAttribute == "atkspd")
        {
            buffName = "Attack Speed";
            float r = Random.value;

            if (r < 0.5f)
            {
                buffAmount = 3f;
                amountText = "3%";
            }
            else if (r < 0.75f)
            {
                buffAmount = 5f;
                amountText = "5%";
            }
            else if (r < 0.9f)
            {
                buffAmount = 7f;
                amountText = "7%";
            }
            else
            {
                buffAmount = 10f;
                amountText = "10%";
            }
        }
        else if (buffedAttribute == "critrate")
        {
            buffName = "Critical Rate";
            float r = Random.value;
            if (r < 0.5f)
            {
                buffAmount = 5f;
                amountText = "5%";
            }
            else if (r < 0.75f)
            {
                buffAmount = 10f;
                amountText = "10%";
            }
            else if (r < 0.9f)
            {
                buffAmount = 15f;
                amountText = "15%";
            }
            else
            {
                buffAmount = 20f;
                amountText = "20%";
            }
        }
        else if (buffedAttribute == "critdmg")
        {
            buffName = "Critical Damage";
            float r = Random.value;
            if (r < 0.5f)
            {
                buffAmount = 10f;
                amountText = "10%";
            }
            else if (r < 0.75f)
            {
                buffAmount = 15f;
                amountText = "15%";
            }
            else if (r < 0.9f)
            {
                buffAmount = 20f;
                amountText = "20%";
            }
            else
            {
                buffAmount = 25f;
                amountText = "25%";
            }
        }
        else if (buffedAttribute == "def")
        {
            buffName = "Defense";
            float r = Random.value;
            if (r < 0.5f)
            {
                buffAmount = 5f;
                amountText = "5%";
            }
            else if (r < 0.75f)
            {
                buffAmount = 10f;
                amountText = "10%";
            }
            else if (r < 0.9f)
            {
                buffAmount = 15f;
                amountText = "15%";
            }
            else
            {
                buffAmount = 20f;
                amountText = "20%";
            }
        }
        else if (buffedAttribute == "spd")
        {
            buffName = "Speed";
            float r = Random.value;
            if (r < 0.5f)
            {
                buffAmount = 5f;
                amountText = "5%";
            }
            else if (r < 0.75f)
            {
                buffAmount = 10f;
                amountText = "10%";
            }
            else if (r < 0.9f)
            {
                buffAmount = 15f;
                amountText = "15%";
            }
            else
            {
                buffAmount = 20f;
                amountText = "20%";
            }
        }
        yield return new WaitForSeconds(0.1f);
        SetText();
    }

    void SetText()
    {
        buffText.text = "Increase " + buffName + " by " + amountText;
    }

    public void IncreaseAttribute()
    {
        SceneManager.UnloadSceneAsync("RoundBuff");
        attr.IncreaseAttr(buffedAttribute, buffAmount);
        waveScr.buffChoosing = false;
        if(waveScr.wave ==3)
        {
            waveScr.StartCoroutine(waveScr.StartRound());
        } else
        {
            waveScr.StartCoroutine(waveScr.StartWave());
        }
    }
    

}
