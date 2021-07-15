using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterController : MonoBehaviour
{
    [SerializeField]
    public GameObject prefabPlayer;
    private CharacterController2D characterController2D;
    private bool directionRight = true;
    private bool onMoving;
    private bool OnMakeJump;
    public KeyCode action = KeyCode.E; // клавиша действия




    public enum ActionType
    {
        None = 0,
        MoveRight = 1,
        MoveLeft = 2,
        Jump = 3,
        Die = 4,
        Up = 5,
        Down = 6,
        Action = 7

    }

    // Start is called before the first frame update
    void Start()
    {
        characterController2D = prefabPlayer.GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        UpdateKeys("d",ActionType.MoveRight );
        UpdateKeys("a", ActionType.MoveLeft);
        UpdateKeys("w", ActionType.Up);
        UpdateKeys("s", ActionType.Down);
       // UpdateKeys("space", ActionType.Jump);

        if (Input.GetButtonDown("Jump"))     // прыжок
        {
            characterController2D.Jumped = true;
        }


        if (Input.GetKeyDown(action))              //  действие
        {
            characterController2D.isAction = true;
        }

    }
    // Update is called once per frame
    void UpdateKeys(string key, ActionType action)
    {
        if (Input.GetKeyDown(key))
        {
            OnDoAction(action);
        }
        if (Input.GetKeyUp(key))
        {
            OnDoAction(ActionType.None);
        }
    }

    public void OnDoAction(int actionId)
    {
        OnDoAction((ActionType)actionId);
    }

    public void OnDoAction(ActionType action)
    {
        Debug.Log("Действие" + action);
        switch (action)
        {
            case ActionType.None:
                characterController2D.horizontal = 0;
                break;

            case ActionType.MoveRight:
                characterController2D.horizontal = 1;
                break;

            case ActionType.MoveLeft:
                characterController2D.horizontal = -1;
                break;

            case ActionType.Up:
                characterController2D.vertical = 1;
                break;

            case ActionType.Down:
                characterController2D.vertical = -1;
                break;

            case ActionType.Jump:
                characterController2D.Jumped = true;
                break;
        }
    }
}
