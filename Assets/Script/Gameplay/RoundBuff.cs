using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class RoundBuff : MonoBehaviour
{
    public TextMeshProUGUI blessingText;
    public EnemyWave waveScr;
    public string blessingName;
    public string descText;
    public TextMeshProUGUI blessingDesc;
    public string amountText;
    public float buffAmount;
    public PlayerAttribute attr;
    public string[] blessing = { "Lifesteal", "CritHit", "Burn" };
    public WeaponTest3 weapon;

    public string buffedBlessing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //find player tag
        attr = GameObject.FindWithTag("Player").GetComponent<PlayerAttribute>();
        weapon = GameObject.FindWithTag("Player").GetComponent<WeaponTest3>();
        waveScr = GameObject.FindWithTag("GameController").GetComponent<EnemyWave>();
        buffedBlessing = blessing[Random.Range(0, blessing.Length)];
        StartCoroutine(SetBlessing());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator SetBlessing()
    {
        if (buffedBlessing == "Lifesteal")
        {
            blessingName = "Lifesteal";
            descText = "Gain " + amountText + " Lifesteal";
        }
        else if (buffedBlessing == "CritHit")
        {
            blessingName = "CritHit";
            descText = "Gain " + amountText + " CritHit";
        }
        else if (buffedBlessing == "Burn")
        {
            blessingName = "Burn";
            descText = "Gain " + amountText + " Burn";
        }
        yield return new WaitForSeconds(0.1f);
        SetText();
    }

    void SetText()
    {
        blessingText.text = blessingName;
        blessingDesc.text = descText;
    }

    public void GiveBlessing()
    {
        if (buffedBlessing == "Lifesteal")
        {
            weapon.bless = WeaponTest3.ActiveBlessing.Lifesteal;
        }
        else if (buffedBlessing == "CritHit")
        {
            weapon.bless = WeaponTest3.ActiveBlessing.CritHit;
        }
        else if (buffedBlessing == "Burn")
        {
            weapon.bless = WeaponTest3.ActiveBlessing.Burn;
        }
        SceneManager.UnloadSceneAsync("RoundBuff");

        waveScr.buffChoosing = false;

        waveScr.StartCoroutine(waveScr.StartRound());
    }


}
