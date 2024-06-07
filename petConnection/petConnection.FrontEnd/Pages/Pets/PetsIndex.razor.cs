using System;
using System.Diagnostics.Metrics;
using System.Net;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using petConnection.FrontEnd.Repositories;
using petConnection.Share.DTOs;
using petConnection.Share.Entitties;

namespace petConnection.FrontEnd.Pages.Pets
{
	public partial class PetsIndex
	{
        private int currentPage = 1;
        private int totalPages;
        private int counter = 0;
        private bool isAuthenticated;


        [Inject] private IRepository Repository { get; set; } = null!;        

        private List<Pet> Pets { get; set; }

        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        [Inject] private NavigationManager navigationManager { get; set; } = null!; // framework component

        [Parameter, SupplyParameterFromQuery] public string Page { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;

        [Parameter, SupplyParameterFromQuery] public int Records { get; set; } = 10;

        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; } = null!;
        

        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        protected override async Task OnParametersSetAsync()
        {
            await CheckIfUserAuthenticated();
            await LoadCounterAsync();
        }


        private async Task CheckIfUserAuthenticated()
        {
            var authenticationState = await authenticationStateTask;
            isAuthenticated = true; // update this line
        }


        private async Task LoadAsync()
        {
            var responseHttp = await Repository.GetAsync<List<Pet>>("api/pets");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            Pets = responseHttp.Response;
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
                url = $"api/pets/full";
            }
            else
            {
                url = $"api/pets?page={page}&RecordsNumber={totalRecords}";
            }

            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"&filter={Filter}";
            }

            var responseHttp = await Repository.GetAsync<List<Pet>>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return false;
            }
            Pets = responseHttp.Response;
            return true;
        }

        private async Task LoadPagesAsync()
        {
            var url = "api/pets/totalPages";
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
            await ApplyFilterAsync(Filter);
        }

        private async Task ApplyFilterAsync(string value)
        {
            int page = 1;
            Filter = value;
            await LoadAsync(page);
            await SelectedPageAsync((page, Records));
        }

        private async Task HandlePageSizeChanged(int value)
        {
            int page = 1;
            Records = value;
            await LoadAsync(page, Records);
        }

        private async Task DeleteAsync(Pet pet)
        {
            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = $"¿Estas seguro de querer borrar esta mascota {pet.Name} ?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true
            });

            var confirm = string.IsNullOrEmpty(result.Value);
            if (confirm)
            {
                return;
            }

            var responseHttp = await Repository.DeleteAsync<Pet>($"api/pets/{pet.Id}");

            if (responseHttp.Error)
            {
                if (responseHttp.Httpresponsemessage.StatusCode == HttpStatusCode.NotFound)
                {
                    navigationManager.NavigateTo("/pets");
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
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Mascota borrada con éxito");
        }

        private async void AddAdoptionAsync(int petId)
        {
            if (!isAuthenticated)
            {
                return; // update it to use modal
            }

            var adoptionDTO = new AdoptionDTO
            {
                PetId = petId
            };

            var httpResponse = await Repository.PostAsync("/api/adoptions/newAdoption", adoptionDTO);
            if (httpResponse.Error)
            {
                var message = await httpResponse.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            await LoadCounterAsync();

            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Adopción creada exitosamente");
        }


        private async Task LoadCounterAsync()
        {
            if (!isAuthenticated)
            {
                return;
            }

            var responseHttp = await Repository.GetAsync<int>("api/adoptions/count");

            if (responseHttp.Error)
            {
                return;
            }

            counter = responseHttp.Response;

        }
    }

    
}

