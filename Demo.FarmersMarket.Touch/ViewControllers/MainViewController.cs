using System;
using Demo.FarmersMarket.Core;
using MonoTouch.UIKit;
using System.Drawing;
using System.Collections.Generic;

namespace Demo.FarmersMarket.Touch
{
	public class MainViewController : UITableViewController
	{
		private static MainViewModel _viewModel;
		public static MainViewModel ViewModel
		{
			get { return _viewModel ?? (_viewModel = new MainViewModel ()); }
		}

		public MainViewController() {
			Title = "Farmers Markets";
		}
			

		private UISearchBar _searchBar;
		private TableSource _source;
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			EdgesForExtendedLayout = UIRectEdge.None;
			_source = new TableSource (ViewModel.Markets, this);
			TableView.Source = _source;

			_searchBar = new UISearchBar ();
			_searchBar.Placeholder = "Enter Zip Code";
			_searchBar.SizeToFit ();
			_searchBar.SearchButtonClicked += async delegate(object sender, EventArgs e) {
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
				await ViewModel.SearchMarkets(_searchBar.Text);
				_source.Items = ViewModel.Markets;
				TableView.ReloadData();
				_searchBar.ResignFirstResponder();
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
			};

			TableView.TableHeaderView = _searchBar;
		}

	}

	class TableSource : UITableViewSource
	{
		public List<Market> Items;
		private string _cellIdentifier = "TableCell";
		private MainViewController _controller;

		public TableSource(List<Market> items, MainViewController controller)
		{
			Items = items;
			_controller = controller;
		}

		public override int NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return Items == null ? 0 : Items.Count;
		}


		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (_cellIdentifier);
			if (cell == null)
				cell = new UITableViewCell (UITableViewCellStyle.Default, _cellIdentifier);

			cell.TextLabel.Text = Items [indexPath.Row].MarketName;
			return cell;
		}

		public override void RowSelected (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var vc = new MarketDetailViewController () { Item = Items[indexPath.Row] };
			_controller.NavigationController.PushViewController (vc, true);
		}
	}
}

