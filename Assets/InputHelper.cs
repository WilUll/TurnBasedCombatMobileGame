using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputHelper : MonoBehaviour
{
	public bool SaveInputFieldData = true;
	List<GameObject> fields;

	void Start()
	{
		fields = new List<GameObject>();

		var inputTMPs = FindObjectsOfType<TMP_InputField>();
		var inputs = FindObjectsOfType<InputField>();

		foreach (var item in inputs)
		{
			fields.Add(item.gameObject);
		}

		foreach (var item in inputTMPs)
		{
			fields.Add(item.gameObject);
		}

		if (SaveInputFieldData)
		{
			foreach (var item in fields)
			{
				TMP_InputField TMP_Input = item.GetComponent<TMP_InputField>();
				if (TMP_Input != null)
				{
					TMP_Input.text = Load(item.name);
					TMP_Input.onEndEdit.AddListener(delegate { SaveData(item.name, TMP_Input.text); });
				}
				else
				{
					InputField input = item.GetComponent<InputField>();
					input.text = Load(item.name);
					input.onEndEdit.AddListener(delegate { SaveData(item.name, input.text); });
				}
			}
		}
	}

	string Load(string name)
	{
		return PlayerPrefs.GetString(name);
	}

	void SaveData(string name, string data)
	{
		PlayerPrefs.SetString(name, data);
	}

	void Update()
	{
		//Cycle the input fields with tab
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			for (int i = 0; i < fields.Count; i++)
			{
				TMP_InputField TMP_Input = fields[i].GetComponent<TMP_InputField>();
				if (TMP_Input != null)
				{
					if (TMP_Input.isFocused)
					{
						SelectNext(i);
					}
				}
				else
				{
					InputField input = fields[i].GetComponent<InputField>();
					if (input.isFocused)
					{
						SelectNext(i);
					}
				}
			}
		}
	}

	private void SelectNext(int index)
	{
		TMP_InputField TMP_Input = fields[(index + 1) % fields.Count].GetComponent<TMP_InputField>();
		if (TMP_Input != null)
		{
			TMP_Input.Select();
		}
		else
		{
			fields[(index + 1) % fields.Count].GetComponent<InputField>().Select();
		}
	}
}