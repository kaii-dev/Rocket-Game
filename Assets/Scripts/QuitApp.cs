using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApp : MonoBehaviour
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys() //when click esc...quit the game 
    {
        if (Keyboard.current.escapeKey.isPressed)
        {
            Debug.Log("esc is pressed");
            Application.Quit();
        }
    }
}
