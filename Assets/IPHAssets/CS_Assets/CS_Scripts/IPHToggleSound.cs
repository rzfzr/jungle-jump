﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteHopper {
    /// <summary>
    /// Toggles a sound source when clicked on. 
    /// In order to detect clicks you need to attach this script to a UI Button and set the proper OnClick() event.
    /// </summary>
    public class IPHToggleSound : MonoBehaviour {
        //The tag of the sound object
        public string soundObjectTag = "GameController";

        // The source of the sound
        public Transform soundObject;

        // The index of the current value of the sound
        internal float currentState; //## default value now with PlayerPrefs statement in Awake

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// Awake is used to initialize any variables or game state before the game starts. Awake is called only once during the 
        /// lifetime of the script instance. Awake is called after all objects are initialized so you can safely speak to other 
        /// objects or query them using eg. GameObject.FindWithTag. Each GameObject's Awake is called in a random order between objects. 
        /// Because of this, you should use Awake to set up references between scripts, and use Start to pass any information back and forth. 
        /// Awake is always called before any Start functions. This allows you to order initialization of scripts. Awake can not act as a coroutine.
        /// </summary>
        void Awake () {
            if (!soundObject && soundObjectTag != string.Empty) soundObject = GameObject.FindGameObjectWithTag (soundObjectTag).transform;

            currentState = PlayerPrefs.GetFloat (soundObjectTag, 1);
            // print (soundObjectTag + currentState);
            // Set the sound in the sound source
            SetSound ();
        }

        /// <summary>
        /// Sets the sound volume
        /// </summary>
        void SetSound () {
            if (!soundObject && soundObjectTag != string.Empty) soundObject = GameObject.FindGameObjectWithTag (soundObjectTag).transform;

            Color newColor = GetComponent<Image> ().material.color;

            // Update the graphics of the button image to fit the sound state
            if (currentState == 1) //sets the transparency if muted
                newColor.a = 1;
            else
                newColor.a = 0.5f;

            GetComponent<Image> ().color = newColor;

            // Set the value of the sound state to the source object
            if (soundObject)
                soundObject.GetComponent<AudioSource> ().volume = currentState;

            PlayerPrefs.SetFloat (soundObjectTag, currentState);
        }

        /// <summary>
        /// Toggle the sound. Cycle through all sound modes and set the volume and icon accordingly
        /// </summary>
        void ToggleSound () {
            currentState = 1 - currentState;

            SetSound ();
        }

        /// <summary>
        /// Starts the sound source.
        /// </summary>
        void StartSound () {
            if (soundObject)
                soundObject.GetComponent<AudioSource> ().Play ();
        }

    }
}