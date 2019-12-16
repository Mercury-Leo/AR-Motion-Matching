using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class Data_Handler : MonoBehaviour
{

	public Transform curr_body;
	string device_path;
	private int frames = 0;
	DateTime current_time;
    string[] rowDataTemp = new string[4];
    private List<string[]> rowData = new List<string[]>();

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
		if (File.Exists(device_path))
		{
			File.Delete(device_path);
		}

        rowDataTemp[0] = "Time";
        rowDataTemp[1] = "X_cords";
        rowDataTemp[2] = "Y_cords";
        rowDataTemp[3] = "Z_cords";
        rowData.Add(rowDataTemp);

		StreamWriter outStream = File.CreateText(device_path);
		outStream.Close();

	}

    private void OnEnable()
    {
        
    }

    void Write_CSV()
	{
        Transform body = curr_body;
        current_time = DateTime.Now;
		rowDataTemp = new string[4];
        rowDataTemp[0] = current_time.ToString();
        rowDataTemp[1] = body.position.x.ToString();
        rowDataTemp[2] = body.position.y.ToString();
        rowDataTemp[3] = body.position.z.ToString();
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

	public class tester
	{
		public DateTime Time { get; set; }
		public string X_cords { get; set; }
		public string Y_cords { get; set; }
		public string Z_cords { get; set; }
    }


	private string getPath()
	{
#if UNITY_EDITOR
            return Path.Combine(Application.dataPath, "DataFiles", "Saved_data.csv");
#elif UNITY_ANDROID
            return Path.Combine(Application.persistentDataPath, "DataFiles", "Saved_data.csv");
#elif UNITY_IPHONE
            return Path.Combine(Application.persistentDataPath, "DataFiles", "Saved_data.csv");
#else
		    return Path.Combine(Application.dataPath, "DataFiles", "Saved_data.csv");
#endif
    }


}
