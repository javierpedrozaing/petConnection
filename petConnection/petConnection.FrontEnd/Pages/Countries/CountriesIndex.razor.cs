using System;
using Microsoft.AspNetCore.Components;
using petConnection.FrontEnd.Repositories;
using System.Net;
using CurrieTechnologies.Razor.SweetAlert2;
using petConnection.Share.Entitties;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Blazored.Modal.Services;
using Blazored.Modal;

namespace petConnection.FrontEnd.Pages.Countries
{
    [Authorize(Roles = "Admin")]
    public partial class CountriesIndex
    {
        private int currentPage = 1;
        private int totalPages;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        [Parameter, SupplyParameterFromQuery] public string Page { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public int Records { get; set; } = 10;
        [CascadingParameter] IModalService Modal { get; set; } = default!;

        public List<Country>? Countries { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task ShowModalAsync(int id = 0, bool isEdit = false)
        {
            IModalReference modalReference;

            if (isEdit)
            {
                modalReference = Modal.Show<CountryEdit>(string.Empty, new ModalParameters().Add("Id", id));
            }
            else
            {
                modalReference = Modal.Show<CountryCreate>();
            }

            var result = await modalReference.Result;
            if (result.Confirmed)
            {
                await LoadAsync();
            }
        }

        private async Task SelectedPageAsync((int, int) pageData)
        {
            currentPage = pageData.Item1;
            await LoadAsync(currentPage, pageData.Item2);
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
               url = $"api/countries/full";
            }
            else
            {
               url = $"api/countries?page={page}&RecordsNumber={totalRecords}";
            }

            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"&filter={Filter}";
            }

            var responseHttp = await Repository.GetAsync<List<Country>>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return false;
            }
            Countries = responseHttp.Response;
            return true;
        }

        private async Task LoadPagesAsync()
        {
            var url = "api/countries/totalPages";
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

        private async Task HandlePageSizeChanged(int value)
        {
            int page = 1;
            Records = value;
            await LoadAsync(page, Records);
        }

        private async Task CleanFilterAsync()
        {
            Filter = string.Empty;
            await ApplyFilterAsync(Filter);
        }

        private async Task ApplyFilterAsync(string value)
        {
            int page = 1;
            Filter = value;
            await LoadAsync(page);
            await SelectedPageAsync((page, Records));
        }

        private async Task DeleteAsycn(Country country)
        {
            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = $"¿Estas seguro de querer borrar el país: {country.Name}?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
            });
            var confirm = string.IsNullOrEmpty(result.Value);
            if (confirm)
            {
                return;
            }

            var responseHttp = await Repository.DeleteAsync<Country>($"api/countries/{country.Id}");
            if (responseHttp.Error)
            {
                if (responseHttp.Httpresponsemessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/countries");
                }
                else
                {
                    var mensajeError = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", mensajeError, SweetAlertIcon.Error);
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
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro borrado con éxito.");
        }
    }
}

