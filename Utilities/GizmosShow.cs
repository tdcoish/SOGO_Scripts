/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/

using UnityEngine;

public class GizmosShow : MonoBehaviour
{
    [SerializeField]
    private bool showGizmos = true;
    [SerializeField, Range(0f, 100f)]
    private float range;
    [SerializeField]
    private Color sphereColor = Color.green;
    [SerializeField]
    private Color forwardLineColor = Color.blue;
    
    private void OnDrawGizmos()
    {
        if(!showGizmos) return;

        Gizmos.color = sphereColor;
        Gizmos.DrawWireSphere(transform.position, range);

        Gizmos.color = forwardLineColor;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * range);
    }
}