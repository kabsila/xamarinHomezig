using System;
using Xamarin.Forms;
using System.Diagnostics;
using Acr.UserDialogs;

namespace HomeZig
{
	public class Node_io_RemoteControl_Cell : ViewCell
	{
		
		public Node_io_RemoteControl_Cell ()
		{
			var remoteButtonName = new Label 
			{ 
				
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), 
				VerticalOptions = LayoutOptions.CenterAndExpand,


			};
			remoteButtonName.SetBinding (Label.TextProperty, "remote_button_name");

			var deleteAction = new MenuItem { Text = "Delete" }; // red background
			deleteAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));

			deleteAction.Clicked += (sender, e) => {

				var mi = ((MenuItem)sender);
				var mo = (RemoteData)mi.BindingContext;

				DependencyService.Get<I_Delete_Remote> ().deleteRemote_Tapped(mo);

			};

			var renameAction = new MenuItem { Text = "Rename" }; // red background
			renameAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
			renameAction.Clicked += async (sender, e) =>  {
				var mi = ((MenuItem)sender);
				var mo = (RemoteData)mi.BindingContext;

				var result = await UserDialogs.Instance.PromptAsync(new PromptConfig {
					Title = "Rename",
					Text = mo.remote_button_name,
					IsCancellable = true,
					Placeholder = "Type new name"

				});

				if(!result.Text.Equals(mo.new_button_name)){
					
					DependencyService.Get<I_Node_io_RemoteControl> ().submitRenameButton_Click(mo, result.Text);
				}
				//DependencyService.Get<I_Node_io_RemoteControl> ().submitRenameButton_Click;
				//MessagingCenter.Send<ContentPage, RemoteData> (new ContentPage(), "RenameRemote_Clicked", mo);
				System.Diagnostics.Debug.WriteLine("RenameRemote_Clicked");
			};

			ContextActions.Add (deleteAction);
			ContextActions.Add (renameAction);


			View = new StackLayout {
				//Orientation = StackOrientation.Vertical,
				//HorizontalOptions = LayoutOptions.CenterAndExpand,
				Padding = new Thickness (15, 0, 0, 0),
				Children = {
					remoteButtonName
				}
			};
		}
	}
}

