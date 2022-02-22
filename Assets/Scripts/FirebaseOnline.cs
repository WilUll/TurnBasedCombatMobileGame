using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseOnline : MonoBehaviour
{
    
    //The thing we want to listen to, when it changes, HandleValueChanged will run.
    public void Subscribe(string id)
    {
        FirebaseDatabase.DefaultInstance.RootReference.Child("games/").Child(id).ValueChanged += HandleValueChanged;
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        // Do something with the data in args.Snapshot
        Debug.Log("Value has changed: " + args.Snapshot.GetRawJsonValue());

        //update our game info
        GameInfo updatedGame = JsonUtility.FromJson<GameInfo>(args.Snapshot.GetRawJsonValue());

        //run the game with the new information
        FindObjectOfType<BattleSystemOnline>().RefreshGame(updatedGame);
        FindObjectOfType<BattleHUDOnline>().UpdateHud();
    }

    private void OnDisable()
    {
        
    }
}
