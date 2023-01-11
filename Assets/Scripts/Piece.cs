using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Default position not valid? Then it's game over
        if (!IsValidBoard())
		{
            Debug.Log("GAME OVER");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame.
    // Implements all piece movements: right, left, rotate and down.
    void Update()
    {
        // Move Left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Modify position
            transform.position += new Vector3(-1, 0, 0);

            // See if it's valid
            if (IsValidBoard())
				// It's valid. Update grid.
				UpdateBoard();
            else
                // Its not valid. revert.
                transform.position += new Vector3(1, 0, 0);
        }
        // Implement Move Right (key RightArrow)
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Modify position
            transform.position += new Vector3(-1, 0, 0);

            // See if it's valid
            if (IsValidBoard())
				// It's valid. Update grid.
				UpdateBoard();
            else
                // Its not valid. revert.
                transform.position += new Vector3(1, 0, 0);
        }
        // Implement Rotate, rotates the piece 90 degrees (Key UpArrow)
        /*if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Modify position
            transform.rotation += new Vector3(0, 0, 90);

            // See if it's valid
            if (IsValidBoard())
                // It's valid. Update grid.
                UpdateBoard();
            else
                // Its not valid. revert.
                transform.position += new Vector3(0, 0, -90);
        }*/
        // Implement move Downwards and Fall (each second)
    }

    // TODO: Updates the board with the current position of the piece. 
    void UpdateBoard()
    {
        // First you have to loop over the Board and make current positions of the piece null.
        
        // Then you have to loop over the blocks of the current piece and add them to the Board.
    }

    // Returns if the current position of the piece makes the board valid or not
    bool IsValidBoard()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Board.RoundVector2(child.position);

            // Not inside Border?
            if (!Board.InsideBorder(v))
                return false;

            // Block in grid cell (and not part of same group)?
            if (Board.grid[(int)v.x, (int)v.y] != null &&
                Board.grid[(int)v.x, (int)v.y].transform.parent != transform)
                return false;
        }
        return true;
    }
}
