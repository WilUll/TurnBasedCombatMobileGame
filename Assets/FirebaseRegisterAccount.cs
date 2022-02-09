//using Firebase;
//using Firebase.Auth;
//using Firebase.Database;
//using Firebase.Extensions;
//using UnityEngine;

//public class FirebaseRegisterAccount : MonoBehaviour
//{
//    FirebaseAuth auth;
//    string emailRegister;
//    string passwordRegister;
//    void Start()
//    {
//        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
//        {
//            if (task.Exception != null)
//                Debug.LogError(task.Exception);

//            auth = FirebaseAuth.DefaultInstance;
//        });
//    }
//    public void EnterNewUser()
//    {
//        emailRegister = emailFieldRegister.text;
//        passwordRegister = passwordFieldRegister.text;

//        RegisterNewUser(emailRegister, passwordRegister);
//    }
//    private void RegisterNewUser(string email, string password)
//    {
//        Debug.Log("Starting Registration");
//        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
//        {
//            if (task.Exception != null)
//            {
//                Debug.LogWarning(task.Exception);
//            }
//            else
//            {
//                FirebaseUser newUser = task.Result;
//                Debug.LogFormat("User Registerd: {0} ({1})",
//                  newUser.DisplayName, newUser.UserId);
//            }
//        });
//    }
//}
