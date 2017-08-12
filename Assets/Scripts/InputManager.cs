using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    GameController gameController;
	void Start ()
    {
        gameController = GameController.Instance;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            gameController.ActiveBase.AttackWithCatapult();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            gameController.ActiveBase.AttackWithRay();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            gameController.ActiveBase.Repair();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            gameController.ActiveBase.Defend();
        }
    }
}
