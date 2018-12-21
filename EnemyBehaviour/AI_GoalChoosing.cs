// using System.Collections.Generic;
using UnityEngine;


// /*****************************************************************************************
// This is just a list of actions. This serves the purpose of a struct field and exists only 
// to store data.

// The data stored is the score that each action has received. We will also have an innate 
// desire for each enemy type.
// ***************************************************************************************** */
// public class AI_GoalChoosing : MonoBehaviour 

//     [Header("Fill this in with goal prefabs")]
//     [SerializeField]
//     private SO_AI_Move_Goal[]           mMoveGoals;

//     // I mean, for now it's just fire and don't fire.
//     [SerializeField]
//     private SO_AI_Wep_Goal[]            mWepGoals;

//     protected AI_Controller   mCont;

//     private void Awake(){
//         mCont = GetComponent<AI_Controller>();
//     }

//     private string FindCommandFromGoals(SO_AI_Goal[] goals, AI_Conditions conResults){
//         string cmdName = "";
//         float highScore = -100000000f;
//         for(int i=0; i<goals.Length; i++){
//             float score = goals[i].ScoreConditions(conResults);

//             if(score > highScore){
//                 highScore = score;
//                 cmdName = goals[i].GetGoalCommandName();
//             }
//         }

//         return cmdName;
//     }

//     public void FindHighestScoringMovementGoal(AI_Conditions conResults){

//         string cmdName = FindCommandFromGoals(mMoveGoals, conResults);
    
//         // For now just display the correct goal, eventually call the correct function.
//         // Invoke("cmdName", -1f);
//         if(cmdName == "MoveToPlayer"){
//             MoveToPlayer();
//         }
//         else if(cmdName == "MoveToFireDistance"){
//             MoveToFireDistance();
//         }
//         else if(cmdName == "Strafe"){
//             Strafe();
//         }
//         else if (cmdName == "DontMove"){
//             DontMove();
//         }
//         else if (cmdName == "Charge"){
//             Charge();
//         }
//         else{
//             Debug.Log("Incorrect function name, movement goal");
//         }
//     }

//     public void FindHighestScoringWeaponGoal(AI_Conditions conResults){

//         string cmdName = FindCommandFromGoals(mWepGoals, conResults);
//         // Debug.Log("Command: " + cmdName);

//         // Invoke("cmdName", -1f);
//         if(cmdName == "FireWeapon") FireWeapon();
//         if(cmdName == "DontFireWeapon") DontFireWeapon();
//     }

//     /***************************************************************************************************
//     Actions implemented here.
//     ************************************************************************************************** */
//     public void MoveToPlayer(){
//         mCont.mGoalPos = mCont.mPlayerTrans.Value.position;
//         // set velocity to higher.
//         mCont.mCurMaxVel = mCont.GetBase().mMaxSpd;
//     }

//     public void MoveToFireDistance(){
//         // Find a spot in the direction from the player/goal to the enemy, behind the enemy
//         Vector3 dest = new Vector3();
//         Vector3 dif = transform.position - mCont.mSpotToAimAt;
//         dif = Vector3.Normalize(dif);
//         dif *= mCont.mGun.mGunProperties.mRange * 0.7f;          // move just within range.
//         dest = mCont.mSpotToAimAt + dif;

//         mCont.mGoalPos = dest;

//         // set velocity
//         mCont.mCurMaxVel = mCont.GetBase().mMaxSpd;
//     }

//     /*************************************************************************************************
//     Strafes circularly around the player/goal.

//     Alternate method. Make them choose totally randomly which direction to go in, but then store the time 
//     they made that decision. Then, after a small amount of time, make them choose differently. Then I can
//     just straight up use right/left.
//     *********************************************************************************************** */
//     private void Strafe(){

//         float timeSinceLast = Time.time - mCont.mTimeStartedStrafe;
//         if(timeSinceLast < 1.5f){
//             return;
//         }

//         mCont.mTimeStartedStrafe = Time.time;

//         // Here we do in fact need the AI_Conditions, but only for figuring our strafing left or right.
//         Vector3 dirToPlayer = GetComponent<AI_Controller>().mPlayerTrans.Value.position - transform.position;

//         // Crossing straight up with the direction to the player will give us to the left.
//         Vector3 strafeLeft = Vector3.Cross(transform.up, dirToPlayer);
//         strafeLeft.y = 0;       // assume 2D plane.
//         strafeLeft = Vector3.Normalize(strafeLeft);
//         Debug.DrawLine(transform.position, (Vector3.Normalize(strafeLeft)*10f), Color.cyan, 0.1f);

//         // eventually, store the time, then recalculate if the time is too stale
//         bool left = (Random.value > 0.5f);

//         Vector3 strafeDir = strafeLeft;
//         if(!left){
//             strafeDir *= -1;
//         }

//         Vector3 destSpot = transform.position + (strafeDir*100f);

//         // This is our new goal. Since it updates every frame, there's no reason to worry that we'll be moving too far away.
//         // Note, this method does not update every frame, since it returns at the top.
//         mCont.mGoalPos = destSpot;

//         // set velocity to higher.
//         mCont.mCurMaxVel = mCont.GetBase().mMaxSpd/2f;
//     }

//     // Could be because they got hit by a grenade or something like that.
//     private void DontMove(){
//         mCont.mGoalPos = transform.position;
//     }

//     // Just set some position in front of the tank. Also, set the velocity to x times normal velocity.
//     private void Charge(){

//         if(mCont.GetBase().mChargeTime != null){
//             TNK_Behaviour tnk = (TNK_Behaviour)mCont;

//             // this means that we must be done charging
//             if(Time.time - tnk.mTimeStartedCharge > tnk.GetBase().mChargeTime.Value){
//                 tnk.mTimeStartedCharge = Time.time;

//                 Vector3 dir = tnk.mPlayerTrans.Value.position - transform.position;
//                 dir = Vector3.Normalize(dir);
//                 dir *= 20f;         // basically it tries to get to a spot behind the player.
//                 tnk.mGoalPos = transform.position + dir;
//             }

//             // set velocity to higher.
//             mCont.mCurMaxVel = mCont.GetBase().mMaxSpd * 4;

//             Debug.Log("Charging");
//         }
//     }

//     private void MoveFurtherFromPlayer(){
        
//     }

//     // Weapon actions. Ugh, should be moved to its own class.
//     private void FireWeapon(){
//         // Now fire the weapon, right now only a Ronald Ray Gun
//         // mCont.mGun.TryToFireGun(mCont.mSpotToAimAt);
//         mCont.mGun.TryToFireGun(mCont.mPlayerTrans.Value);
//     }

//     private void DontFireWeapon(){
//         // lul
//     }

//     // This system just hacked in here.
//     public void PlayAudioEvent(AI_Conditions conResults){
//         // if(conResults.mCanSeePlayer){
//         //     AUD_Manager.SetSwitch("Voice", "Spot", gameObject);
//         //     AUD_Manager.PostEvent("VC_ALIEN_ST", gameObject);
//         // }
//     }
    
    
// }
//     // // Helper function
//     // private List<Vector3> GetNodePosInRadius(float rad){
//     //     // find all the nodes in a small radius around the NPC
//     //     List<Vector3> nodPos = new List<Vector3>();
//     //     for(int i=0; i<mCont.pather.nodes.Length; i++){
//     //         Vector3 pos = mCont.pather.nodes[i].transform.position;

//     //         if(Vector3.Distance(pos, transform.position) < rad){
//     //             nodPos.Add(pos);
//     //         }
//     //     }

//     //     return nodPos;
//     // }