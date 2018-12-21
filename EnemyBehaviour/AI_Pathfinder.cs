using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AI_Pathfinder : MonoBehaviour {

    // All the nodes in our level
    [HideInInspector]
    public AI_PathfindingNode[]       nodes;

    // If there are no nodes in the scene, turn this off.
    public bool                       isActive;

    private void Awake(){
        nodes = FindObjectsOfType<AI_PathfindingNode>();
    }
    
    public int GetTargetNodeIndex(AI_PathfindingNode node){
        for(int i=0; i<nodes.Length; i++){
            if(node == nodes[i]){
                return i;
            }
        }

        // Debug.Log("Node not found");
        return -1;
    }

    public AI_PathfindingNode GetNodeFromIndex(int index)
    {
        if(index < 0){
            Debug.Log("Index less than 0 - lksdjf");
            return nodes[0];
        }
        if(index >= nodes.Length){
            Debug.Log("Index greater than length - lksdjf");
            return nodes[0];
        }
        return nodes[index];
    }

    /**************************************************************************************
    Returns the index of the node closest to the enemy.
    No side effects, for now.
    **************************************************************************************/
    public int FindNearestNode(Vector3 pos){
        // Debug.Log("NODES: " + nodes[0].transform.position);
        if(nodes.Length < 1) return -1;
        
        float distance = Vector3.Distance(pos, nodes[0].transform.position);
        int index = 0;

        for(int i=0; i<nodes.Length; i++){
            float tempDis = Vector3.Distance(pos, nodes[i].transform.position);
            if(tempDis < distance){
                distance = tempDis;
                index = i;
            }
        }

        return index;
    }

    /************************************************************************************
    Returns the index of the nearest node that's actually visible.
    ********************************************************************************** */
    public int FindNearestVisibleNode(Vector3 pos){
        if(nodes.Length <= 0){
            Debug.Log("No nodes");
            return -1;
        } 
        
        float distance = 100000f;
        int index = -1;

        // here we check that we can actually see the node. Basically, that no level geometry is in the way.
        for(int i=0; i<nodes.Length; i++){
            if(!CanSeeNode(pos, nodes[i].transform.position)){
                continue;
            } 

            float tempDis = Vector3.Distance(pos, nodes[i].transform.position);
            if(tempDis < distance){
                distance = tempDis;
                index = i;
            }
        }

        return index;
    }

    private bool CanSeeNode(Vector3 pos, Vector3 nodePos){
		Vector3 direction = (nodePos - pos).normalized;

		int layerMask = 1<<LayerMask.NameToLayer("WALL");

		RaycastHit hit;

		float disToGoal = Vector3.Distance(nodePos, pos);

		if(Physics.Raycast(pos, direction, out hit, disToGoal, layerMask)){
			float disToWall = Vector3.Distance(pos, hit.point);
			// Debug.DrawLine(pos, hit.point, Color.magenta, 0.05f);
			if(disToWall < disToGoal){
				return false;
			}
		}
		return true;
    }


    /************************************************************************************************** 
    Set the distances inside of each node as the shortest path from the starting node. When we finally do this 
    for the goal node, then we call SetPathIndexes, and we build a list of indexes that serve as our path.
    We can now move through that path as we wish.
    ************************************************************************************************* */
    public List<int> GenerateNodePath(int startIndex, int goalIndex, out bool success){
        SetNodeDistances(startIndex, goalIndex);
        return SetPathIndexes(startIndex, goalIndex, out success);
    }

    // Overloaded function, finds path using FindClosestNode
    public List<int> GenerateNodePath(Vector3 startPos, Vector3 goalPos, out bool success){
        int startIndex = FindNearestNode(startPos);
        int goalIndex = FindNearestNode(goalPos);
        SetNodeDistances(startIndex, goalIndex);
        return SetPathIndexes(startIndex, goalIndex, out success);
    }

    /**************************************************************************************************************
    Sets the distances of the nodes only relative to the current active starting node. If using A*, will also take into 
    account the straight distance to the goal node. Will be different with different starting and goal node indexes.

    For now, also return the number of iterations it took, purely for telemetry purposes.
    **************************************************************************************************************/
    public int SetNodeDistances(int startIndex, int goalIndex){
        if(startIndex < 0) return -1;

        // 1.Assign to every node a tentative distance of infinity, or some huge number, ie. 10000
        // Set this tentative distance to zero for our original starting node.
        for(int i=0; i<nodes.Length; i++){
            nodes[i].disToStart = 10000f;
            // nodes[i].disToGoal = Vector3.Distance(nodes[i].transform.position, nodes[FindNearestNode()].transform.position);
        }
        // needed for A*. Let's calculate the time required.
        CalculateNodeStraightDistance(goalIndex);
        
        // set the distance of the very starting node to 0
        nodes[startIndex].disToStart = 0f;

        // 2.Keep a set of visited node indexes. This will start with just the initial node
        // booleans initialized to false by default.  
        bool[] visited = new bool[nodes.Length];     
        for(int i=0; i<visited.Length; i++) visited[i] = false;       

        // this just helps us with error checking. Later, if we hit a -1 node, then there is no correct path.
        ResetPreviousNodes();
        
        bool foundPath = false;

        while(!foundPath){

            // 3. Get the unvisited node with the lowest tentative distance. Make this the current node to work with.
            int curNode = -1;
            curNode = FindSmallestUnvisitedNodeAStar(visited);

            //now that we have the correct node, visit all of its neighbours, update their distances if appropriate.
            UpdateNeighbourDistances(curNode);
            
            // Now that we've touched ALL the neighbours and potentially updated their distances, mark this index as having been visited.
            visited[curNode] = true;

            // If the destination node has been marked visited, the algorithm has finished.
            // This code will actually do one more iteration than necessary, since we already updated the goal node's neighbours, 
            // when we could have just returned.
            if(visited[goalIndex]){
                // I mean I could just return but this is clearer I believe
                foundPath = true;
            }

        }
        return -1;
    }

    /***********************************************************************************************
    Iterate through all the nodes and find the one with the smallest distance. Ignore all nodes that 
    have already been visited. The node with the smallest tentative distance that hasn't yet been visited
    is our new node to work with. Wherever we call this function we need to check that the node returned 
    is not the goal node. If it is then we have already built our path.
    ********************************************************************************************** */
    private int FindSmallestUnvisitedNodeDjikstra(bool[] visitedIndexes){
        float nodeDis = 1000000f;
        int curNode = -1;
        for(int i=0; i<nodes.Length; i++){
            if(visitedIndexes[i]){
                continue;
            }
            // this is not a visited node
            else{
                // getting the node with the shortest distance
                if(nodes[i].disToStart < nodeDis){
                    nodeDis = nodes[i].disToStart;
                    curNode = i;
                }
            }
        }

        if(curNode == -1){
            Debug.Log("Woah, for some reason we couldn't find an appropriate node");
        }

        // check if the returned node is equal to our goal node in the function that calls this.
        return curNode;
    }

    /***********************************************************************************************************
    No Side Effects.

    Iterate through all the nodes and find the one with the smallest distance. Ignore all nodes that 
    have already been visited. The node with the smallest tentative distance that hasn't yet been visited
    is our new node to work with. Wherever we call this function we need to check that the node returned 
    is not the goal node. If it is then we have already built our path.

    Will get the next node using the A* heuristic of distance from starting node + straight distance from finish. 
    It will return the index of the node with the smallest combined distance.
    ***********************************************************************************************************/    
    private int FindSmallestUnvisitedNodeAStar(bool[] visitedIndexes){
        float nodeDis = 1000000f;
        int curNode = -1;
        for(int i=0; i<nodes.Length; i++){
            if(visitedIndexes[i]){
                continue;
            }
            // this is not a visited node
            else{
                float heuristicDis = nodes[i].disToStart + nodes[i].disToGoal;
                // getting the node with the shortest distance
                if(heuristicDis < nodeDis){
                    nodeDis = nodes[i].disToStart;
                    curNode = i;
                }
            }
        }

        if(curNode == -1){
            Debug.Log("Woah, for some reason we couldn't find an appropriate node");
        }

        // check if the returned node is equal to our goal node in the function that calls this.
        return curNode;
    }

    /*******************************************************************************************************************
    Side Effects. 
    For the current node, consider all of its unvisited neighbors and calculate 
    (distance to the current node) + (distance from current node to neighbor)
    if this value is less than those nodes current tentative value replace their tentative value with the new value.
    If so, also add the index of the curNode, since that is needed to rebuild the path afterwards.

    Performance: I tested by looping 10000 times through this. Turns out this code takes about 15-20 microseconds to run.
    That seems like nothing, but I'm honestly stumped as to how it could possibly take that long. We're iterating through a list
    of probably 3 nodes on average, then doing a tiny amount of math. Is this cache miss 101?
    Oh wait, I was misstiming it. Since I iterate through the list x times, multiplying that by 1000 does not give 1000, it 
    gives x000. So if we need to go through the list 19 times that's 19000, divided by the ms time of something like 80ms and 
    that's actually not so bad. Around 4-5 microseconds. That last point also explains why we see the biggest gain from A* here,
    since we lower the multiple more by just not iterating through so many nodes. 
    *******************************************************************************************************************/
    private void UpdateNeighbourDistances(int curNode){

        // now that we have the correct node, visit ALL of its neighbours, update their distances if appropriate.
        for(int i=0; i<nodes[curNode].connectingNodes.Count; ++i){

            // Get the distance of the path from our current node, to the next connecting node.
            float dis = Vector3.Distance(nodes[curNode].transform.position, nodes[curNode].connectingNodes[i].transform.position);

            // remember to add the distance of the current node as well.
            dis += nodes[curNode].disToStart;

            // first check if the distance is lower, could already have a distance in there. If so, put in the distance
            if(dis < nodes[curNode].connectingNodes[i].disToStart){

                nodes[curNode].connectingNodes[i].disToStart = dis;

                // also, put in the node index that went to us. Used to trace back the path of course.
                nodes[curNode].connectingNodes[i].previousNodeIndex = curNode;
            }
        }
    }

    /*****************************************************************************************************************
    This exists so that I can quickly know if a path is invalid. If all the unvisited nodes have their previous node index
    set to -1, then I can check that in SetPathIndexes to break out of an incomplete path, should there not be one.
    ***************************************************************************************************************** */
    private void ResetPreviousNodes(){
        for(int i=0; i<nodes.Length; i++){
            nodes[i].previousNodeIndex = -1;
        }
    }


    /*******************************************************************************************************************
    No Side Effects.
    This should only be called after we've already set the nodes correctly. If we plug in the start and end nodes we can
    work back to build a path. Since we've written the visiting node into the node we can recreate the path taken.
    eg. A -> D -> B -> Z -> C
    Starting backwards from the goalIndex and going until we hit the start index. Although we then have to read the list back
    to front.

    Currently there's an error. If you can't make a path then we'll try to add nodes infinitely.
    ********************************************************************************************************************/
    private List<int> SetPathIndexes(int startIndex, int goalIndex, out bool success){
        success = true;

        List<int> nodePathIndexes = new List<int>();

        int curIndex = goalIndex;

        nodePathIndexes.Add(curIndex);

        // work back to start, adding another index each time.
        while(curIndex != startIndex){
            curIndex = nodes[curIndex].previousNodeIndex;

            if(curIndex == -1){
                // this means there is no valid path. Break out of all this.
                // Debug.Log("NO VALID PATH, MOVING TO CLOSEST NODE");
                nodePathIndexes.Clear();
                nodePathIndexes.Add(startIndex);
                success = false;
                return nodePathIndexes;
            }

            nodePathIndexes.Add(curIndex);
        }

        // since we went goal -> intermediates -> start, we need to flip the order of everything around.
        nodePathIndexes.Reverse();

        return nodePathIndexes;
    }

    private void CalculateNodeStraightDistance(int goalIndex){
        if(goalIndex >= nodes.Length){
            Debug.Log("Illegal node, past end of array");
            return;
        }
        if(goalIndex < 0){
            Debug.Log("Negative index");
            return;
        }
        for(int i=0; i<nodes.Length; i++){
            // calculate each node x times.
            nodes[i].disToGoal = Vector3.Distance(nodes[goalIndex].transform.position, nodes[i].transform.position);
        }
    }

}
