using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
using TMPro;

public class FirebaseRegisterAccount : MonoBehaviour
{
    FirebaseAuth auth;
    string emailRegister;
    string passwordRegister;

    public TMP_InputField emailField;
    public TMP_InputField passwordField;
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogError(task.Exception);

            auth = FirebaseAuth.DefaultInstance;
        });
    }
    public void EnterNewUser()
    {
        emailRegister = emailField.text;
        passwordRegister = passwordField.text;

        RegisterNewUser(emailRegister, passwordRegister);
    }

    public void SingInUser()
    {
        emailRegister = emailField.text;
        passwordRegister = passwordField.text;

        SignIn(emailRegister, passwordRegister);
    }
    private void RegisterNewUser(string email, string password)
    {
        Debug.Log("Starting Registration");
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
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
}
