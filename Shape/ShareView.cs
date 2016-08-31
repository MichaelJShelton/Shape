using System;
using Plugin.Share;
using Xamarin.Forms;

namespace Shape
{
	public class ShareView : Grid
	{
		public ShareView(string message)
		{
			BackgroundColor = Color.Transparent;
			HorizontalOptions = LayoutOptions.End;
			VerticalOptions = LayoutOptions.End;

			// Add a Transparent Button and an Image to each Grid location.
			Children.Add(
				new Image
				{
					IsVisible = true,
					Source = ImageSource.FromResource("Shape.Resources.share.png"),
					HeightRequest = 50,
					WidthRequest = 50
				}, 0, 0);

			var shareBtn = new Button
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				BackgroundColor = Color.Transparent,
				HeightRequest = 50,
				WidthRequest = 50
			};

			shareBtn.Clicked += (sender, e) =>
			{
				// Do Sharing stuff here.
				CrossShare.Current.Share(message, "Shape Survey");
			};

			Children.Add(shareBtn, 0, 0);
		}
	}
}

