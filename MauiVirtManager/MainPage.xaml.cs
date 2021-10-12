using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;

namespace MauiVirtManager
{
	public partial class MainPage : ContentPage
	{
		int count = 0;

		string url = "https://localhost:5001/virt";

		public MainPage()
		{
			InitializeComponent();
		}

		private void OnCounterClicked(object sender, EventArgs e)
		{
			count++;
			CounterLabel.Text = $"Current count: {count}";

			SemanticScreenReader.Announce(CounterLabel.Text);
		}
	}
}
