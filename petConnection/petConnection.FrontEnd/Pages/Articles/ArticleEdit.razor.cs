using System;
using System.Data;
using System.Net;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using petConnection.FrontEnd.Pages.Pets;
using petConnection.FrontEnd.Repositories;
using petConnection.Share.Entitties;

namespace petConnection.FrontEnd.Pages.Articles
{
    [Authorize(Roles = "Admin")]
    public partial class ArticleEdit
	{
		private ArticleForm articleForm;
		private Article? article;

        [Inject] private IRepository repository { get; set; } = null;

        [Inject] private SweetAlertService sweetAlertService { get; set; } = null;

        [Inject] private NavigationManager navigationManager { get; set; } = null;

        [EditorRequired, Parameter] public int Id { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var responseHttp = await repository.GetAsync<Article>($"/api/articles/{Id}");

            if (responseHttp.Error)
            {
                if (responseHttp.Httpresponsemessage.StatusCode == HttpStatusCode.NotFound)
                {
                    navigationManager.NavigateTo("/articles/");
                }
                else
                {
                    var message = await responseHttp.GetErrorMessageAsync();
                    await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                }
            }
            else
            {
                article = responseHttp.Response;
            }
        }

        private async Task EditArticleAsync()
        {
            var responseHttp = await repository.PutAsync("/api/articles", article);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }


            Return();

            var toast = sweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 300
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Cambios guardados con éxito");
        }


        private void Return()
        {
            articleForm!.FormPostedSuccessfully = true;
            navigationManager.NavigateTo("/articles");
        }
    }    
}

