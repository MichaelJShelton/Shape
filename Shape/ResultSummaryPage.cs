using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Shape
{
	public class ResultSummaryPage : ContentPage
	{
		public ResultSummaryPage()
		{
			Button resultsButton = new Button()
			{
				Text = "See Your Results",
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				HorizontalOptions = LayoutOptions.Center
			};

			resultsButton.Clicked += (sender, e) =>
			{
				bool complete = true;

				// Validate that all questions have been answered.
				foreach (Question q in SurveyPage.Questions)
				{
					if (!q.Answered)
					{
						Label failureMessage = new Label()
						{
							FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
							Text = "\n\r\n\rNot all questions have been answered. Please go back and answer the remaining question(s).\n You have missed Question #: " + q.Id
						};
						complete = false;

						resultsButton.Text = "Refresh Your Results.";
						Content = new StackLayout
						{
							Padding = new Thickness(20, 0, 20, 20),
							Children = {
								failureMessage,
								resultsButton
							}
						};
						return;
					}
				}

				if (complete)
				{

					StackLayout resultsView = new StackLayout
					{
						Padding = new Thickness(20, 0, 20, 20)
					};

					resultsView.Children.Add(new Label { Text = "\n\r\n\r" });

					Array.Sort(SurveyPage.Results);
					for (int i = 0; i < 3; i++)
					{
						ShapeResult sr = SurveyPage.Results[i];
						resultsView.Children.Add(
							new Label()
							{
								Text = sr.Title,
								FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
								FontAttributes = FontAttributes.Bold
							});
						resultsView.Children.Add(
							new Label()
							{
								Text = sr.Description,
								FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
								TextColor = Color.FromHex("5593BD")
							});
						resultsView.Children.Add(
							new Label()
							{
								Text = sr.Verses,
								FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
								TextColor = Color.FromHex("B5CD36")
							});
						resultsView.Children.Add(
							new BoxView // Just using this as a separator
							{
								HeightRequest = 1,
								BackgroundColor = Color.Black,
								HorizontalOptions = LayoutOptions.FillAndExpand
							});
					}

					resultsView.Children.Add(
						new ShareView(GenerateResultsString()));

					Content = new ScrollView
					{
						Content = resultsView
					};
				}
			};

			Content = new ScrollView
			{
				Content = new StackLayout
				{
					Children =
					{
						new Label()
						{
							Text = "\n\r\n\rWell Done!\n\r",
							FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
							HorizontalOptions = LayoutOptions.Center
						},
						resultsButton
					}
				}
			};
		}

		private string GenerateResultsString()
		{
			StringBuilder sb = new StringBuilder("Thank you for taking the Shape Survey.\n");
			sb.AppendLine("Here are your results.\n");
			foreach (var result in SurveyPage.Results)
			{
				sb.AppendLine(result.ToString());
			}

			return sb.ToString();
		}
	}
}


