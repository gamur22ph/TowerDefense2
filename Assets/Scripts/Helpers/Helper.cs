using UnityEngine;

public static class Helper
{
    public static class Targeting
    {
        public static Collider2D GetClosest(Vector3 origin, Collider2D[] colliders, int targetHits)
        {
            float closestDist = float.MaxValue;
            Collider2D closestObj = null;
            for (int i = 0; i < targetHits; i++)
            {
                float distanceFromTower = (colliders[i].transform.position - origin).magnitude;
                if (distanceFromTower < closestDist)
                {
                    closestDist = distanceFromTower;
                    closestObj = colliders[i];
                }
            }
            return closestObj;
        }

        public static Collider2D GetFirst(Vector3 origin, Collider2D[] colliders, int targetHits)
        {
            float highestProgress = 0;
            Collider2D firstObj = null;
            for (int i = 0; i < targetHits; i++)
            {
                float progress = colliders[i].GetComponent<Enemy>().progress;
                if (progress > highestProgress)
                {
                    highestProgress = progress;
                    firstObj = colliders[i];
                }
            }
            return firstObj;
        }

        public static Collider2D GetLast(Vector3 origin, Collider2D[] colliders, int targetHits)
        {
            float LowestProgress = float.PositiveInfinity;
            Collider2D lastObj = null;
            for (int i = 0; i < targetHits; i++)
            {
                float progress = colliders[i].GetComponent<Enemy>().progress;
                if (progress < LowestProgress)
                {
                    LowestProgress = progress;
                    lastObj = colliders[i];
                }
            }
            return lastObj;
        }

        public static Collider2D GetStrongest(Vector3 origin, Collider2D[] colliders, int targetHits)
        {
            float highestHealth = 0;
            Collider2D strongObj = null;
            for (int i = 0; i < targetHits; i++)
            {
                float health = colliders[i].GetComponent<Enemy>().Health.currentHealth;
                if (health > highestHealth)
                {
                    highestHealth = health;
                    strongObj = colliders[i];
                }
            }
            return strongObj;
        }
    }
}
