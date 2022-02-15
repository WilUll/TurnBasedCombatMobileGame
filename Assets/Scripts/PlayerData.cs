using Firebase.Auth;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
	public static PlayerSaveData data;
	public static string userPath;


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
		}
		else
		{
			data = new PlayerSaveData();
			data.Level = 1;
			SaveData();
		}

		FindObjectOfType<FirebaseRegisterAccount>()?.PlayerDataLoaded();
	}

	public static void SaveData()
	{
		SaveManager.Instance.SaveData(userPath, JsonUtility.ToJson(data));
	}

	public static void UpdateSaveData(PlayerSaveData updatedData)
    {
		SaveManager.Instance.SaveData(userPath, JsonUtility.ToJson(updatedData));
	}
}