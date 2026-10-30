using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int button_number;

    public void OpenLevel()
    {
        PlayerPrefs.SetInt("button_number", button_number);
        SceneManager.LoadScene("SampleScene");
    }
}
