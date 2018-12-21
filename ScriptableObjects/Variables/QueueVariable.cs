/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/


using System.Collections.Generic;
using UnityEngine;

public class QueueVariable<T>
{
    private Queue<T> elements;
    private int elementLimit;

    private void OnEnable() {
        elements = new Queue<T>();
    }

    public void AddElement(T newItem) {
        if (elements.Count == elementLimit) {
            elements.Dequeue();
        } else {
            elements.Enqueue(newItem);
        }
    }

    public T GetNextElement() {
        return elements.Dequeue();
    }

    public int ElementCount() {
        return elements.Count;
    }
}