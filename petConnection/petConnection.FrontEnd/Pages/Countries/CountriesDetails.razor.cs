using System;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using petConnection.FrontEnd.Repositories;
using petConnection.Share.Entitties;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Blazored.Modal.Services;
using Blazored.Modal;
using petConnection.FrontEnd.Pages.States;

namespace petConnection.FrontEnd.Pages.Countries
{
    [Authorize(Roles = "Admin")]
    public partial class CountriesDetails
    {
        private Country? country;
        private List<State>? states;
        private int currentPage = 1;
        private int totalPages;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;

        [Parameter] public int CountryId { get; set; }
        [Parameter, SupplyParameterFromQuery] public string Page { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public int Records { get; set; } = 10;
        [CascadingParameter] IModalService Modal { get; set; } = default!;

        public List<State>? States { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadStatesAsync();
        }

        private async Task ShowModalAsync(int id = 0, bool isEdit = false)
        {
            IModalReference modalReference;

            if (isEdit)
            {
                modalReference = Modal.Show<StateEdit>(string.Empty, new ModalParameters().Add("StateId", id));
            }
            else
            {
                modalReference = Modal.Show<StateCreate>(string.Empty, new ModalParameters().Add("CountryId", CountryId));
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
            await LoadStatesAsync(currentPage, pageData.Item2);
        }

        private async Task LoadStatesAsync(int page = 1, int totalRecords = 10)
        {
            var ok = await LoadCountryAsync();
            if (ok)
            {
                ok = await LoadStatesAsync(page);
                if (ok)
                {
                    await LoadPagesAsync();
                }
            }
        }

        private async Task<bool> LoadListAsync(int page, int totalRecords)
        {
            var url = "";

            if (totalRecords == 1000)
            {
                url = $"api/states/full";
            }
            else
            {
                url = $"api/states?page={page}&RecordsNumber={totalRecords}";
            }

            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"&filter={Filter}";
            }

            var responseHttp = await Repository.GetAsync<List<State>>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return false;
            }
            States = responseHttp.Response;
            return true;
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

        private async Task LoadPagesAsync()
        {
            var url = $"api/states/totalPages?id={CountryId}";
            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"&filter={Filter}";
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

        private async Task<bool> LoadStatesAsync(int page)
        {
            var url = $"api/states?id={CountryId}&page={page}";
            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"&filter={Filter}";
            }

            var responseHttp = await Repository.GetAsync<List<State>>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return false;
            }
            states = responseHttp.Response;
            return true;
        }

        private async Task CleanFilterAsync()
        {
            Filter = string.Empty;
            await ApplyStatesFilterAsync(Filter);
        }
   
        private async Task ApplyStatesFilterAsync(string value)
        {
            int page = 1;
            Filter = value;
            await LoadAsync(page);
            await SelectedPageAsync((page, Records));
        }

        private async Task<bool> LoadCountryAsync()
        {
            var responseHttp = await Repository.GetAsync<Country>($"/api/countries/{CountryId}");
            if (responseHttp.Error)
            {
                if (responseHttp.Httpresponsemessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/countries");
                    return false;
                }

                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return false;
            }
            country = responseHttp.Response;
            return true;
        }

        
        private async Task HandlePageSizeChanged(int value)
        {
            int page = 1;
            Records = value;
            await LoadAsync(page, Records);
        }

        private async Task ApplyFilterAsync(string value)
        {
            int page = 1;
            Filter = value;
            await LoadAsync(page);
            await SelectedPageAsync((page, Records));
        }

        private async Task DeleteAsync(State state)
        {
            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = $"¿Realmente deseas eliminar el departamento/estado? {state.Name}",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
                CancelButtonText = "No",
                ConfirmButtonText = "Si"
            });

            var confirm = string.IsNullOrEmpty(result.Value);
            if (confirm)
            {
                return;
            }

            var responseHttp = await Repository.DeleteAsync<State>($"/api/states/{state.Id}");
            if (responseHttp.Error)
            {
                if (responseHttp.Httpresponsemessage.StatusCode != HttpStatusCode.NotFound)
                {
                    var message = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                    return;
                }
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

