using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PyramidControl : MonoBehaviour
{
    public static int slotsOccupied;

    [SerializeField]
    private Transform[] rings;

    [SerializeField]
    private GameObject wiSign;

    [SerializeField]
    private GameObject wrongSign;


    private void Start()
    {
        Drag.PuzzuleDone += CheckResults;
        slotsOccupied = 0;
        wiSign.SetActive(false);
        wrongSign.SetActive(false);
    }

    public void CheckResults()
    {
        if (rings[0].position.y == 1.7f &&
            rings[1].position.y == 0.15f &&
            rings[2].position.y == -1.5f &&
            rings[3].position.y == -3.15f)
        {
            wiSign.SetActive(true);
            Invoke("ReloadGame", 2f);
        }
        else
        {
            wrongSign.SetActive(true);
            Invoke("ReloadGame", 1f);
        }
    }

    private void ReloadGame()
    {
        Drag.PuzzuleDone -= CheckResults;
        SceneManager.LoadScene("SampleScene");
    }
}
