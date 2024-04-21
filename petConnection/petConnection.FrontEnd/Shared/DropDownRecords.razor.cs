using System;
using Microsoft.AspNetCore.Components;

namespace petConnection.FrontEnd.Shared
{
	public partial class DropDownRecords
	{
		private int SelectedValue { get; set; }

		[Parameter] public EventCallback<int> UpdateListAsync { get; set; }

		private async Task UpdateList()
		{
			await UpdateListAsync.InvokeAsync(SelectedValue);
		}
	}
}

