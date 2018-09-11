using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSounds : MonoBehaviour {

	public static ToggleSounds toggleSounds;
	[SerializeField] private AudioClip[] audios = null;
	public AudioSource bgmSource = null, sfxSource = null, walkingSoundSource = null;
	private static bool isBGM = true, isSFX = true;

	public enum Sfx{
		victory, correct, arranged, notArranged, button, openBag, closeBag, onLight, offLight, portalBullet, walk, laser, charged, roomMoving, reflectorMoving
	}

	private void Awake(){
		toggleSounds = this;
		sfxSource = toggleSounds.GetComponent<AudioSource> ();
		isBGM = true;
		isSFX = true;
	}

	private void Start(){
		if (PlayerPrefs.GetFloat ("default") == 0) {
			PlayerPrefs.SetFloat ("default", 1);
			PlayerPrefs.SetFloat ("isBGM", 1);
			PlayerPrefs.SetFloat ("isSFX", 1);
		}
		if (PlayerPrefs.GetFloat ("isBGM") == 1) {
			isBGM = true;
		} else {
			isBGM = false;
		}
		if (PlayerPrefs.GetFloat ("isSFX") == 1) {
			isSFX = true;
		} else {
			isSFX = false;
		}

			sfxSource.volume = 0;
			walkingSoundSource.volume = 0;
		}

	

	public void PlaySfx(Sfx sound){
		if (sound == Sfx.victory){
			toggleSounds.sfxSource.PlayOneShot (toggleSounds.audios [0]);
		}
		if (sound == Sfx.correct)
		{
			toggleSounds.sfxSource.PlayOneShot (toggleSounds.audios [1]);
		}
		if (sound == Sfx.arranged)
		{
			toggleSounds.sfxSource.PlayOneShot (toggleSounds.audios [2]);
		}
		if (sound == Sfx.notArranged)
		{
			toggleSounds.sfxSource.PlayOneShot (toggleSounds.audios [3]);
		}
		if (sound == Sfx.button)
		{
			toggleSounds.sfxSource.PlayOneShot (toggleSounds.audios [4]);
		}
		if (sound == Sfx.openBag)
		{
			toggleSounds.sfxSource.PlayOneShot (toggleSounds.audios [5]);
		}
		if (sound == Sfx.closeBag)
		{
			toggleSounds.sfxSource.PlayOneShot (toggleSounds.audios [6]);
		}
		if (sound == Sfx.onLight)
		{
			toggleSounds.sfxSource.PlayOneShot (toggleSounds.audios [7]);
		}
		if (sound == Sfx.offLight)
		{
			toggleSounds.sfxSource.PlayOneShot (toggleSounds.audios [8]);
		}
		if (sound == Sfx.portalBullet)
		{
			toggleSounds.sfxSource.PlayOneShot (toggleSounds.audios [9]);
		}
		if (sound == Sfx.walk)
		{
			if(!toggleSounds.walkingSoundSource.isPlaying)
				toggleSounds.walkingSoundSource.PlayOneShot (toggleSounds.audios [10]);
		}
		if (sound == Sfx.laser)
		{
			toggleSounds.sfxSource.PlayOneShot (toggleSounds.audios [11]);
		}
		if (sound == Sfx.charged)
		{
			toggleSounds.sfxSource.PlayOneShot (toggleSounds.audios [12]);
		}
		if (sound == Sfx.roomMoving)
		{
			toggleSounds.sfxSource.PlayOneShot (toggleSounds.audios [13]);
		}
		if (sound == Sfx.reflectorMoving)
		{
			toggleSounds.sfxSource.PlayOneShot (toggleSounds.audios [14]);
		}
	}

	public void ToggleBgm(){
		isBGM = !isBGM;
		if (isBGM) {
			PlayerPrefs.SetFloat ("isBGM", 1);
			bgmSource.volume = 1;
		} else {
			PlayerPrefs.SetFloat ("isBGM", 0);
			bgmSource.volume = 0;
		}
	}

	public void ToggleSfx(){
		isSFX = !isSFX;
		if (isSFX) {
			PlayerPrefs.SetFloat ("isSFX", 1);
			sfxSource.volume = 0.75f;
			walkingSoundSource.volume = 0.75f;
		} else {
			PlayerPrefs.SetFloat ("isSFX", 0);
			sfxSource.volume = 0;
			walkingSoundSource.volume = 0;
		}
	}

	public void PlayButtonSound(){
		toggleSounds.PlaySfx (Sfx.button);
	}
}
