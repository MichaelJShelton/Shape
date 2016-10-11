using System.Text;
using Xamarin.Forms;

namespace Shape
{
	public class WelcomePage : ContentPage
	{
		private Image shapeImg = new Image
		{
			IsVisible = true,
			Source = ImageSource.FromResource("Shape.Resources.ShapeNoBorder.jpg"),
			HorizontalOptions = LayoutOptions.CenterAndExpand
		};

		private Image shapeBasicImg = new Image
		{
			IsVisible = true,
			Source = ImageSource.FromResource("Shape.Resources.ShapeBasic.jpg"),
			HorizontalOptions = LayoutOptions.CenterAndExpand
		};

		public WelcomePage()
		{
			var aboutBtn = new Button
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HeightRequest = 50,
				Text = "Learn More."
			};

			var popup = InitializePopupContent(aboutBtn);

			aboutBtn.Clicked += (sender, e) =>
				{
					ShowPopup(aboutBtn, popup);
				};

			InitializeContent(aboutBtn);
		}

		void ShowPopup(Button caller, View content)
		{
			caller.IsEnabled = false;
			Content = content;
		}

		private static void DismissPopup(WelcomePage instance, Button caller)
		{
			caller.IsEnabled = true;
			instance.InitializeContent(caller);
		}

		private void InitializeContent(Button aboutBtn)
		{
			Content = new ScrollView
			{
				Content = new StackLayout
				{
					Padding = new Thickness(20, 40, 20, 20),
					Children =
					{
						shapeImg,
						aboutBtn,
						new Label
						{
							Text = "Swipe from right to left to begin.",
							FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
							TextColor = Color.FromHex("5593BD"),
							HorizontalOptions = LayoutOptions.CenterAndExpand,
							VerticalOptions = LayoutOptions.EndAndExpand,
							IsVisible = true
						}
					}
				}
			};
		}

		private View InitializePopupContent(Button caller)
		{
			var sb = new StringBuilder();
			sb.AppendLine("    Spiritual gifts are not the same as talents, but are spiritual strengths God gives for being a part of his work in the world. The set of gifts in this survey is by no means exhaustive. There are many different attributes identified as spiritual gifts in the Bible, and even those are not complete listings of the ways God gifts people to do His work.");
			sb.AppendLine("    This assessment is simply meant to be a tool to help you better understand the gifts God is expressing in you. Throughout your life, as you grow, change, and experience different things your spiritual gifts may change. We hope that this list provides a glimpse into the part God has for you in his body right now. To learn more about spiritual gifts you can look at:");

			var dismiss = new Button
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.EndAndExpand,
				BackgroundColor = Color.Transparent,
				HeightRequest = 50,
				Text = "Dismiss"
			};

			dismiss.Clicked += (sender, e) =>
			{
				DismissPopup(this, caller);
			};

			return new ScrollView
			{
				Content = new StackLayout
					{
						Padding = new Thickness(20, 20, 20, 20),
						Children = {
							shapeBasicImg,
							new Label
							{
								Text = sb.ToString(),
								FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label))
							},
							new Label
							{
								Text = "1 Corinthians 12:27-31;\nRomans 12:3-8;\nEphesians 4:11-16.",
								FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
								TextColor = Color.FromHex("B5CD36")
							},
							dismiss
						}
					}
			};
		}
	}
}


