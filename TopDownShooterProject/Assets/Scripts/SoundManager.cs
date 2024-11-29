using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioClip[] sounds;
    private AudioSource audiosource;

    private void Start()
    {
        audiosource = GameObject.Find("Player").GetComponent<AudioSource>();
    }

    public void PlaySound(string audioName)
    {
        switch (audioName)
        {
            case "Rifle":
                audiosource.PlayOneShot(sounds[0]);
                break;
            case "Shotgun":
                audiosource.PlayOneShot(sounds[1]);
                break;
            case "Missile Launcher":
                audiosource.PlayOneShot(sounds[2]);
                break;
            case "Explosion":
                audiosource.PlayOneShot(sounds[3]);
                break;
            case "Minigun":
                audiosource.PlayOneShot(sounds[4]);
                break;
            case "Sawblade":
                audiosource.PlayOneShot(sounds[5]);
                break;
            case "Ricochet":
                audiosource.PlayOneShot(sounds[6]);
                break;
            case "Frost Beam":
                audiosource.PlayOneShot(sounds[7]);
                break;
            case "Fire Orb":
                audiosource.PlayOneShot(sounds[8]);
                break;
            case "Punch":
                audiosource.PlayOneShot(sounds[9]);
                break;
            case "Lightning":
                audiosource.PlayOneShot(sounds[10]);
                break;
            case "Grappling Hook":
                audiosource.PlayOneShot(sounds[11]);
                break;
            case "Mind Blast":
                audiosource.PlayOneShot(sounds[12]);
                break;
            case "Crossbow":
                audiosource.PlayOneShot(sounds[13]);
                break;
            case "HitShot":
                audiosource.PlayOneShot(sounds[14]);    
                break;
            case "Ice Blast":
                audiosource.PlayOneShot(sounds[15]);
                break;
            case "Ice Shatter":
                audiosource.PlayOneShot(sounds[16]);
                break;
            case "Teleport":
                audiosource.PlayOneShot(sounds[17]);
                break;
            case "ZergHit":
                audiosource.PlayOneShot(sounds[18]);
                break;
            case "Death":
                audiosource.PlayOneShot(sounds[19]);
                break;
            case "Flamethrower":
                audiosource.PlayOneShot(sounds[20]);
                break;
                
        }
    }
}
