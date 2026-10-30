using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class CorrectAnswer : MonoBehaviour
{
    public Interpreter interpreter;
    public Translator translator;
    public Image panel;
    public GameObject panelGO;

    string[] levelAnswers = { "Hello", "42", "!!!", "65", "AB" };

    void Start()
    {
        panelGO.SetActive(false);
    }
    public void CompareAnswer()
    {
        if (interpreter.output_string == levelAnswers[translator.level - 1])
        {
            if (translator.level == 5 && !interpreter.clear_code.Contains('>') || translator.level == 4 && interpreter.clear_code.Contains('S') || translator.level == 3 && interpreter.clear_code.Contains('[') || translator.level == 2 && interpreter.clear_code.Contains('['))
            {
                Debug.Log("Right");
                StartCoroutine(panelEnum(1.0f, new Color(0, 1, 0, 0.5f)));
            }
            else if (translator.level == 1)
            {
                Debug.Log("Right");
                StartCoroutine(panelEnum(1.0f, new Color(0, 1, 0, 0.5f)));
            }
        }
        else
        {
            Debug.Log("You Invalid");
            Debug.Log(interpreter.output_string);
            StartCoroutine(panelEnum(1.0f, new Color(1, 0, 0, 0.5f)));
        }
    }

    public IEnumerator panelEnum(float sec, Color color)
    {
        panelGO.SetActive(true);
        panel.color = color;
        yield return new WaitForSeconds(sec);
        panel.color = new Color(0, 0, 0, 0);
        panelGO.SetActive(false);
        if (interpreter.output_string == levelAnswers[translator.level - 1]) translator.level++;
        if (translator.level > 5)
        {
            translator.level = 1;
            SceneManager.LoadScene("Path");
        }
    }
}
