using System;
using MonoTouch.UIKit;
using Demo.FarmersMarket.Core;
using System.Drawing;

namespace Demo.FarmersMarket.Touch
{
	public class MarketDetailViewController : UIViewController
	{

		private static MainViewModel _viewModel;
		public static MainViewModel ViewModel
		{
			get { return _viewModel ?? (_viewModel = new MainViewModel ()); }
		}

		public Market Item { get; set; }
		public MarketDetailViewController ()
		{
		}


		private UILabel _address;
		private UILabel _products;
		private UILabel _schedule;

		public override void ViewDidLoad ()
		{
			View = new UIView { BackgroundColor = UIColor.White };

			base.ViewDidLoad ();

			EdgesForExtendedLayout = UIRectEdge.None;

			Title = Item.MarketName;

			var aLabel = new UILabel (new RectangleF (5, 10, 100, 25));
			aLabel.Text = ("Address: ");

			_address = new UILabel (new RectangleF (5, 35, 300, 90));
			_address.Lines = 5;

			var pLabel = new UILabel (new RectangleF (5, 100, 100, 25));
			pLabel.Text = ("Products: ");

			_products = new UILabel (new RectangleF (5, 125, 300, 90));
			_products.Lines = 6;

			var sLabel = new UILabel (new RectangleF (5, 230, 100, 25));
			sLabel.Text = ("Schedule: ");

			_schedule = new UILabel (new RectangleF (5, 255, 300, 90));
			_schedule.Lines = 5;

			Add (aLabel);
			Add (pLabel);
			Add (sLabel);
			Add (_address);
			Add (_products);
			Add (_schedule);

			LoadUI ();
		}

		private async void LoadUI()
		{
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
			await ViewModel.GetMarketDetails (Item.Id);
			_address.Text = ViewModel.SelectedMarket.Address;
			_address.SizeToFit ();

			_products.Text = ViewModel.SelectedMarket.Products;
			_products.SizeToFit ();

			_schedule.Text = ViewModel.SelectedMarket.Schedule;
			_schedule.SizeToFit ();
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
		}
	}
}

