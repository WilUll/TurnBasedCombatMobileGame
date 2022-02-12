using Firebase.Auth;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
	public static PlayerSaveData data;
	static string userPath;


	private void Start()
	{
		FindObjectOfType<FirebaseRegisterAccount>().OnSignIn += OnSignIn;
		userPath = "users/" + FirebaseAuth.DefaultInstance.CurrentUser.UserId;
	}

	void OnSignIn()
	{
		userPath = "users/" + FirebaseAuth.DefaultInstance.CurrentUser.UserId;
		SaveManager.Instance.LoadData(userPath, OnLoadData);
	}

	void OnLoadData(string json)
	{
		if (json != null)
		{
			data = JsonUtility.FromJson<PlayerSaveData>(json);
			Debug.Log(JsonUtility.ToJson(data));
		}
		else
		{
			data = new PlayerSaveData();
			Debug.Log(JsonUtility.ToJson(data));
			SaveData();
		}

		FindObjectOfType<FirebaseRegisterAccount>()?.PlayerDataLoaded();
	}

	public static void SaveData()
	{
		SaveManager.Instance.SaveData(userPath, JsonUtility.ToJson(data));
	}
}