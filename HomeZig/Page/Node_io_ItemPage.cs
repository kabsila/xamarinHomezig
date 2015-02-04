using System;
using Xamarin.Forms;


namespace HomeZig
{
	public class Node_io_ItemPage : ContentPage
	{
		SwitchCell switchCellLeft; 
		SwitchCell switchCellRight;
		public static Db_allnode item;
		public Node_io_ItemPage ()
		{
			Label header = new Label
			{
				Text = "Name of node",
				//Font = Font.Default,
				FontAttributes = FontAttributes.Bold,
				FontSize = 40,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.EndAndExpand
			};

			switchCellLeft = new SwitchCell
			{
				Text = "Left"
				//HorizontalOptions = LayoutOptions.FillAndExpand,
				//VerticalOptions = LayoutOptions.CenterAndExpand
			};
			switchCellLeft.OnChanged += DependencyService.Get<IDeviceCall> ().switchLeft_OnChange;

			switchCellRight = new SwitchCell
			{
				Text = "Right"
				//HorizontalOptions = LayoutOptions.FillAndExpand,
				//VerticalOptions = LayoutOptions.CenterAndExpand
			};
			switchCellRight.OnChanged += DependencyService.Get<IDeviceCall> ().switchRight_OnChange;

			TableView tableView = new TableView
			{
				Intent = TableIntent.Form,
				Root = new TableRoot
				{
					new TableSection
					{
						switchCellLeft,
						switchCellRight
					}
				}
			};
			var layout = new StackLayout 
			{
				//Padding = new Thickness(0, 0, 0, 0),
				//Orientation = StackOrientation.Horizontal,
				//HorizontalOptions = LayoutOptions.StartAndExpand,
				//VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = 
				{
					header,
					tableView
				}
			};
			Content = layout;
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			item = (Db_allnode)BindingContext;
			string state = NumberConversion.hex2binary (item.node_io);
			string stateLeft = state.Substring(6, 1);
			string stateRight = state.Substring(7, 1);
			if (stateLeft.Equals ("0")) 
			{ 
				stateLeft = "false";
			} 
			else 
			{
				stateLeft = "true";
			}

			if (stateRight.Equals ("0")) 
			{ 
				stateRight = "false";
			} 
			else 
			{
				stateRight = "true";
			}

			switchCellLeft.On = Convert.ToBoolean(stateLeft);
			switchCellRight.On = Convert.ToBoolean(stateRight);
			//addressListView.ItemsSource = await App.Database.GetItemByDeviceType(deviceType.node_deviceType.ToString());
		}

	}
}

