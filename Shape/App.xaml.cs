using Xamarin.Forms;

namespace Shape
{
	public partial class App : Application
	{
		public App()
		{
			QuestionFactory.InitializeQuestions();
			InitializeComponent();

			MainPage = new SurveyPage();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}

