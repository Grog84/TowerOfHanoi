/*
 * Extends the Vector2 and Vector3 components with a function that allows
 * a fast convertion from one to the other
 * 
 * */

using UnityEngine;

public static class Vector3Convertion
{

    public static Vector2 ToVector2(this Vector3 vec3)
    {   
        return new Vector2(vec3.x, vec3.y);
    }

    public static Vector3 ToVector3(this Vector2 vec2, float z)
    {
        return new Vector3(vec2.x, vec2.y, z);
    }

}
