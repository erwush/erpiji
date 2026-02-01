using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : MonoBehaviour
{
    public float damage = 10f;
    public int maxChains = 5;             // maksimal pantulan
    public float chainRadius = 3f;        // radius mencari target berikut
    public LayerMask enemyLayer;

    public void TriggerChainLightning(Transform firstTarget)
    {
        StartCoroutine(DoChainLightning(firstTarget));
    }

    private IEnumerator<System.Object> DoChainLightning(Transform currentTarget)
    {
        // List musuh yang sudah terkena, agar tidak kena 2x
        List<Transform> hitTargets = new List<Transform>();

        Transform lastTarget = currentTarget;

        for (int i = 0; i < maxChains; i++)
        {
            if (lastTarget == null) break;

            // Beri damage ke target sekarang
            DealDamage(lastTarget);
            hitTargets.Add(lastTarget);

            yield return new WaitForSeconds(0.1f); // delay antar pantulan (opsional)

            // Cari target lain di sekitar
            Collider2D[] nearby = Physics2D.OverlapCircleAll(
                lastTarget.position,
                chainRadius,
                enemyLayer
            );

            Transform nextTarget = null;
            float closestDist = Mathf.Infinity;

            foreach (Collider2D col in nearby)
            {
                if (!hitTargets.Contains(col.transform))
                {
                    float dist = Vector2.Distance(lastTarget.position, col.transform.position);

                    if (dist < closestDist)
                    {
                        closestDist = dist;
                        nextTarget = col.transform;
                    }
                }
            }

            if (nextTarget == null)
            {
                break; // tidak ada target lagi
            }

            // 🔵 opsional: buat efek visual lightning dari lastTarget ke nextTarget
            SpawnLightningEffect(lastTarget.position, nextTarget.position);

            // lanjut ke target berikutnya
            lastTarget = nextTarget;
        }
    }

    private void DealDamage(Transform enemy)
    {
        var hp = enemy.GetComponent<EnemyHealth>();
        if (hp != null)
        {
            hp.HealthChange(-damage);
        }
    }

    private void SpawnLightningEffect(Vector2 from, Vector2 to)
    {
        // DIISI kalau kamu ada VFX line renderer atau prefab
        Debug.DrawLine(from, to, Color.cyan, 0.15f);
    }
}
