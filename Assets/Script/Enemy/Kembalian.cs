using UnityEngine;

public class HitungKembalianFor : MonoBehaviour
{
    public int totalBelanja = 75000;
    public int uangDiberikan = 100000;

    void Start()
    {
        Debug.Log("Total belanja: " + totalBelanja);
        Debug.Log("Uang dibayarkan: " + uangDiberikan);
        Hitung(totalBelanja, uangDiberikan);
    }

    void Hitung(int totalBelanja, int uangDiberikan)
    {
        int[] pecahan = { 100000, 50000, 20000, 10000, 5000, 2000, 1000, 500, 200, 100 };
        int kembalian = uangDiberikan - totalBelanja;

        if (kembalian < 0)
        {
            Debug.Log("Uang tidak cukup!");
            return;
        }

        Debug.Log("Kembalian:");
        for (int i = 0; i < pecahan.Length; i++)
        {
            int jumlah = kembalian / pecahan[i];
            if (jumlah > 0)
            {
                Debug.Log(jumlah + " lembar uang " + pecahan[i]);
                kembalian %= pecahan[i];
            }
        }
    }
}
