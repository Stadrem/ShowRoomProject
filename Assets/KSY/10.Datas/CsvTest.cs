using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class InfoArray
{
    public string[] go = new string[100];
}

public class CsvTest : MonoBehaviour
{
    public TextAsset text;
    public InfoArray[] array;
    public string[] colum = new string[74];
    public string[] row = new string[11];
    public string[][] input = new string[11][];
    public string[,] input2 = new string[11,74];
    //public string[] info1 = new string[74];
    //public string[] info2 = new string[74];
    //public string[] info3 = new string[74];
    //public string[] info4 = new string[74];
    //public string[] info5 = new string[74];
    //public string[] info6 = new string[74];
    //public string[] info7 = new string[74];
    //public string[] info8 = new string[74];
    //public string[] info9 = new string[74];
    //public string[] info10 = new string[74];
    //public string[] info11 = new string[74];
    public List<string[]> list = new List<string[]>();
    public List<List<string>> list2 = new List<List<string>>();
    void Start()
    {
        array = new InfoArray[11];
        
        print(text);
        row = text.text.Split('\n');
        
        for(int i = 0; i < 11; i++)
        {
            input[i] = row[i].Split(',');
            
        }
        for(int i = 0; i< 11; i++)
        {
            array[i].go = new string[75];
            for (int j = 0; j < 74; j++)
            {
                input2[i, j] = input[i][j];
                
                array[i].go[j] = input2[i,j];
            }
        }
        for(int i = 0; i< 11; i++)
        {
            list.Add(row[i].Split(','));
            list2.Add(row[i].Split(',').ToList());
        }
        //info1 = list[0];
        //info2 = list[1];
        //info3 = list[2];
        //info4 = list[3];
        //info5 = list[4];
        //info6 = list[5];
        //info7 = list[6];
        //info8 = list[7];
        //info9 = list[8];
        //info10 = list[9];
        //info11 = list[10];
    }

    void Update()
    {
        
    }
}
