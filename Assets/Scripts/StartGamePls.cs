using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGamePls : MonoBehaviour
{
    void Start()
    {
        // Display the game info message
        /*
        Debug.Log("Lively Game is where you find the limits of your critical thinking in puzzles.");
        Debug.Log("Copyright (C) 2023 The Lively Developers Community");
        Debug.Log("");
        Debug.Log("This program is free software: you can redistribute it and/or modify");
        Debug.Log("it under the terms of the GNU General Public License as published by");
        Debug.Log("the Free Software Foundation, either version 3 of the License, or");
        Debug.Log("(at your option) any later version.");
        Debug.Log("");
        Debug.Log("This program is distributed in the hope that it will be useful,");
        Debug.Log("but WITHOUT ANY WARRANTY; without even the implied warranty of");
        Debug.Log("MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the");
        Debug.Log("GNU General Public License for more details.");
        Debug.Log("");
        Debug.Log("You should have received a copy of the GNU General Public License");
        Debug.Log("along with this program.  If not, see https://www.gnu.org/licenses/.");*/

        // Find the "OK!" button and attach the StartGame function to it
        GameObject okButton = GameObject.Find("StartGame");
        if (okButton != null)
        {
            okButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(StartGame);
        }
        else
        {
            Debug.LogWarning("Could not find the OKButton GameObject.");
        }
    }
    // Start the game
    void StartGame()
    {
        SceneManager.LoadScene("Test");
    }
}
