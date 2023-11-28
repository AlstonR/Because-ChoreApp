using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace because.ViewModels
{
	public class AppViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private bool _isBusy = true;
		public bool IsBusy
		{
			get => _isBusy;
			private set
			{
				if (_isBusy != value)
				{
					_isBusy = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsBusy)));
				}
			}
		}

		public void ToggleIsBusy(bool isBusy) => IsBusy = isBusy;
	}
}

