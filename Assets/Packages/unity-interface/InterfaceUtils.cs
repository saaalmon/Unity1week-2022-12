using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceUtils
{
    public static T[] FindObjectOfInterfaces<T>() where T : class
    {
        List<T> findList = new List<T>();

        foreach (var component in GameObject.FindObjectsOfType<Component>())
        {
            var obj = component as T;

            if (obj == null) continue;

            findList.Add(obj);
        }

        T[] findObjArray = new T[findList.Count];
        int count = 0;

        foreach (T obj in findList)
        {
            findObjArray[count] = obj;
            count++;
        }
        return findObjArray;
    }
}