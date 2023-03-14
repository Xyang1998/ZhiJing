using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class MyReadCSV : MonoBehaviour
{

    private static char _csvSeparator = ',';
    private static bool _trimColumns = false;
    //��ȡһ����Ԫ���д���ʽ
    public static string GetCSVFormat(string str)
    {
        string tempStr = str;
        if (str.Contains(","))
        {
            if (str.Contains("\""))
            {
                tempStr = str.Replace("\"", "\"\"");
            }
            tempStr = "\"" + tempStr + "\"";
        }
        return tempStr;
    }

    //��ȡһ�е�д���ʽ
    public static string GetCSVFormatLine(List<string> strList)
    {
        string tempStr = "";
        for (int i = 0; i < strList.Count - 1; i++)
        {
            string str = strList[i];
            tempStr = tempStr + GetCSVFormat(str) + ",";
        }
        tempStr = tempStr + GetCSVFormat(strList[strList.Count - 1]) + "\r\n";
        return tempStr;
    }

    //����һ��
    public static List<string> ParseLine(string line)
    {
        StringBuilder _columnBuilder = new StringBuilder();
        List<string> Fields = new List<string>();
        bool inColum = false;//�Ƿ�����һ����Ԫ����
        bool inQuotes = false;//�Ƿ���Ҫת��
        bool isNotEnd = false;//��ȡ���δ����ת��
        _columnBuilder.Remove(0, _columnBuilder.Length);

        //����Ҳ��һ����Ԫ�أ�һ��������2����Ԫ��
        if (line == "")
        {
            Fields.Add("");
        }
        // Iterate through every character in the line  �������е�ÿ���ַ�
        for (int i = 0; i < line.Length; i++)
        {
            char character = line[i];

            //If we are not currently inside a column   ����������ڲ���һ����
            if (!inColum)
            {
                // If the current character is a double quote then the column value is contained within
                //�����ǰ�ַ���˫���ţ�����ֵ��������
                // double quotes, otherwise append the next character
                //˫���ţ�����׷����һ���ַ�
                inColum = true;
                if (character == '"')
                {
                    inQuotes = true;
                    continue;
                }
            }
            // If we are in between double quotes   ������Ǵ���˫����֮��
            if (inQuotes)
            {
                if ((i + 1) == line.Length)//����ַ��Ѿ�����������
                {
                    if (character == '"')//����ת��������Ҹ����Ѿ�����
                    {
                        inQuotes = false;
                        continue;
                    }
                    else//�쳣������ת��δ��β
                    {
                        isNotEnd = true;
                    }
                }
                else if (character == '"' && line[i + 1] == _csvSeparator)//����ת�壬�Һ����п��ܻ�������
                {
                    inQuotes = false;
                    inColum = false;
                    i++;//������һ���ַ�
                }
                else if (character == '"' && line[i + 1] == '"')//˫����ת��
                {
                    i++;//������һ���ַ�
                }
                else if (character == '"')//˫���ŵ������֣��������ʵ�����Ѿ��Ǹ�ʽ����Ϊ�˼�����ʱ������
                {
                    throw new System.Exception("��ʽ���󣬴����˫����ת��");
                }
                //�������ֱ�������������������
            }
            else if (character == _csvSeparator)
            {
                inColum = false;
            }
            // If we are no longer in the column clear the builder and add the columns to the list
            if (!inColum)
            {
                Fields.Add(_trimColumns ? _columnBuilder.ToString().Trim() : _columnBuilder.ToString());
                _columnBuilder.Remove(0, _columnBuilder.Length);
            }
            else//׷�ӵ�ǰ��
            {
                _columnBuilder.Append(character);
            }
        }

        // If we are still inside a column add a new one ����׼��ʽһ�н�β����Ҫ���Ž�β��������for���������Ų���ӵģ�Ϊ�˼������Ҫ���һ�Σ�
        if (inColum)
        {
            if (isNotEnd)
            {
                _columnBuilder.Append("\r\n");
            }
            Fields.Add(_trimColumns ? _columnBuilder.ToString().Trim() : _columnBuilder.ToString());
        }
        else  //���inColumnΪfalse��˵���Ѿ���ӣ���Ϊ���һ���ַ�Ϊ�ָ��������Ժ���Ҫ����һ����Ԫ��
        {
            Fields.Add("");
        }
        return Fields;
    }

    //��ȡ�ļ�
    public static List<List<string>> Read(string filePath, Encoding encoding)
    {
        List<List<string>> result = new List<List<string>>();
        string content = File.ReadAllText(filePath, encoding);//��ȡcsv���е��ı�����
        string[] lines = content.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        //�Ի��лس�����ַ�����ȥ���ո�
        //ע���س����п��ܶ�ĳЩcsv�����ã�����������ǳ��ֶ�ȡ�����������Ը��� \n �����У�����

        for (int i = 0; i < lines.Length; i++)
        {
            List<string> line = ParseLine(lines[i]);
            result.Add(line);
        }
        return result;
    }

    // Use this for initialization
    void Start()
    {
        List<List<string>> lists;
        //lists = Read(Application.streamingAssetsPath + "/test.csv", Encoding.Default);//������ַ
        lists = Read(Application.dataPath + "/dialog.csv", Encoding.Default);//���ǰ���Ե�ַ
        //      ��       ��
        Debug.Log(lists[0][0]);
        //Debug.Log(lists[0][1]);
        Debug.Log(lists[1][0]);
        Debug.Log(lists.Count);
    }

    // Update is called once per frame
    void Update()
    {

    }

}