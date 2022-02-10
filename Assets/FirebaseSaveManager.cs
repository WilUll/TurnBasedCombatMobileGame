using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;

public class FirebaseSaveManager : MonoBehaviour
{
	FirebaseAuth auth;
	void Start()
	{
		FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
		{
			if (task.Exception != null)
				Debug.LogError(task.Exception);

			auth = FirebaseAuth.DefaultInstance;
		});
	}

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			SignOut();
        }
	}



	private void SavePlayer(string userID)
	{
		PlayerSaveData saveData = new PlayerSaveData();

		PlayerInfo playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();

		saveData.Name = playerScript.Name;
		saveData.ColorHUE = playerScript.ColorHUE;
		saveData.Exp = playerScript.Exp;

		string jsonString = JsonUtility.ToJson(saveData);



		Debug.Log("Trying to write data...");
		var db = FirebaseDatabase.DefaultInstance;

		var task = db.RootReference.Child("users").Child(userID).SetValueAsync(jsonString).ContinueWith(task =>
		{
			if (task.Exception != null)
				Debug.LogWarning(task.Exception);
			else
				Debug.Log("DataTestWrite: Complete");
		});
	}

	private void SignOut()
	{
		auth.SignOut();
		Debug.Log("User signed out");
	}
}
