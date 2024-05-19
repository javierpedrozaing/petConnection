using System;
namespace petConnection.FrontEnd.Services
{
	public interface ILoginService
	{
        Task LoginAsync(string token);

        Task LogoutAsync();
    }
}

