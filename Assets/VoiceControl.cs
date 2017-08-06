using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using KKSpeech;
using System.Collections.Generic;
using System.Text;
using System;

public class VoiceControl : MonoBehaviour {
	public Button startRecordingButton; // The button where we start the voice events.
	public Text resultText; // Where to draw a response.

	public string[] blocks; // Blocks of words the user speaks.
	public int score1, score2; // Required scores for voice command recognition.
	public string[] commands = {
		"sit",
		"stay",
		"walk",
		"good",
		"bad",
		"dead",
		"speak",
		"fetch",
	};

	public string command;

	void Start () {
		if (SpeechRecognizer.ExistsOnDevice()) {
			SpeechRecognizerListener listener = GameObject.FindObjectOfType<SpeechRecognizerListener>();

			/* Add a bunch of listeners for all events from speech recognition. */
			listener.onAuthorizationStatusFetched.AddListener(OnAuthorizationStatusFetched);
			listener.onAvailabilityChanged.AddListener(OnAvailabilityChange);
			listener.onErrorDuringRecording.AddListener(OnError);
			listener.onErrorOnStartRecording.AddListener(OnError);
			listener.onFinalResults.AddListener(OnFinalResult);
			listener.onPartialResults.AddListener(OnPartialResult);
			listener.onEndOfSpeech.AddListener(OnEndOfSpeech);
			startRecordingButton.enabled = false;

			/* As for permission to record. */
			SpeechRecognizer.RequestAccess();
		} else {
			WWW req = this.POSTRequest ("https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment",
				"This is just a test debug string, hopefully I can get some sentiment analysis going.");

			while (!req.isDone) {}

			req = this.POSTRequestGeneral ("https://woof-api.herokuapp.com/api/conversation", "This is just a test debug string, hopefully I can get some sentiment analysis going.", req.text);

			while (!req.isDone) {}

			resultText.text = "RESP: " +  req.text;

			startRecordingButton.enabled = false;
		}
	}
	
	public void OnFinalResult(string result) {
		resultText.text = result;
		score1 = 0;
		score2 = 0;

		command = "";

		/* Analyse the words the user says for the activation phrase. This bypasses NLP. */
		blocks = resultText.text.Split (' ');

		/* Only test the first ten words. */
		for (int i=0; i < ((blocks.Length > 10) ? 10 : blocks.Length); i++) {
			if (blocks [i] == "dog" || blocks [i] == "dawg" || blocks[i] == "doggy" || blocks[i] == "doggie" || blocks[i] == "Dogg") {
				score1++;
			} else if (blocks [i] == "hey" || blocks [i] == "yo"  || blocks[i] == "hi" || blocks[i] == "hello" || blocks[i] == "good" || blocks[i] == "bad") {
				score2++;
			}
		}

		/* Score must be at least 2 for it to recognise command mode. */
		if (score1 >= 1 && score2 >= 1) {
			for (int i = 0; i < (blocks.Length); i++) {
				for (int j = 0; j < (commands.Length); j++) {
					if (blocks [i] == commands [j]) {
						command = commands [j];

						break;
					}
				}
			}
		}

		/* Now parse using an NLP API. */
		// resultText.text += this.POSTRequest ("https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment", resultText.text);

		WWW req = this.POSTRequest ("https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment", resultText.text);

//		/* And also add to the body. */
//		if (command != "") {
//			//resultText.text = "COMMAND RECOGNISED: " + command + ", YOU SAID: " + resultText.text;
//		} else {
//			//resultText.text = "NO COMMAND, YOU SAID: " + resultText.text;
//		}

		while (!req.isDone) {}

		// resultText.text += req.text;

		/* Send the transcript and sentiment to the server, two requests. */
		req = this.POSTRequestGeneral ("https://woof-api.herokuapp.com/api/conversation", resultText.text, req.text);

		while (!req.isDone) {}

		resultText.text = req.text;

		/* If there's a command, execute method. */
	}

	public WWW POSTRequest(string url, string text) {
		WWW www;

		WWWForm form = new WWWForm();
		Dictionary<string,string> headers = form.headers;

		form.AddField("language", "en");
		form.AddField("id", "enNlpHack4");
		form.AddField("text", text);

		headers["Ocp-Apim-Subscription-Key"] = "dc458677da564056ab9fa0403b593fb8";
		headers["Content-Type"] = "application/json";

		string str = "{'documents': [{'language': 'en', 'id':'enlpHack5', 'text': '" + text.Replace("'", @"\'") + "'}]}";
		Debug.Log (str);

		www = new WWW(url, Encoding.ASCII.GetBytes(str), headers);
		StartCoroutine(WaitForRequest(www));

		return www;
	}

	public WWW POSTRequestGeneral(string url, string text, string text_large) {
		WWW www;

		WWWForm form = new WWWForm();
		Dictionary<string,string> headers = form.headers;

		form.AddField("transcript", text);

		headers["Authorization"] = "Basic d29vZnVzZXI6VU5JSEFDSzIwMTc=";
		headers["Content-Type"] = "application/json";

		string str = "{'transcript': '" + text.Replace("'", @"\'") + "', 'data': " + text_large.Replace("'", @"\'") + "}";
		str = str.Replace("'","\"");

		Debug.Log (str);

		www = new WWW(url, Encoding.ASCII.GetBytes(str), headers);
		StartCoroutine(WaitForRequest(www));

		return www;
	}

	IEnumerator WaitForRequest(WWW data) {
		yield return data;

		if (data.error != null) {
			Debug.Log("There was an error sending request: " + data.error);
		} else {
			Debug.Log("WWW Request: " + data.text);
		}
	}

	public void OnPartialResult(string result) {
		resultText.text = result;
	}

	public void OnAvailabilityChange(bool available) {
		startRecordingButton.enabled = available;

		if (!available) {
			resultText.text = "Speech Recognition not available";
		} else {
			resultText.text = "Say something :-)";
		}
	}

	public void OnAuthorizationStatusFetched(AuthorizationStatus status) {
		switch (status) {
		case AuthorizationStatus.Authorized:
			startRecordingButton.enabled = true;
			break;
		default:
			startRecordingButton.enabled = false;
			resultText.text = "Cannot use Speech Recognition, authorization status is " + status;
			break;
		}
	}

	public void OnEndOfSpeech() {
		startRecordingButton.GetComponentInChildren<Text>().text = "Start Recording";
	}

	public void OnError(string error) {
		Debug.LogError(error);
		resultText.text = "Something went wrong... Try again! \n [" + error + "]";
		startRecordingButton.GetComponentInChildren<Text>().text = "Start Recording";
	}

	public void OnStartRecordingPressed() {
		if (SpeechRecognizer.IsRecording()) {
			SpeechRecognizer.StopIfRecording();
			startRecordingButton.GetComponentInChildren<Text>().text = "Start Recording";
		} else {
			SpeechRecognizer.StartRecording(true);
			startRecordingButton.GetComponentInChildren<Text>().text = "Stop Recording";
			resultText.text = "Say something :-)";
		}
	}
}
