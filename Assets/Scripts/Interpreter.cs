using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Interpreter : MonoBehaviour
{
    public Text codeText;
    public string clear_code;
    public bool error_code;
    public string output_string = "";

    public CorrectAnswer correctAnswer;

    int[] tapes = { 0 };

    public void Lexer()
    {
        if (codeText == null || string.IsNullOrEmpty(codeText.text))
        {
            clear_code = "";
            return;
        }
        output_string = "";
        clear_code = RemoveComments(codeText.text);

        clear_code = Regex.Replace(clear_code, @"\s", "");

        error_code = false;
        Executor(clear_code);
    }

    string RemoveComments(string input)
    {
        int depth = 0;
        string result = "";
        foreach (char c in input)
        {
            if (c == '{')
            {
                depth++;
            }
            else if (c == '}')
            {
                if (depth == 0) error_code = true;
                else depth--;
            }
            else
            {
                if (depth == 0) result += c;
            }
        }
        if (depth > 0) error_code = true;
        return result;
    }

    void Executor(string code)
    {
        tapes = new int[30000];
        int sym = 0;
        char output;
        int repeat_plus = 0;
        int repeat_minus = 0;
        int i = 0;
        int steps = 0;
        while (i < code.Length && !error_code)
        {
            steps++;
            if (steps >= 10000)
            {
                error_code = true;
            }
            char c = code[i];
            switch (c)
            {
                case '+':
                    tapes[sym] += 1;
                    break;
                case '-':
                    tapes[sym] -= 1;
                    if (tapes[sym] == -1) tapes[sym] += 256;
                    break;
                case 'm':
                    tapes[sym] *= tapes[sym + 1];
                    break;
                case 'd':
                    tapes[sym + 1] = tapes[sym] % 10;
                    break;
                case '>':
                    sym++;
                    break;
                case '<':
                    sym--;
                    break;
                case '.':
                    output = (char)tapes[sym];
                    output_string += output;
                    break;
                case ':':
                    repeat_plus = 0;
                    if (i + 3 < code.Length && code[i + 3] >= '0' && code[i + 3] <= '9')
                    {
                        repeat_plus += (code[i + 1] - '0') * 100;
                        repeat_plus += (code[i + 2] - '0') * 10;
                        repeat_plus += code[i + 3] - '0';
                        i += 3;
                    }
                    else if (i + 2 < code.Length && code[i + 2] >= '0' && code[i + 2] <= '9')
                    {
                        repeat_plus += (code[i + 1] - '0') * 10;
                        repeat_plus += code[i + 2] - '0';
                        i += 2;
                    }
                    else if (i + 1 < code.Length && code[i + 1] >= '0' && code[i + 1] <= '9')
                    {
                        repeat_plus += code[i + 1] - '0';
                        i += 1;
                    }
                    tapes[sym] = repeat_plus;
                    break;
                case ';':
                    repeat_minus = 0;
                    if (i + 3 < code.Length && code[i + 3] >= '0' && code[i + 3] <= '9')
                    {
                        repeat_minus += (code[i + 1] - '0') * 100;
                        repeat_minus += (code[i + 2] - '0') * 10;
                        repeat_minus += code[i + 3] - '0';
                        i += 3;
                    }
                    else if (i + 2 < code.Length && code[i + 2] >= '0' && code[i + 2] <= '9')
                    {
                        repeat_minus += (code[i + 1] - '0') * 10;
                        repeat_minus += code[i + 2] - '0';
                        i += 2;
                    }
                    else if (i + 1 < code.Length && code[i + 1] >= '0' && code[i + 1] <= '9')
                    {
                        repeat_minus += code[i + 1] - '0';
                        i += 1;
                    }
                    tapes[sym] -= repeat_minus;
                    break;
                case '#':
                    output_string += tapes[sym].ToString();
                    break;
                case 'N':
                    tapes[sym] -= '0';
                    break;
                case 'S':
                    tapes[sym] += '0';
                    break;
                case '[':
                    if (tapes[sym] == 0)
                    {
                        int depth = 1;
                        while (depth > 0 && i + 1 < code.Length - 1)
                        {
                            i++;
                            if (code[i] == '[') depth--;
                            else if (code[i] == ']') depth++;
                        }
                        if (depth > 0) error_code = true;
                    }
                    break;
                case ']':
                    if (tapes[sym] != 0)
                    {
                        int depth = 1;
                        while (depth > 0 && i > 0)
                        {
                            i--;
                            if (code[i] == '[') depth--;
                            else if (code[i] == ']') depth++;
                        }
                        if (depth > 0) error_code = true;
                    }
                    break;
                case '(':
                    if (tapes[sym] < 48 || tapes[sym] > 57)
                    {
                        int depth = 1;
                        while (depth > 0 && i < code.Length - 1)
                        {
                            i++;
                            if (code[i] == '(') depth--;
                            else if (code[i] == ')') depth++;
                        }
                    }
                    break;
                case ')':
                    break;
                default:
                    error_code = true;
                    break;
            }
            i++;
        }
        correctAnswer.CompareAnswer();
    }
}

