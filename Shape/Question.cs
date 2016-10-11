using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using Xamarin.Forms;

namespace Shape
{
	public class Question
	{
		public int Id { get; set; }
		public string Text { get; set; }
		public List<Answer> Answers { get; set; }
		public bool Answered { get; set; }
		public Color ShadedColor
		{
			get
			{
				return Answered ? Color.Green : Color.White;
			}
		}
	}

	public class Answer
	{
		public string Text { get; set; }
		public int Value { get; set; }
		public Action<int> UpdateResult { get; set; }

		public override string ToString()
		{
			return Text;
		}
	}

	public static class AnswerExtensions
	{
		public static Answer Clone(this Answer other)
		{
			var clone = new Answer();
			clone.Text = other.Text;
			clone.Value = other.Value;
			return clone;
		}
	}

	public class ShapeResult : IComparable
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string Verses { get; set; }
		public string[] Components { get; set; }
		public int Value { get; private set; }

		public void UpdateValue(int increment)
		{
			Value += increment;
		}

		public int CompareTo(object obj)
		{
			return ((ShapeResult)obj).Value.CompareTo(Value);
		}

		public override string ToString()
		{
			return string.Format("Spiritual Gift: {0}\nDescription: {1}\nVerses: {2}\n\nYour Score: {3}\n\n", Title, Description, Verses, Value);
		}
	}

	public static class QuestionFactory
	{
		public static void InitializeQuestions()
		{
			var assembly = typeof(SurveyPage).GetTypeInfo().Assembly;
			Stream stream = assembly.GetManifestResourceStream("Shape.ShapeQuestions.xml");
			XDocument survey = XDocument.Load(stream);

			var surveyNode = survey.Nodes().FirstOrDefault() as XElement;
			var xquestions = surveyNode.Descendants().First((XElement arg) => arg.Name.ToString().Equals("Questions"));
			var xanswers = surveyNode.Descendants().First((XElement arg) => arg.Name.ToString().Equals("Answers"));
			var xresults = surveyNode.Descendants().First((XElement arg) => arg.Name.ToString().Equals("Results"));

			var answers = new List<Answer>();
			foreach (XElement ans in xanswers.Descendants())
			{
				var next = new Answer
				{
					Text = ans.Attribute("text").Value,
					Value = int.Parse(ans.Attribute("value").Value)
				};

				answers.Add(next);
			}

			int questionCount = int.Parse(xquestions.Attribute("count").Value);
			Question[] questions = new Question[questionCount + 1];
			var sb = new StringBuilder();
			sb.AppendLine("Instructions:\n\nPlease select your answers on each page.");
			sb.AppendLine("\nWhen you have completed all of the questions you will be able to see your results.\n\nYou may swipe back and forth between questions.");
			questions[0] = new Question()
			{
				Id = 0,
				Text = sb.ToString(),
				Answers = new List<Answer>(),
				Answered = true
			};

			foreach (XElement question in xquestions.Descendants())
			{
				var q = new Question
				{
					Id = int.Parse(question.Attribute("id").Value),
					Text = question.Attribute("text").Value,
					Answers = new List<Answer>()
				};

				questions[q.Id] = q;
			}

			var results = new List<ShapeResult>();
			foreach (XElement res in xresults.Descendants())
			{
				var shapeRes = new ShapeResult
				{
					Components = res.Attribute("components").Value.Split(','),
					Title = res.Attribute("text").Value,
					Description = res.Attribute("description").Value,
					Verses = res.Attribute("verses").Value
				};

				foreach (string component in shapeRes.Components)
				{
					int id = int.Parse(component);

					if (id <= questionCount)
					{
						// Create a new instance for each Answer for this affecting Question
						// and set the Action so that if the answer is selected it updates the value
						// in this result.
						foreach (Answer a in answers)
						{
							Answer aClone = a.Clone();
							aClone.UpdateResult = shapeRes.UpdateValue;
							questions[id].Answers.Add(aClone);
						}
					}
				}

				results.Add(shapeRes);
			}

			SurveyPage.Questions = questions;
			SurveyPage.Results = results.ToArray();
		}
	}
}

