using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullCheck
{
 

    internal static void CheckIfNull<T>(T t,Type type,  Component fromClass, string gameObjectName = null)
    {
        if (t == null)
        {
            if(gameObjectName == null)
            {
                throw new MissingComponentException($"Component {type} missing in {fromClass}");
            } else
            {
                throw new MissingComponentException($"Component {type} missing in {fromClass}, GameObject name = {gameObjectName}");
            }
        }
    }
}
