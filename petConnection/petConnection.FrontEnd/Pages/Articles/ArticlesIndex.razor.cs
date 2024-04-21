using System;
using System.Net;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using petConnection.FrontEnd.Repositories;
using petConnection.FrontEnd.Shared;
using petConnection.Share.Entitties;

namespace petConnection.FrontEnd.Pages.Articles
{
	public partial class ArticlesIndex
	{
        private int currentPage = 1;
        private int totalPages;        

        [Inject] private IRepository Repository { get; set; } = null!;

        private List<Article> Articles { get; set; }

        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        [Inject] private NavigationManager navigationManager { get; set; } = null!; // framework component

        [Parameter, SupplyParameterFromQuery] public string Page { get; set; } = string.Empty;

        [Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;

        [Parameter, SupplyParameterFromQuery] public int Records { get; set; } = 10;

        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            var responseHttp = await Repository.GetAsync<List<Article>>("api/articles");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            Articles = responseHttp.Response;
        }

        private async Task SelectedPageAsync((int, int) pageData)
        {
            currentPage = pageData.Item1;
            await LoadAsync(currentPage, pageData.Item2);
        }

        private async Task HandlePageSizeChanged(int value)
        {
            int page = 1;
            Records = value;
            await LoadAsync(page, Records);
        }

        private async Task LoadAsync(int page = 1, int totalRecords = 10)
        {
            if (!string.IsNullOrWhiteSpace(Page))
            {
                page = Convert.ToInt32(Page);
            }
            
            var ok = await LoadListAsync(page, totalRecords);
            if (ok)
            {
                await LoadPagesAsync();
            }
        }

        private async Task<bool> LoadListAsync(int page, int totalRecords)
        {
            var url = "";

            if (totalRecords == 1000)
            {
                url = $"api/articles/full";
            }
            else
            {
                url = $"api/articles?page={page}&RecordsNumber={totalRecords}";
            }

            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"&filter={Filter}";
            }

            var responseHttp = await Repository.GetAsync<List<Article>>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return false;
            }
            Articles = responseHttp.Response;
            return true;
        }

        private async Task LoadPagesAsync()
        {
            var url = "api/articles/totalPages";
            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"?filter={Filter}";
            }

            if (Records > 0)
            {
                url += $"?RecordsNumber={Records}";
            }

            var responseHttp = await Repository.GetAsync<int>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            totalPages = responseHttp.Response;
        }

        private async Task CleanFilterAsync()
        {
            Filter = string.Empty;
            ApplyFilterAsync(Filter);
        }


        private async Task ApplyFilterAsync(string value)
        {
            int page = 1;
            Filter = value;
            await LoadAsync(page);
            await SelectedPageAsync((page, Records));
        }

        private async Task DeleteAsync(Article article)
        {
            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = $"¿Estas seguro de querer borrar este artículo {article.Title} ?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true
            });

            var confirm = string.IsNullOrEmpty(result.Value);
            if (confirm)
            {
                return;
            }

            var responseHttp = await Repository.DeleteAsync<Article>($"api/articles/{article.Id}");

            if (responseHttp.Error)
            {
                if (responseHttp.Httpresponsemessage.StatusCode == HttpStatusCode.NotFound)
                {
                    navigationManager.NavigateTo("/articles");
                }
                else
                {
                    var message = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                }
                return;
            }

            await LoadAsync();

            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Artículo borrado con éxito");
        }
    }
}

