using UnityEngine;

/***************************************************************
This class exists soley to have a way to move the camera around while the game 
is running. It is purely for debugging purposes. This should not, under any circumstances
be mistaken for a true camera controller for any object in the game.
************************************************************* */
public class DEB_FreeCam : MonoBehaviour {

    [SerializeField]
    private InputState input;
    
    [SerializeField]
    private float       _LookSpeed = 2f;

    [SerializeField]
    private float       _TumbleSpeed = 1f;

    [SerializeField]
    private float       _HorizontalMovementSpeed = 10f;

    [SerializeField]
    private float       _VerticalMovementSpeed = 4f;

    [SerializeField]
    private bool        _FreezeRoll = true;

    private bool        _Sprint = false;

    public bool         _FreezeEverything = false;
    

    // attach a camera to whatever object you put this in.
    private void Update(){
        if(!_FreezeEverything){
            CameraRotations();
            MoveCamera();
            Orient();
        }
    }
    
    private void Orient(){
        if(_FreezeRoll){
            Vector3 rot = transform.rotation.eulerAngles;
            rot.z = 0;

            transform.rotation = Quaternion.Euler(rot);
        }
    }

    private void MoveCamera(){

        float mult = 1.0f;

        _Sprint = Input.GetKey(KeyCode.LeftShift);
        if(_Sprint) mult = 2.0f;
        
        // These must be valid in the Unity Editor. If the defaults get changed, then this won't work.
        // float XInput = input.mouseXInput;
        // float ZInput = input.mouseZInput;
        // float YInput = input.mouseYInput;

        float XInput = Input.GetAxis("Horizontal");
        float ZInput = Input.GetAxis("Vertical");
        float YInput = Input.GetAxis("Altitude");

        /// Final velocity will be multiplied if boost active
        Vector3 vel = new Vector3(XInput, 0, ZInput) * _HorizontalMovementSpeed;

        // Vertical movement
        vel.y = YInput * _VerticalMovementSpeed;

        // Set velocity to correct direction
        vel = transform.TransformVector(vel);

        // instead of using a rigidbody, just manually add the velocity to the position.
        transform.position += vel*Time.deltaTime * mult;
    }

    private void CameraRotations()
    {
        /// Rotates player according to mouse movement

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(0, mouseX * _LookSpeed, 0);
        transform.Rotate(-mouseY * _LookSpeed, 0, 0);
        transform.Rotate(0, 0, -mouseX * _TumbleSpeed);// Only valid if _FreezeRoll is true
    }
}
