using System;
using Xamarin.Forms;

namespace HomeZig
{
	public interface I_Node_io_RemoteControl
	{
		void NodeIoRemoteControl_Tapped(object sender, ItemTappedEventArgs e);
		//void submitRenameButton_Click(object sender, EventArgs e);
		void submitRenameButton_Click(RemoteData remote, string newName);
		void cancelRenameButton_Click(object sender, EventArgs e); 
	}

}

