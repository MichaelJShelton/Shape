using Xamarin.Forms;

namespace Shape
{
	/// <summary>
	/// Question page.
	/// </summary>
	public partial class QuestionPage : ContentPage
	{
		Answer selectedItem;

		public QuestionPage(int id)
		{
			Question Q = SurveyPage.Questions[id];

			var Answers = new ListView(ListViewCachingStrategy.RecycleElement)
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.EndAndExpand,
				ItemsSource = Q.Answers,
				RowHeight = 40,
				HasUnevenRows = false,
				ItemTemplate = new DataTemplate(() =>
				{
					TextCell tc = new TextCell();
					tc.SetBinding(TextCell.TextProperty, "Text");
					return tc;
				})
			};

			var resetButton = new Button
			{
				IsEnabled = false,
				BackgroundColor = Color.White
			};

			var questionLabel = new Label
			{
				HorizontalTextAlignment = TextAlignment.Start,
				Text = id > 0 ? id + ".  " + Q.Text : Q.Text,
				FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
				TextColor = Color.Black
			};

			Answers.ItemSelected += (o, e) =>
			{
				// ItemSelected is called on deselection, which results in SelectedItem being set to null
				if (e.SelectedItem == null)
				{
					return;
				}

				selectedItem = (Answer)e.SelectedItem;
				if (selectedItem.UpdateResult != null)
				{
					Q.Answered = true;
					selectedItem.UpdateResult(selectedItem.Value);
					Answers.IsEnabled = false;
					resetButton.Text = "Reset Selection";
					resetButton.IsEnabled = true;
				}
				else
				{
					Answers.SelectedItem = null;
				}
			};

			resetButton.Clicked += (sender, e) =>
			{
				selectedItem.UpdateResult(-1 * selectedItem.Value);
				Q.Answered = false;
				Answers.IsEnabled = true;
				Answers.SelectedItem = null;
				resetButton.Text = string.Empty;
				resetButton.IsEnabled = false;
			};

			var stack = new StackLayout
			{
				Padding = new Thickness(20, 20, 20, 20),
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children =
					{
						new Label {
							Text = string.Empty,
							HeightRequest = 40
						},
						questionLabel,
						new Label {
							Text = string.Empty,
							HeightRequest = 40
						},
						Answers,
						resetButton
					}
			};

			Content = new ScrollView
			{
				Content = stack
			};

			InitializeComponent();
		}
	}
}

