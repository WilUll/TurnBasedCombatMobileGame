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
		if (Input.GetKeyDown(KeyCode.A))
			AnonymousSignIn();

		if (Input.GetKeyDown(KeyCode.D))
			SavePlayer(auth.CurrentUser.UserId);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			SignOut();
        }
	}

	private void AnonymousSignIn()
	{
		auth.SignInAnonymouslyAsync().ContinueWith(task => {
			if (task.Exception != null)
			{
				Debug.LogWarning(task.Exception);
			}
			else
			{
				FirebaseUser newUser = task.Result;
				Debug.LogFormat("User signed in successfully: {0} ({1})",
					newUser.DisplayName, newUser.UserId);
			}
		});
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
	public void RegisterNewUser(string email, string password)
	{
		Debug.Log("Starting Registration");
		auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
		{
			if (task.Exception != null)
			{
				Debug.LogWarning(task.Exception);
			}
			else
			{
				FirebaseUser newUser = task.Result;
				Debug.LogFormat("User Registerd: {0} ({1})",
				  newUser.DisplayName, newUser.UserId);
			}
		});
	}

	public void SignIn(string email, string password)
	{
		auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
		{
			if (task.Exception != null)
			{
				Debug.LogWarning(task.Exception);
			}
			else
			{
				FirebaseUser newUser = task.Result;
				Debug.LogFormat("User signed in successfully: {0} ({1})",
				  newUser.DisplayName, newUser.UserId);
			}
		});
	}

	private void SignOut()
	{
		auth.SignOut();
		Debug.Log("User signed out");
	}
}
