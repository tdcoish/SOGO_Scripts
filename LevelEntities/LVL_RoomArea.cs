using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***********************************************************************************************************
Enemies need a way to know whether they are in the same room as the player. So we make trigger volumes that encompass 
the room. We then check if we're in the same room as the player, and go from there.
The reason that we need to know that is to switch between graph based pathfinding (nodes, A*), and a more natural 
Force based pathfinding (Boids), when we are already in the same room.
For now, we move directly to the player when in the same room, but this will be replaced with Boids.
This is little more than a glorified tag.
********************************************************************************************************** */
[RequireComponent(typeof(BoxCollider))]
public class LVL_RoomArea : MonoBehaviour {

}
