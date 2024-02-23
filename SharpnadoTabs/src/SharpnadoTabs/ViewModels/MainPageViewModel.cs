using System;
using System.Windows.Input;

namespace SharpnadoTabs.ViewModels
{
	public class MainPageViewModel : BaseViewModel
	{
		private MarvelHeroesViewModel _marvelHeroesBindingContext;
		public MarvelHeroesViewModel MarvelHeroesBindingContext
		{
			get => _marvelHeroesBindingContext;
			set => SetProperty(ref _marvelHeroesBindingContext, value);
		}

		public ICommand TabChangeCommand { get; private set; }

		public MainPageViewModel()
		{
			TabChangeCommand = new Command(ExecuteTabChangeCommand);
		}

		private void ExecuteTabChangeCommand()
		{
			string message = "We hit here!";
		}
	}
}

