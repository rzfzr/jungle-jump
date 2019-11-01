using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteHopper
{
    /// <summary>
    /// This script defines the player, its movement, as well as its death
    /// </summary>
    public class IPHPlayer : MonoBehaviour
    {
        internal List<float> jumpHistory = new List<float>();
        internal Transform thisTransform;
        internal GameObject gameController;

        //The current jump power of the player
        internal float jumpPower = 2;

        //Should the player automatically jump when reaching the maximum power?
        internal bool autoJump = true;

        //A power bar that shows how high the player will jump.
        internal Transform powerBar;

        //The particle effects that will play when the player jumps and lands on a column
        internal ParticleSystem jumpEffect;
        internal ParticleSystem landEffect;
        internal ParticleSystem perfectEffect;

        //The main sprite renderer to change the visual of player
        public BoxCollider2D boxCcollider;

        public string soundSourceTag = "GameController";
        public string powerbarTag = "PowerBar";
        internal GameObject soundSource;

        //Did the player start the jump process ( powering up and then releasing )
        internal bool startJump = false;

        //Is the player jumping now ( is in mid-air )
        internal bool isJumping = false;

        //Has the player landed on a column?
        internal bool isLanded = false;

        // Is the player falling now?
        internal bool isFalling = false;

        // Is the player dead?
        internal bool isDead = false;

        /// <summary>
        /// Store the player config
        /// </summary>
        public IPHPlayerScriptable PlayerPresets { get; set; }

        void Start()
        {
            thisTransform = transform;

            //Assign the game controller for easier access
            gameController = GameObject.FindGameObjectWithTag("GameController");

            //Create the effects
            jumpEffect = (Instantiate(PlayerPresets.JumpEffect.gameObject) as GameObject).GetComponent<ParticleSystem>();
            landEffect = (Instantiate(PlayerPresets.LandEffect.gameObject) as GameObject).GetComponent<ParticleSystem>();
            perfectEffect = (Instantiate(PlayerPresets.PerfectEffect.gameObject) as GameObject).GetComponent<ParticleSystem>();

            //Set the parent to the effects
            jumpEffect.transform.SetParent(transform, false);
            landEffect.transform.SetParent(transform, false);
            perfectEffect.transform.SetParent(transform, false);

            //Set the particle effects to the "RenderInFront" sorting layer so that the effects appear in front of the player object
            if (jumpEffect) jumpEffect.GetComponent<Renderer>().sortingLayerName = "RenderInFront";
            if (landEffect) landEffect.GetComponent<Renderer>().sortingLayerName = "RenderInFront";
            if (perfectEffect) perfectEffect.GetComponent<Renderer>().sortingLayerName = "RenderInFront";

            //Change the player visual
            SetupAvatar();

            //Assign the sound source for easier access
            if (GameObject.FindGameObjectWithTag(soundSourceTag)) soundSource = GameObject.FindGameObjectWithTag(soundSourceTag);

            //Assign the powerbar
            if (GameObject.FindGameObjectWithTag(powerbarTag)) powerBar = GameObject.FindGameObjectWithTag(powerbarTag).transform;            
        }

        void Update()
        {
            if (isDead == false)
            {
                //If we are starting to jump, charge up the jump power as long as we are holding the jump button down
                if (startJump == true)
                {
                    //Charge up the jump power
                    if (jumpPower < PlayerPresets.JumpChargeMax)
                    {
                        //Add to the jump power based on charge speed
                        jumpPower += Time.deltaTime * PlayerPresets.JumpChargeSpeed;

                        //Update the power bar fill amount
                        powerBar.Find("Base/FillAmount").GetComponent<Image>().fillAmount = jumpPower / PlayerPresets.JumpChargeMax;

                        //Play the charge sound and change the pitch based on the jump power
                        if (soundSource) soundSource.GetComponent<AudioSource>().pitch = 0.3f + jumpPower * 0.1f;
                    }
                    else if (autoJump == true)
                    {
                        //If the jump power exceeds the maximum allowed jump power, end charging, and launch the player
                        EndJump();
                    }
                    else
                    {
                        //Play the full power animation
                        if (GetComponent<Animation>() && PlayerPresets.AnimationFullPower) GetComponent<Animation>().Play(PlayerPresets.AnimationFullPower.name);
                    }
                }

                //If the player is moving down, then he is falling
                if (isFalling == false && GetComponent<Rigidbody2D>().velocity.y < 0)
                {
                    isFalling = true;

                    //Play the falling animation
                    if (GetComponent<Animation>() && PlayerPresets.AnimationFalling)
                    {
                        //Play the animation
                        GetComponent<Animation>().PlayQueued(PlayerPresets.AnimationFalling.name, QueueMode.CompleteOthers);
                    }
                }
            }
        }

        //This function adds score to the gamecontroller
        void ChangeScore(Transform landedObject)
        {
            gameController.SendMessage("ChangeScore", landedObject);
        }

        //This function destroys the player, and triggers the game over event
        void Die()
        {
            if (isDead == false)
            {
                //Call the game over function from the game controller
                gameController.SendMessage("GameOver", 0.5f);

                //Play the death sound
                if (soundSource)
                {
                    soundSource.GetComponent<AudioSource>().pitch = 1;

                    //If there is a sound source and a sound assigned, play it from the source
                    if (PlayerPresets.SoundCrash) soundSource.GetComponent<AudioSource>().PlayOneShot(PlayerPresets.SoundCrash);
                }

                // The player is dead
                isDead = true;
            }
        }

        // This function resets the player's dead status
        public void NotDead()
        {
            isDead = false;
        }

        //This function starts the jumping process, allowing the player to charge up the jump power as long as he is holding the jump button down
        void StartJump(bool playerAutoJump)
        {
            if (isDead == false)
            {
                //Set the player auto jump state based on the GameController playerAutoJump value
                autoJump = playerAutoJump;

                //You can only jump if you are on land
                if (isLanded == true)
                {
                    startJump = true;

                    //Reset the jump power
                    jumpPower = 0;

                    //Play the jump start animation ( charging up the jump power )
                    if (GetComponent<Animation>() && PlayerPresets.AnimationJumpStart)
                    {
                        //Stop the animation
                        GetComponent<Animation>().Stop();

                        //Play the animation
                        GetComponent<Animation>().Play(PlayerPresets.AnimationJumpStart.name);
                    }

                    //Align the power bar to the player and activate it
                    if (powerBar)
                    {
                        powerBar.position = thisTransform.position;

                        powerBar.gameObject.SetActive(true);
                    }

                    if (soundSource)
                    {
                        //If there is a sound source and a sound assigned, play it from the source
                        if (PlayerPresets.SoundStartJump) soundSource.GetComponent<AudioSource>().PlayOneShot(PlayerPresets.SoundStartJump);
                    }
                }
            }
        }

        //This function ends the jump process, and launches the player with the jump power we charged
        void EndJump()
        {
            if (isDead == false)
            {
                //You can only jump if you are on land, and you already charged up the jump power ( jump start )
                if (isLanded == true && startJump == true)
                {
                    thisTransform.parent = null;

                    startJump = false;
                    isJumping = true;
                    isLanded = false;
                    isFalling = false;                    

                    //Give the player velocity based on jump power and move speed
                    GetComponent<Rigidbody2D>().velocity = new Vector2(PlayerPresets.MoveSpeed, jumpPower);

                    //Play the jump ( launch ) animation
                    if (GetComponent<Animation>() && PlayerPresets.AnimationJumpEnd)
                    {
                        //Stop the animation
                        GetComponent<Animation>().Stop();

                        //Play the animation
                        GetComponent<Animation>().Play(PlayerPresets.AnimationJumpEnd.name);
                    }

                    //Deactivate the power bar
                    if (powerBar) powerBar.gameObject.SetActive(false);

                    //Play the jump particle effect
                    if (jumpEffect) jumpEffect.Play();

                    //Play the jump sound ( launch )
                    if (soundSource)
                    {
                        soundSource.GetComponent<AudioSource>().Stop();

                        soundSource.GetComponent<AudioSource>().pitch = 0.6f + jumpPower * 0.05f;

                        //If there is a sound source and a sound assigned, play it from the source
                        if (PlayerPresets.SoundEndJump) soundSource.GetComponent<AudioSource>().PlayOneShot(PlayerPresets.SoundEndJump);
                    }

                }
            }
        }

        //This function runs when the player succesfully lands on a column
        void PlayerLanded()
        {
            isLanded = true;

            //Play the landing animation
            if (GetComponent<Animation>() && PlayerPresets.AnimationLanded)
            {
                //Stop the animation
                GetComponent<Animation>().Stop();

                //Play the animation
                GetComponent<Animation>().Play(PlayerPresets.AnimationLanded.name);
            }

            //Play the landing particle effect
            if (landEffect) landEffect.Play();

            //Play the landing sound
            if (soundSource)
            {
                soundSource.GetComponent<AudioSource>().pitch = 1;

                //If there is a sound source and a sound assigned, play it from the source
                if (PlayerPresets.SoundLand) soundSource.GetComponent<AudioSource>().PlayOneShot(PlayerPresets.SoundLand);
            }
        }

        //This function runs when the player executes a perfect landing ( closest to the middle )
        void PerfectLanding(int streak)
        {
            //Play the perfect landing particle effect
            if (perfectEffect) perfectEffect.Play();

            //If there is a sound source and a sound assigned, play it from the source
            if (soundSource && PlayerPresets.SoundPerfect) soundSource.GetComponent<AudioSource>().PlayOneShot(PlayerPresets.SoundPerfect);
        }

        //Setup the player visual
        void SetupAvatar()
        {
            //Create the visual avatar of player
            GameObject avatar = Instantiate(PlayerPresets.Avatar) as GameObject;
            avatar.name = "Part1";
            avatar.transform.SetParent(thisTransform.GetChild(0), false);

            //Setup the collider that close the visual of player
            boxCcollider.offset = PlayerPresets.ColliderOffset;
            boxCcollider.size = PlayerPresets.ColliderSize;
        }        

        //This function rescales this object over time
        IEnumerator Rescale(float targetScale)
        {
            //Perform the scaling action for 1 second
            float scaleTime = 1;

            while (scaleTime > 0)
            {
                //Count down the scaling time
                scaleTime -= Time.deltaTime;

                //Wait for the fixed update so we can animate the scaling
                yield return new WaitForFixedUpdate();

                float tempScale = thisTransform.localScale.x;

                //Scale the object up or down until we reach the target scale
                tempScale -= (thisTransform.localScale.x - targetScale) * 5 * Time.deltaTime;

                thisTransform.localScale = Vector3.one * tempScale;
            }

            //Rescale the object to the target scale instantly, so we make sure that we got the the target
            thisTransform.localScale = Vector3.one * targetScale;
        }        
    }
}