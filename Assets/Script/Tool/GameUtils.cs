using UnityEngine;


public static class GameUtils

{



    public static int FloatArrayChecker(float[] array, float value, float tolerance = 1f)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (Mathf.Abs(array[i] - value) < tolerance)
            {
                return i;
            }
        }
        return -1;
    }
    public static int ArrayChecker<T>(T[] array, T value)
    {

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].Equals(value))
            {
                return i;
            }
        }

        return -1;
    }


    public static float DamageApplier(float atk, float def, float dmgType, float dmgRes, float elemDmg, float elemRes, float critRate = 0f, float critDmg = 0f, int typeIdx = 0)
    {
        float finalDmg;
        if (Random.value < (critRate / 100f))
        {
            if (typeIdx == 2)
            {
                finalDmg = atk * (1f + critDmg / 100f) * (dmgType / 100f) * (elemDmg / 100f);
            }
            else
            {

                finalDmg = atk * (1f + critDmg / 100f) * (dmgType / 100f) * (elemDmg / 100f) * (1f - dmgRes / 100f) * (1f - elemRes / 100f) - (def * 0.1f);
            }
        } else {
            if (typeIdx == 2)
            {
                finalDmg = atk * (dmgType / 100f) * (elemDmg / 100f);
            }
            else
            {
                finalDmg = atk * (dmgType / 100f) * (elemDmg / 100f) * (1-dmgRes/100f) * (1-elemRes/100f) - (def*0.1f);
            }
        }
        if (finalDmg <= 0f)
        {
            finalDmg = 1;
        }
        return finalDmg;
    }
}
