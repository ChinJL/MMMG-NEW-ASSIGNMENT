using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

	public static AudioManager audioManager;
	[SerializeField] private AudioClip[] audios = null;
	public AudioSource bgmSource = null, sfxSource = null, walkingSoundSource = null;
	private static bool isBGM = true, isSFX = true;
	[SerializeField] private Image bgmImg = null, sfxImg = null;
	[SerializeField] private Sprite musicOn = null, musicOff = null, soundOn = null, soundOff = null;

	public enum Sfx{
		victory, correct, arranged, notArranged, button, openBag, closeBag, onLight, offLight, portalBullet, walk, laser, charged, roomMoving, reflectorMoving
	}

	private void Awake(){
		audioManager = this;
		sfxSource = audioManager.GetComponent<AudioSource> ();
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
		if (isBGM) {
			if (bgmImg != null) {
				bgmImg.sprite = musicOn;
			}
			bgmSource.volume = 1;
		} else {
			if (bgmImg != null) {
				bgmImg.sprite = musicOff;
			}
			bgmSource.volume = 0;
		}
		if (isSFX) {
			if (sfxImg != null) {
				sfxImg.sprite = soundOn;
			}
			sfxSource.volume = 0.75f;
			walkingSoundSource.volume = 0.75f;
		} else {
			if (sfxImg != null) {
				sfxImg.sprite = soundOff;
			}
			sfxSource.volume = 0;
			walkingSoundSource.volume = 0;
		}
	}

	public void PlaySfx(Sfx sound){
		if (sound == Sfx.victory){
			audioManager.sfxSource.PlayOneShot (audioManager.audios [0]);
		}
		if (sound == Sfx.correct)
		{
			audioManager.sfxSource.PlayOneShot (audioManager.audios [1]);
		}
		if (sound == Sfx.arranged)
		{
			audioManager.sfxSource.PlayOneShot (audioManager.audios [2]);
		}
		if (sound == Sfx.notArranged)
		{
			audioManager.sfxSource.PlayOneShot (audioManager.audios [3]);
		}
		if (sound == Sfx.button)
		{
			audioManager.sfxSource.PlayOneShot (audioManager.audios [4]);
		}
		if (sound == Sfx.openBag)
		{
			audioManager.sfxSource.PlayOneShot (audioManager.audios [5]);
		}
		if (sound == Sfx.closeBag)
		{
			audioManager.sfxSource.PlayOneShot (audioManager.audios [6]);
		}
		if (sound == Sfx.onLight)
		{
			audioManager.sfxSource.PlayOneShot (audioManager.audios [7]);
		}
		if (sound == Sfx.offLight)
		{
			audioManager.sfxSource.PlayOneShot (audioManager.audios [8]);
		}
		if (sound == Sfx.portalBullet)
		{
			audioManager.sfxSource.PlayOneShot (audioManager.audios [9]);
		}
		if (sound == Sfx.walk)
		{
			if(!audioManager.walkingSoundSource.isPlaying)
				audioManager.walkingSoundSource.PlayOneShot (audioManager.audios [10]);
		}
		if (sound == Sfx.laser)
		{
			audioManager.sfxSource.PlayOneShot (audioManager.audios [11]);
		}
		if (sound == Sfx.charged)
		{
			audioManager.sfxSource.PlayOneShot (audioManager.audios [12]);
		}
		if (sound == Sfx.roomMoving)
		{
			audioManager.sfxSource.PlayOneShot (audioManager.audios [13]);
		}
		if (sound == Sfx.reflectorMoving)
		{
			audioManager.sfxSource.PlayOneShot (audioManager.audios [14]);
		}
	}

	public void ToggleBgm(){
		isBGM = !isBGM;
		if (isBGM) {
			PlayerPrefs.SetFloat ("isBGM", 1);
			bgmImg.sprite = musicOn;
			bgmSource.volume = 1;
		} else {
			PlayerPrefs.SetFloat ("isBGM", 0);
			bgmImg.sprite = musicOff;
			bgmSource.volume = 0;
		}
	}

	public void ToggleSfx(){
		isSFX = !isSFX;
		if (isSFX) {
			PlayerPrefs.SetFloat ("isSFX", 1);
			sfxImg.sprite = soundOn;
			sfxSource.volume = 0.75f;
			walkingSoundSource.volume = 0.75f;
		} else {
			PlayerPrefs.SetFloat ("isSFX", 0);
			sfxImg.sprite = soundOff;
			sfxSource.volume = 0;
			walkingSoundSource.volume = 0;
		}
	}

	public void PlayButtonSound(){
		audioManager.PlaySfx (Sfx.button);
	}
}
