using UnityEngine;

public static class Helper
{
    public static class Targeting
    {
        public static Collider2D GetNearest(Vector3 origin, Collider2D[] colliders, int targetHits)
        {
            float nearestDist = float.MaxValue;
            Collider2D nearestObj = null;
            for (int i = 0; i < targetHits; i++)
            {
                float distanceFromTower = (colliders[i].transform.position - origin).magnitude;
                if (distanceFromTower < nearestDist)
                {
                    nearestDist = distanceFromTower;
                    nearestObj = colliders[i];
                }
            }
            return nearestObj;
        }
    }
}
