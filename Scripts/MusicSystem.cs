using System.Collections;
using UnityEngine;

public class MusicSystem
{
    public AudioClip songClipsA;
    bool shouldGameMusicBePlaying;


    public Start()
	{
        shouldGameMusicBePlaying = true;
        StartCoroutine(PlayGameMusic());
	}

    IEnumerator PlayGameMusic()
    {
        // let's loop 4 eva
        while (shouldGameMusicBePlaying)
        {
            AudioClip clipToPlay = null;
            int currentIndex = GetSongIndexFromJumpCount();

            // check which section we should play. 0 = A, 1 = B
            if (currentSongSectionIndex == 0)
            {
                // get the clip A at index (and therefore BPM) relative to current player jumps
                clipToPlay = songClipsA[currentIndex];
                // increment song section index so it equals 1 and we play a Clip B next
                currentSongSectionIndex++;
            }
            else
            {
                // get the clip B at index (and therefore BPM) relative to current player jump count
                clipToPlay = songClipsB[currentIndex];
                // set song section index back to 0 so next time we play a Clip A
                currentSongSectionIndex = 0;
            }

            // start a coroutine that plays the next clip, repeat this loop when it's done playing
            yield return StartCoroutine(PlayClip(clipToPlay));
        }
    }

    int GetSongIndexFromJumpCount()
    {
        int currentJumpCount = 0;

        // get the player's current jump count  
        currentJumpCount = PlayState.Instance.GetJumpCount();

        // get an index based on the current jump count divided by 4
        int index = currentJumpCount / jumpDivision;

        // if the result is higher than the length of our audio clip arrays, just get the last element
        if (index >= songClipsA.Length)
        {
            index = songClipsA.Length - 1;
        }

        // return the clip index
        return index;
    }      

    private IEnumerator PlayClip(AudioClip clipToPlay)
    {
        // stop the audiosource if it was already playing
        audioSource.Stop();
        // set the next clip to play
        audioSource.clip = clipToPlay;
        // playyyyy!
        audioSource.Play();

        // while the clip is playing we yield	
        while (audioSource.isPlaying)
        {
            yield return null;
        }
    }
}
