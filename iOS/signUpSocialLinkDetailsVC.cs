using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Collections.Generic;
using Facebook.LoginKit;
using Facebook.CoreKit;
using CoreGraphics;

namespace donow.iOS
{
	partial class signUpSocialLinkDetailsVC : UIViewController
	{
		List<string> readPermissions = new List<string> { "public_profile", "email", "user_friends" };
		LoginButton loginButton;
		ProfilePictureView pictureView;
		UILabel nameLabel;

		public signUpSocialLinkDetailsVC (IntPtr handle) : base (handle)
		{
		}

//		public override void ViewDidLoad ()
//		{
//			
//		}

		public override void ViewDidLoad ()
		{			
			Profile.Notifications.ObserveDidChange ((sender, e) => {

				if (e.NewProfile == null)
					return;

				nameLabel.Text = e.NewProfile.Name;
			});

			// Set the Read and Publish permissions you want to get
			loginButton = new LoginButton (new CGRect (100, 100, 220, 46)) {
				LoginBehavior = LoginBehavior.Native,
				ReadPermissions = readPermissions.ToArray ()
			};

			// Handle actions once the user is logged in
			loginButton.Completed += (sender, e) => {
				if (e.Error != null) {
					// Handle if there was an error
				}

				if (e.Result.IsCancelled) {
					// Handle if the user cancelled the login request
				}

				// Handle your successful login
			};


			// Handle actions once the user is logged out
			loginButton.LoggedOut += (sender, e) => {
				// Handle your logout
				nameLabel.Text = "";
			};

			// The user image profile is set automatically once is logged in
			pictureView = new ProfilePictureView (new CGRect (160, 160, 220, 220));

			// Create the label that will hold user's facebook name
			nameLabel = new UILabel (new CGRect (20, 319, 280, 21)) {
				TextAlignment = UITextAlignment.Center,
				BackgroundColor = UIColor.Clear
			};

			// If you have been logged into the app before, ask for the your profile name
			if (AccessToken.CurrentAccessToken != null) {
				var request = new GraphRequest ("/me/friends", null, AccessToken.CurrentAccessToken.TokenString, null, "GET");
				request.Start ((connection, result, error) => {
					// Handle if something went wrong with the request
					if (error != null) {
						new UIAlertView ("Error...", error.Description, null, "Ok", null).Show ();
						return;
					}

					// Get your profile name
					var userInfo = result as NSDictionary;
					var friendlist = userInfo["data"];
					var summary = userInfo["summary"];
				});
			}

			// Add views to main view
			View.AddSubview (loginButton);
			View.AddSubview (pictureView);
			View.AddSubview (nameLabel);
		}
	}
}
