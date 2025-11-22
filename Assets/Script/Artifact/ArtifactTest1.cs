//*INCREASE SIZE AND DAMAGE
using System.Collections;
using UnityEngine;

public class ArtifactTest1 : ArtifactBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public enum ArtifactType
    {
        Passive = 0,
        Active = 1
    }
    public ArtifactType artype;
    public Transform Pleyer;
    public float sizeInc;
    public PlayerAttribute attr;
    public Vector3 tempScale;
    public float timer;
    public float duration;

    public bool isActive;

    void Start()
    {
        Pleyer = GetComponent<Transform>();
        attr = GetComponent<PlayerAttribute>();
        tempScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (timer < 0)
        {
            timer = 0;
        }
        if (artype == ArtifactType.Active && Input.GetButtonDown("Artifact1") && isActive == false)
        {
            StartCoroutine(OnUse());
        }
    }

    public IEnumerator OnUse()
    {
        attr.atk *= sizeInc;
        isActive = true;
        timer = duration;
        tempScale.x += sizeInc;
        tempScale.y += sizeInc;
        transform.localScale = tempScale;
        yield return new WaitUntil(() => timer <= 0);
        tempScale.x -= sizeInc;
        tempScale.y -= sizeInc;
        transform.localScale = tempScale;
        attr.atk /= sizeInc;
        isActive = false;
    }
}
