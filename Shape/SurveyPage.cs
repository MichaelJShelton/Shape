using System;
using Xamarin.Forms;

namespace Shape
{
	public class SurveyPage : CarouselPage
	{
		public static Question[] Questions;
		public static ShapeResult[] Results;

		public SurveyPage()
		{
			Children.Add(new WelcomePage());
			foreach (Question q in SurveyPage.Questions)
			{
				Children.Add(new QuestionPage(q.Id));
			}

			Children.Add(new ResultSummaryPage());
		}
	}
}

