using System;
using Microsoft.AspNetCore.Components;

namespace petConnection.FrontEnd.Shared
{
	public partial class Filter<TModel>
	{		
		[Parameter] public EventCallback<string> ApplyFilterAsync { get; set; }

        [Parameter] public EventCallback CleanFilterAsync { get; set; }

        private string FilterValue { get; set; } = null!;

        public async Task ApplyFilter()
		{
			await ApplyFilterAsync.InvokeAsync(FilterValue);
		}

		public async Task CleanFilter()
		{
			FilterValue = string.Empty;
			await CleanFilterAsync.InvokeAsync();
		}

	}
}