using System;
using Microsoft.AspNetCore.Components;

namespace petConnection.FrontEnd.Shared
{
    public partial class PetProfile<TModel>
    {
        [Parameter]
        public TModel Model { get; set; }

        private string GetName()
        {            
            return Model.GetType().GetProperty("Name").GetValue(Model).ToString();
        }

        private string GetSpecie()
        {            
            return Model.GetType().GetProperty("Specie").GetValue(Model).ToString();
        }

        private int GetAge()
        {         
            return (int)Model.GetType().GetProperty("Age").GetValue(Model);
        }

        private string GetRace()
        {         
            return Model.GetType().GetProperty("Race").GetValue(Model).ToString();
        }

        private string? GetGender()
        {         
            return Model.GetType().GetProperty("Gender").GetValue(Model)?.ToString();
        }

        private string? GetSize()
        {
            return Model.GetType().GetProperty("Size").GetValue(Model)?.ToString();
        }

        private string? GetWeight()
        {
            return Model.GetType().GetProperty("Weight").GetValue(Model)?.ToString();
        }

        private string? GetColor()
        {
            return Model.GetType().GetProperty("Color").GetValue(Model)?.ToString();
        }

        private string? GetHealthCondition()
        {
            return Model.GetType().GetProperty("HealthCondition").GetValue(Model)?.ToString();
        }

        private string? GetBehavior()
        {
            return Model.GetType().GetProperty("Behavior").GetValue(Model)?.ToString();
        }
    }
}

