using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace Assets.DemoProjects.GameDevHQ.Projects.DungeonEscapeC3Mobile.Scripts
{
	public class AdsManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
	{

		// IUnityAdsListener
		[SerializeField] private Button _showAdButton;
		[SerializeField] private string _androidAdUnitId = "Rewarded_Android";
		[SerializeField] private string _iOsAdUnitId = "Rewarded_iOS";
		private GameManager gm;
		private UIManager uiM;
		private string _adUnitId;
		private ShowResult result;

		private void Awake()
		{
			// Get the Ad Unit ID for the current platform:
			_adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
					? _iOsAdUnitId
					: _androidAdUnitId;

			//Disable button until ad is ready to show
			_showAdButton.interactable = false;
		}

		private void Start()
		{
			gm = GameManager.Instance;
			uiM = UIManager.Instance;
		}

		// Load content to the Ad Unit:
		public void LoadAd()
		{
			// IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
			Debug.Log("Loading Ad: " + _adUnitId);
			Advertisement.Load(_adUnitId, this);
		}

		// Implement a method to execute when the user clicks the button.
		public void ShowAd()
		{
			// Disable the button: 
			_showAdButton.interactable = false;
			// Then show the ad:
			Advertisement.Show(_adUnitId, this);

			var player = GameObject.FindWithTag("Player").GetComponent<DungeonPlayer.Player>();
			if (player == null)
				return;
			Debug.Log("player reward granted");
			player.AddGems(100);
			uiM.UpdateShopGemsCount(player.diamonds);
			// Create a way to reset the ShowAd method because it's being called continuously after exiting the shop
		}

		// If the ad successfully loads, add a listener to the button and enable it:
		public void OnUnityAdsAdLoaded(string adUnitId)
		{
			Debug.Log("Ad Loaded: " + adUnitId);

			if (adUnitId.Equals(_adUnitId))
			{
				// Configure the button to call the ShowAd() method when clicked:
				_showAdButton.onClick.AddListener(ShowAd);
				// Enable the button for users to click:
				_showAdButton.interactable = true;
			}
		}

		// Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
		public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
		{
			Debug.Log("ads show complete");
			if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
			{
				Debug.Log("OnUnityAdsShowComplete called");
				// Grant a reward.

				// Load another ad:
				Advertisement.Load(_adUnitId, this);
			}
		}

		// Implement Load and Show Listener error callbacks:
		public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
		{
			Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
			// Use the error details to determine whether to try to load another ad.
		}

		public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
		{
			Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
			// Use the error details to determine whether to try to load another ad.
		}

		public void OnUnityAdsShowStart(string adUnitId) { }
		public void OnUnityAdsShowClick(string adUnitId) { }

		private void OnDestroy()
		{
			// Clean up the button listeners:
			_showAdButton.onClick.RemoveAllListeners();
		}

		public void OnUnityAdsReady(string placementId)
		{

		}

		public void OnUnityAdsDidError(string message)
		{

		}

		public void OnUnityAdsDidStart(string placementId)
		{

		}

		public void OnUnityAdsDidFinish(string placementId, ShowResult result)
		{
			Debug.Log("unity ads finished");

		}
	}
}
