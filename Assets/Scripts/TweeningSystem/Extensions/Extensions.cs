using UnityEngine;

public static class Extensions
{
    public static bool IsNull(this object obj)
    {
        if (obj != null)
        {
            if (obj is Object) return (Object)obj == null;

            return false;
        }

        return true;
    }
}
