using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseRegisterAccount : MonoBehaviour
{
	//Shows the user whats happening
	public TextMeshProUGUI outputText;

	//The button to enter the lobby
	public Button playButton;
	public Button signInButton;
	public Button registerButton;

	//Login fields
	public TMP_InputField username;
	public TMP_InputField password;

	//Delegate
	public delegate void SignInHandler();
	public SignInHandler OnSignIn;

	FirebaseAuth auth;

	private void Start()
	{
		//Runs in the first scene of the game
		FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
		{
			if (task.Exception != null)
			{
				Debug.LogError(task.Exception);
			}

			auth = FirebaseAuth.DefaultInstance;
		});

		//Disable button untill we have logged in
		playButton.interactable = false;

		signInButton.onClick.AddListener(() => SignIn(username.text, password.text));
		registerButton.onClick.AddListener(() => RegisterNewUser(username.text, password.text));
	}

	private void RegisterNewUser(string email, string password)
	{
		outputText.text = "Attempting to register new user";

		auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
		{
			if (task.Exception != null)
			{
				Debug.LogWarning(task.Exception);
				outputText.text = task.Exception.InnerExceptions[0].InnerException.Message;
			}
			else
			{
				FirebaseUser newUser = task.Result;
				SignedIn(newUser);
			}
		});
	}

	private void SignIn(string email, string password)
	{
		outputText.text = "Attempting to log in";

		auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
		{
			if (task.Exception != null)
			{
				Debug.LogWarning(task.Exception);
				outputText.text = task.Exception.InnerExceptions[0].InnerException.Message;
			}
			else
			{
				SignedIn(task.Result);
			}
		});
	}

	private void AnonymousSignIn()
	{
		auth.SignInAnonymouslyAsync().ContinueWithOnMainThread(task => {
			if (task.Exception != null)
			{
				Debug.LogWarning(task.Exception);
				outputText.text = task.Exception.InnerExceptions[0].InnerException.Message;
			}
			else
			{
				SignedIn(task.Result);
			}
		});
	}

	private void SignedIn(FirebaseUser newUser)
	{
		Debug.LogFormat("User signed in successfully: {0} ({1})",
					newUser.DisplayName, newUser.UserId);

		//Display who logged in
		if (newUser.DisplayName != "")
			outputText.text = "Logged in as: " + newUser.DisplayName;
		else if (newUser.Email != "")
			outputText.text = "Logged in as: " + newUser.Email;
		else
			outputText.text = "Logged in as: Anonymous User " + newUser.UserId.Substring(0, 6);

		OnSignIn?.Invoke();
	}

	public void PlayerDataLoaded()
	{
		//Activate the play button once we have logged in
		playButton.interactable = true;
	}
}