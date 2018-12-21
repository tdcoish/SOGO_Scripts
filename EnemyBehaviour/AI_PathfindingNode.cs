using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*************************************************************
* This script is used mostly to identify an object. However, we also need
* to store a list of all the nodes any particular node is connected to.
************************************************************* */
public class AI_PathfindingNode : MonoBehaviour {

    // All the nodes we're connected to.
    // [SerializeField]
    public List<AI_PathfindingNode>        connectingNodes;

    // The node that we need to go through to get to us.
    [HideInInspector]
    public int                          previousNodeIndex;

    // The distance to this node from our original node. Used for A*.
    [HideInInspector]
    public float                        disToStart; 

    // The distance directly to the end node. Needed for A*
    [HideInInspector]
    public float                        disToGoal;

    /***********************************************************************************
    We have nodes that are used to flank on a level basis, such as moving through different doors in a room.
    There are also nodes that are used to flank within a room, such as moving to both sides of an obstacle.
    ********************************************************************************** */
    public bool                         isLevelFlankNode;
    public bool                         isRoomFlankNode;

    // Used only for testing that I can go node to node.
    public AI_PathfindingNode GetNextNode(){
        if(connectingNodes.Count!=0){
            Debug.Log("Could this be the problem?");
            return connectingNodes[0];
        }else{
            return null;
        }
    }

    void OnDrawGizmos ()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);

        foreach(var node in connectingNodes){
            Gizmos.DrawLine(transform.position, node.transform.position);
        }
        
    }
}

