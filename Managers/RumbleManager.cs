using XInputDotNetPure;
using UnityEngine;

public class RumbleManager : SingletonBehaviour<RumbleManager>
{	
	private bool playerIndexSet = false;
    private PlayerIndex playerIndex;
    private GamePadState currentGamePadState;
    private GamePadState prevGamePadState;

	private float rumblingTimeLimit = 0f;
	private float lastRumbletimestamp = 0f;
	private bool isRumbling = false;
    
	protected override void SingletonAwake()
    {
        
    }

	private void Update()
    {
        FindConnectedController();
    }

	private void FixedUpdate() {
		if (isRumbling) {
			float timeToFinishRumbling = 1f - Time.time.Normalize(lastRumbletimestamp, lastRumbletimestamp + rumblingTimeLimit);
			GamePad.SetVibration(playerIndex, timeToFinishRumbling, timeToFinishRumbling);
			isRumbling = Time.time <= lastRumbletimestamp + rumblingTimeLimit;
		}
	}

	public void StartRumble(object time) {
		if (isRumbling == false) {
			isRumbling = true;
		}
		lastRumbletimestamp = Time.time;
		rumblingTimeLimit = (float) time;
	}

    private void FindConnectedController()
    {
		// Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!playerIndexSet || !prevGamePadState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }
		prevGamePadState = currentGamePadState;
        currentGamePadState = GamePad.GetState(playerIndex);
    }
    
    private void OnApplicationQuit()
    {
        StopRumbling(null);
    }

    public void StopRumbling(object data) {
        GamePad.SetVibration(playerIndex, 0f, 0f);
        isRumbling = false;
        lastRumbletimestamp = 0f;
    }

}
