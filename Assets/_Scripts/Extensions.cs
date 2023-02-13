using UnityEngine;

public static class Extensions
{
    public static LayerMask layerMask = LayerMask.GetMask("Default");

    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction)
    {
        if (rigidbody.isKinematic) return false;

        float radius = .05f;
        float distance = .5f;
        Vector3 origin = rigidbody.position;
        RaycastHit2D hit = Physics2D.CircleCast(origin, radius, direction.normalized, distance, layerMask);        

        return hit.collider != null && hit.rigidbody != rigidbody;
    }

    public static bool DotProduct(this Transform transform, Transform other, Vector2 testDirection)
    {
        var direction = other.position - transform.position;
        return Vector2.Dot(direction.normalized, testDirection) > 0.25f;
    }
}
