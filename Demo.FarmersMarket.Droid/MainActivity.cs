using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Demo.FarmersMarket.Core;
using System.Collections.Generic;
using Android.Views.InputMethods;

namespace Demo.FarmersMarket.Droid
{
	[Activity (Label = "Farmers Market", MainLauncher = true)]
	public class MainActivity : Activity
	{
		private EditText _zipCodeText;
		private ViewFlipper _viewFlipper;
		private ListView _listView;
		private MarketAdapter _adapter;

		private static MainViewModel _viewModel;
		public static MainViewModel ViewModel
		{
			get { return _viewModel ?? (_viewModel = new MainViewModel ()); }
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);		

			_viewFlipper = FindViewById<ViewFlipper> (Resource.Id.viewFlipper);
			_zipCodeText = FindViewById<EditText> (Resource.Id.zipCodeText);
			_listView = FindViewById<ListView> (Resource.Id.List);
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += async delegate {
				await ViewModel.SearchMarkets(_zipCodeText.Text);
				_adapter = new MarketAdapter(this, ViewModel.Markets);
				_listView.Adapter = _adapter;
				InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
				imm.HideSoftInputFromWindow(_zipCodeText.WindowToken, HideSoftInputFlags.NotAlways);
				_viewFlipper.DisplayedChild = 1;
			};

			_listView.ItemClick += async delegate(object sender, AdapterView.ItemClickEventArgs e) {
				var item = _adapter[e.Position];
				await ViewModel.GetMarketDetails(item.Id);

				var nameText = FindViewById<TextView>(Resource.Id.nameText);
				nameText.Text = item.MarketName;
				var addressText = FindViewById<TextView>(Resource.Id.addressText);
				addressText.Text = ViewModel.SelectedMarket.Address;
				var productsText = FindViewById<TextView>(Resource.Id.productsText);
				productsText.Text = ViewModel.SelectedMarket.Products;
				var scheduleText = FindViewById<TextView>(Resource.Id.scheduleText);
				scheduleText.Text = ViewModel.SelectedMarket.Schedule;
				_viewFlipper.DisplayedChild = 2;
			};
		}

		public override void OnBackPressed ()
		{
			if (_viewFlipper.DisplayedChild > 0) {
				_viewFlipper.DisplayedChild -= 1;
			} else {
				base.OnBackPressed ();
			}
		}
	}

	public class MarketAdapter : BaseAdapter<Market>
	{
		private List<Market> _items;
		private Activity _context;

		public MarketAdapter(Activity context, List<Market> items) : base() {
			_context = context;
			_items = items;
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override Market this[int position] {
			get { return _items [position]; }
		}

		public override int Count {
			get { return _items.Count; }
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			View view = convertView;
			if (view == null) {
				view = _context.LayoutInflater.Inflate (Android.Resource.Layout.SimpleListItem1, null);
			}
			view.FindViewById<TextView> (Android.Resource.Id.Text1).Text = _items [position].MarketName;
			return view;
		}
	}
}


