﻿using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UnityEngine.UI.InputField))]
public class PlayerNameInputField : MonoBehaviour
{
    #region Private Constants


    // Store the PlayerPref Key to avoid typos
    const string playerNamePrefKey = "PlayerName";
    const string playerRoomNamePrefKey = "RoomName";


    #endregion

    [SerializeField]
    bool playerName;


    #region MonoBehaviour CallBacks


    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during initialization phase.
    /// </summary>
    void Start()
	{
        if (playerName)
            SetPlayerName();
        else
            SetPlayerRoomName();
	}

	private void SetPlayerRoomName()
	{
        string defaultName = string.Empty;
        InputField _inputField = this.GetComponent<InputField>();
        if (_inputField != null)
        {
            if (PlayerPrefs.HasKey(playerRoomNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerRoomNamePrefKey);
                _inputField.text = defaultName;
            }
        }
    }

	private void SetPlayerName()
	{
		string defaultName = string.Empty;
		InputField _inputField = this.GetComponent<InputField>();
		if (_inputField != null)
		{
			if (PlayerPrefs.HasKey(playerNamePrefKey))
			{
				defaultName = PlayerPrefs.GetString(playerNamePrefKey);
				_inputField.text = defaultName;
			}
		}
		PhotonNetwork.NickName = defaultName;
	}


	#endregion


	#region Public Methods


	/// <summary>
	/// Sets the name of the player, and save it in the PlayerPrefs for future sessions.
	/// </summary>
	/// <param name="value">The name of the Player</param>
	public void SetPlayerName(string value)
    {
        // #Important
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }
        PhotonNetwork.NickName = value;


        PlayerPrefs.SetString(playerNamePrefKey, value);
    }

    public void SetPlayerRoomName(string value)
    {
        // #Important
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Room Name is null or empty");
            return;
        }
        PlayerPrefs.SetString(playerRoomNamePrefKey, value);
    }

    #endregion
}