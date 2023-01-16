using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    private float secondsCounter = 0;

    private float secondsToCount = 1;
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
            Console.WriteLine("Tecla izquierda pulsada");
            // See if it's valid
            if (IsValidBoard())
				// It's valid. Update grid.
				UpdateBoard();
            else
                // Its not valid. revert.
                transform.position += new Vector3(1, 0, 0);
        }
        // Implement Move Right (key RightArrow)
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Modify position
            transform.position += new Vector3(1, 0, 0);

            // See if it's valid
            if (IsValidBoard())
                // It's valid. Update grid.
                UpdateBoard();
            else
                // Its not valid. revert.
                transform.position += new Vector3(-1, 0, 0);
        }
        // Implement Rotate, rotates the piece 90 degrees (Key UpArrow)
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Modify position
            transform.Rotate(0, 0, 90);

            // See if it's valid
            if (IsValidBoard())
                // It's valid. Update grid.
                UpdateBoard();
            else
                // Its not valid. revert.
                transform.Rotate(0, 0, -90);
        }
        // Implement move Downwards and Fall (each second)
        else if (Input.GetKeyDown(KeyCode.DownArrow) || (secondsCounter += Time.deltaTime) >= secondsToCount)
        {
            // Modify position
            transform.position += new Vector3(0, -1, 0);

            // See if it's valid
            if (IsValidBoard())
                // It's valid. Update grid.
                UpdateBoard();
            else
                // Its not valid. revert.
                transform.position += new Vector3(0, 1, 0);
            secondsCounter = 0;
        }
    }

    // TODO: Updates the board with the current position of the piece. 
    void UpdateBoard()
    {
        // First you have to loop over the Board and make current positions of the piece null.
        for (int y = 0; y < Board.h; ++y)
        {
            for (int i = 0; i < Board.w; ++i)
            {
                if (Board.grid[i, y] != null)
                {
                    if (Board.grid[i, y].transform.parent == transform)
                        Board.grid[i, y] = null;
                }
            }
                
        }
        // Then you have to loop over the blocks of the current piece and add them to the Board.
        foreach (GameObject block in transform) {
            Vector2 v = Board.RoundVector2(block.transform.position);
            Board.grid[(int)v.x, (int)v.y] = block;
        }  
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
