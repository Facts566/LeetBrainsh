using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Translator : MonoBehaviour
{

    public Dropdown lang_list;
    public Text work_text, ready_text;
    public int level = 1;
    string[] level_ru =
    {
       "Напиши простую программу для вывода строки \"Hello\"",
       "Сложи числа 25 и 17. Выведи результат на экран, используя команду # и цикл []",
       "Выведи \"!!!\" с помощью цикла []",
       "Преобразуй число 65 в символы(например 1 -> \"1\") и выведи с помощью команды S",
       "Выведи \"AB\" используя только 1 ячейку"
    };
    string[] level_en =
    {
       "Write a simple program that prints the string \"Hello\".",
       "Add the numbers 25 and 17. Display the result on the screen using the # command and the loop []",
       "Output \"!!!\" using a loop []",
       "Convert the number 65 into a character and output it using the S command",
       "Output \"AB\" using only 1 cell"
    };
    string[] level_sp =
    {
       "Escribe un programa sencillo que imprima la cadena \"Hello\".",
       "Suma los números 25 y 17. Muestra el resultado en la pantalla usando el comando # y el bucle []",
       "Imprimir \"!!!\" usando el bucle []",
       "Convierte el número 65 en un símbolo e imprímelo usando el comando S",
       "Imprime \"AB\" usando solo 1 celda"
    };
    string[] level_fn =
    {
       "Écris un programme simple pour afficher la chaîne \"Hello\".",
       "Additionnez les nombres 25 et 17. Affichez le résultat à l'écran à l'aide de la commande # et la boucle []",
       "Affiche \"!!!\" à l'aide d'une boucle []",
       "Convertis le nombre 65 en caractère et affiche-le à l'aide de la commande S",
       "Affiche \"AB\" en utilisant une seule cellule"
    };
    string[] level_gm =
    {
       "Schreiben Sie ein einfaches Programm, um die Kette \"Hello\" anzuhängen.",
       "Addiere die Zahlen 25 und 17. Gib das Ergebnis auf dem Bildschirm aus, indem du den Befehl # und die Schleife [] verwendest",
       "Gib \"!!!\" mithilfe einer Schleife aus []",
       "Wandle die Zahl 65 in ein Zeichen um und gib sie mithilfe des Befehls S aus",
       "Gib \"AB\" aus, wobei du nur eine einzige Zelle verwendest"
    };
    void Start()
    {
        level = PlayerPrefs.GetInt("button_number", 1);
    }

    void Update()
    {
        switch (lang_list.value)
        {
            case 0:
                work_text.text = level_ru[level - 1];
                ready_text.text = "Готово";
                break;
            case 1:
                work_text.text = level_en[level - 1];
                ready_text.text = "Ready";
                break;
            case 2:
                work_text.text = level_sp[level - 1];
                ready_text.text = "Listo";
                break;
            case 3:
                work_text.text = level_fn[level - 1];
                ready_text.text = "C'est prêt";
                break;
            case 4:
                work_text.text = level_gm[level - 1];
                ready_text.text = "Fertig";
                break;
        }
    }
}
