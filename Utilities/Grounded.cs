/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/

using UnityEngine;

public class Grounded : MonoBehaviour
{
    [SerializeField]
    private float distance = 1f;

    [SerializeField]
    private bool debugMode = false;

    private void Update()
    {
        if(debugMode)
        {
            Debug.DrawRay(transform.position, Vector3.down * distance, Color.red);
        }
    }
    
    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distance);
    }

    public float DisFromGround(Vector3 pos){
        RaycastHit hit;
        Ray downRay = new Ray(transform.position, Vector3.down);
        if(Physics.Raycast(downRay, out hit)){
            return hit.distance;
        }

        return 0f;
    }

    public float GetSetDis(){
        return distance;
    }

}