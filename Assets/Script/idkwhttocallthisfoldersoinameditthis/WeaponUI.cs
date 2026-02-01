using UnityEngine;
using System.Collections.Generic;

public class WeaponUI : MonoBehaviour
{
    public List<string> buffList;
    public List<Sprite> spriteList;
    public SpriteRenderer selfSprite;

    public BuffManager buffManager;
    public Dictionary<string, Sprite> buffSprites = new Dictionary<string, Sprite>();

    void Start()
    {
        selfSprite = GetComponent<SpriteRenderer>();
        buffManager = GameObject.FindWithTag("GameController").GetComponent<BuffManager>();

        // Ambil buffList dari BuffManager
        buffList = buffManager.buffList;

        BuildSpriteDictionary();
    }

    void Awake()
    {
        buffSprites = new Dictionary<string, Sprite>();
    } 

    private void BuildSpriteDictionary()
    {
        buffSprites.Clear();

        // Masukkan semua sprite dulu
        foreach (var spritev in spriteList)
        {
            if (!buffSprites.ContainsKey(spritev.name))
            {
                buffSprites.Add(spritev.name, spritev);
                Debug.Log("added");
            }



            else
            {
                
                Debug.LogWarning($"Duplicate sprite name: {spritev.name}");
            }

        }


    }

 // variabel baru

void Update()

    {

    // kalau activeBuff kosong, jangan lakukan apa-apa
    if (string.IsNullOrEmpty(buffManager.activeBuff))
        return;

    // cek apakah dictionary punya sprite untuk buff tersebut
    if (buffSprites.TryGetValue(buffManager.activeBuff, out Sprite buffIcon))
    {
        selfSprite.sprite = buffIcon;
    }
}

}
