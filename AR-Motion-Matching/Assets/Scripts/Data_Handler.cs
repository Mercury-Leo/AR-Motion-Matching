using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
using System.Runtime.Serialization;

public class Data_Handler : MonoBehaviour
{

	public Transform curr_body;
	string device_path;
	private int frames = 0;
	DateTime current_time;
    string[] rowDataTemp = new string[4];
    private List<string[]> rowData = new List<string[]>();
    private Body_Position body_Position;

	// Start is called before the first frame update
	void Start()
	{
		device_path = getPath();

		Debug.Log("datapath: " + device_path);

		Create_CSV();
	}

	// Update is called once per frame
	void Update()
	{
		frames++;
		if (frames % 30 == 0)
		{
            Write_CSV();
           frames = 0;
        }

        //Invoke("Write_CSV", 0.7f);
		
	}

	void Create_CSV()
	{
        rowDataTemp[0] = "Time";
        rowDataTemp[1] = "X_cords";
        rowDataTemp[2] = "Y_cords";
        rowDataTemp[3] = "Z_cords";
        rowData.Add(rowDataTemp);

		StreamWriter outStream = File.CreateText(device_path);
		outStream.Close();
        if(!File.Exists(device_path))
        {   
            Debug.LogError("Didn't create +" + device_path);
        }

    }

    void Write_CSV()
	{
        Transform body = curr_body;

        current_time = DateTime.Now;

		rowDataTemp = new string[4];

        Vector3 a = curr_body.position;
        Vector3 b = curr_body.position;

        body_Position = Body_Position.CreateComponent(gameObject, current_time, a, b, a, b);

        rowDataTemp[0] = body_Position.Time.ToString();
        rowDataTemp[1] = body_Position.head.Head_X_cords.ToString();
        rowDataTemp[2] = body_Position.head.Head_Y_cords.ToString();
        rowDataTemp[3] = body_Position.head.Head_Z_cords.ToString();
        rowData.Add(rowDataTemp);

		string[][] output = new string[rowData.Count][];

		for (int i = 0; i < output.Length; i++)
		{
			output[i] = rowData[i];
		}

		int length = output.GetLength(0);
		string delimiter = ",";

		StringBuilder sb = new StringBuilder();

		for (int index = 0; index < length; index++)
		{
			sb.AppendLine(string.Join(delimiter, output[index]));
		}

		StreamWriter outStream = new StreamWriter(device_path, true);
		outStream.WriteLine(sb);
		outStream.Close();
        rowData.Clear();
	}

    

    private string getPath()
	{
        string body_location = "Saved_data" + DateTime.Now.ToString("dd_MM_yy_HH_mm_ss") + ".csv";
#if UNITY_EDITOR
        return Path.Combine(Application.dataPath, "DataFiles", body_location);
#elif UNITY_ANDROID
            return Path.Combine(Application.persistentDataPath, "DataFiles", body_location);
#elif UNITY_IPHONE
            return Path.Combine(Application.persistentDataPath, "DataFiles", body_location);
#else
		    return Path.Combine(Application.dataPath, "DataFiles", body_location);
#endif
    }

}
