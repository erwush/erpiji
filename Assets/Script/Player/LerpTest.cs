using UnityEngine;

public class LerpTest : MonoBehaviour
{
    public Vector4 kolor = new Vector4(1f, 1f, 1f, 1f); // putih
    public SpriteRenderer sprite;

    void Update()
    {
        // ubah Vector4 jadi Color untuk dirender
        sprite.color = new Color(kolor.x, kolor.y, kolor.z, kolor.w);

        // kalau belum hitam penuh, terus lerp
        if (kolor.x > 0.01f || kolor.y > 0.01f || kolor.z > 0.01f)
        {
            kolor = Vector4.Lerp(kolor, new Vector4(0f, 0f, 0f, 1f), Time.deltaTime);
        }
    }
}
