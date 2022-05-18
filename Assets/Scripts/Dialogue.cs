using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class Dialogue : MonoBehaviour
    {
        public int curLine;

        public string[] names;

        [TextArea(3, 10)]
        public string[] sentences;

        public Sprite[] charSprites;

        public GameObject[] turnOnObj;

        public GameObject turnOnObjAtEnd;
        public GameObject turnOffObjAtEnd;

        public Animator anim;
        public Image charImage;
        public Text nameText;
        public Text dialogueText;

        public GameObject playerCanvas;
        //    public PlayerManager playerMan;

        bool alreadyTurnedOff;

        void OnEnable()
        {
            Initialise();
        }

        public void Initialise() 
        {
            alreadyTurnedOff = false;

            //        playerMan = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
            //        playerCanv = playerMan.playerCanvas;
            //        playerCanvas.SetActive(false);

            anim.SetTrigger("On");

            curLine = 0;
            dialogueText.text = sentences[curLine];
            nameText.text = names[curLine];
            charImage.sprite = charSprites[curLine];
        }

        void LateUpdate()
        {
            //stop player from moving
            //        playerMan.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //        playerMan.gameObject.GetComponent<Animator>().SetBool("Moving", false);
        }

        public void Click()
        {
            print("Clicked");
            curLine++;
            if (curLine < sentences.Length)
            {

                dialogueText.text = sentences[curLine];

                if (charSprites[curLine] != null)
                {
                    charImage.sprite = charSprites[curLine];
                    anim.SetTrigger("ChangeSprite");
                }
                else
                {
                    anim.SetTrigger("Click");
                }

                nameText.text = names[curLine];

                if (turnOnObj[curLine] != null)
                {
                    turnOnObj[curLine].SetActive(true);
                }
            }
            else
            {
                if (turnOnObjAtEnd != null)
                {
                    turnOnObjAtEnd.SetActive(true);
                }
                if (turnOffObjAtEnd != null)
                {
                    turnOffObjAtEnd.SetActive(false);
                }

                if (!alreadyTurnedOff)
                {
                    //END DIALOGUE
                    anim.SetTrigger("Off");
                    alreadyTurnedOff = true;

                    //call Off() after 0.33 seconds to leave room for the animation to finish
                    Invoke("Off", 0.33f);
                }
            }
        }

        public void Off()
        {
            //change this to allow player movement
            //        playerCanvas.SetActive(true);
            //        playerMan.move.dashing = false;
            gameObject.SetActive(false);
        }
    }