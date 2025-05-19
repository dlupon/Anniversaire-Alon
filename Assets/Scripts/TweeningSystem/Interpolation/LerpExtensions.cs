using System.Linq;
using UnityEngine;

public static class LerpExtensions
{
    public static string Whrite(this string pTarget, float pRatio)
    {
        int lLength = (int)(pTarget.Count() * pRatio);
        char[] lChars = pTarget.ToCharArray();
        string lResult = "";

        for (int lcharIndex = 0; lcharIndex < lLength; lcharIndex++)
            lResult += lChars[lcharIndex];

        return lResult;
    }
}
