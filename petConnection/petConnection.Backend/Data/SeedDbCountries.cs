    using System;
    using petConnection.Share.Entitties;

    namespace petConnection.Backend.Data
    {
	    public class SeedDbCountries
	    {
            private readonly DataContext _context;

            public SeedDbCountries(DataContext context)
            {
                _context = context;
        }

            public async Task SeedAsync()
            {
                await _context.Database.EnsureCreatedAsync();
                await CheckCountriesAsync();            
            }

            private async Task CheckCountriesAsync()
            {
                if (!_context.Countries.Any())
                {
                    // Existing countries
                    _context.Countries.Add(new Country
                    {
                        Name = "Colombia",
                        States = new List<State>()
                {
                    new State()
                    {
                        Name = "Antioquia",
                        Cities = new List<City>()
                        {
                            new City() { Name = "Medellín" },
                            new City() { Name = "Intagui" },
                            new City() { Name = "Envigado" },
                            new City() { Name = "Bello" },
                            new City() { Name = "Rionegro" }
                        }
                    },
                    new State()
                    {
                        Name = "Bogotá",
                        Cities = new List<City>()
                        {
                            new City() { Name = "Usaquen" },
                            new City() { Name = "Chapinero" },
                            new City() { Name = "Santa fe" },
                            new City() { Name = "Usme" },
                            new City() { Name = "Bosa" }
                        }
                    }
                }
                    });

                    _context.Countries.Add(new Country
                    {
                        Name = "Estados Unidos",
                        States = new List<State>()
                {
                    new State()
                    {
                        Name = "Florida",
                        Cities = new List<City>()
                        {
                            new City() { Name = "Orlando" },
                            new City() { Name = "Miami" },
                            new City() { Name = "Tampa" },
                            new City() { Name = "Fort Lauderdale" },
                            new City() { Name = "Key West" }
                        }
                    },
                    new State()
                    {
                        Name = "Texas",
                        Cities = new List<City>()
                        {
                            new City() { Name = "Houston" },
                            new City() { Name = "San Antonio" },
                            new City() { Name = "Dallas" },
                            new City() { Name = "Austin" },
                            new City() { Name = "El Paso" }
                        }
                    }
                }
                    });

                    // Add 13 more countries
                    _context.Countries.Add(new Country
                    {
                        Name = "Germany",
                        States = new List<State>()
                {
                    new State()
                    {
                        Name = "Bavaria",
                        Cities = new List<City>()
                        {
                            new City() { Name = "Munich" },
                            new City() { Name = "Nuremberg" },
                            new City() { Name = "Augsburg" },
                            new City() { Name = "Regensburg" },
                            new City() { Name = "Ingolstadt" }
                        }
                    },
                    new State()
                    {
                        Name = "Berlin",
                        Cities = new List<City>()
                        {
                            new City() { Name = "Mitte" },
                            new City() { Name = "Charlottenburg" },
                            new City() { Name = "Kreuzberg" },
                            new City() { Name = "Prenzlauer Berg" },
                            new City() { Name = "Neukölln" }
                        }
                    }
                }
                    });

                    _context.Countries.Add(new Country
                    {
                        Name = "Japan",
                        States = new List<State>()
                {
                    new State()
                    {
                        Name = "Tokyo",
                        Cities = new List<City>()
                        {
                            new City() { Name = "Shinjuku" },
                            new City() { Name = "Shibuya" },
                            new City() { Name = "Chiyoda" },
                            new City() { Name = "Taito" },
                            new City() { Name = "Minato" }
                        }
                    },
                    new State()
                    {
                        Name = "Osaka",
                        Cities = new List<City>()
                        {
                            new City() { Name = "Chuo" },
                            new City() { Name = "Naniwa" },
                            new City() { Name = "Tennoji" },
                            new City() { Name = "Yodogawa" },
                            new City() { Name = "Higashisumiyoshi" }
                        }
                    }
                }
                    });

                    _context.Countries.Add(new Country
                    {
                        Name = "France",
                        States = new List<State>()
                {
                    new State()
                    {
                        Name = "Île-de-France",
                        Cities = new List<City>()
                        {
                            new City() { Name = "Paris" },
                            new City() { Name = "Versailles" },
                            new City() { Name = "Boulogne-Billancourt" },
                            new City() { Name = "Saint-Denis" },
                            new City() { Name = "Argenteuil" }
                        }
                    },
                    new State()
                    {
                        Name = "Provence-Alpes-Côte d'Azur",
                        Cities = new List<City>()
                        {
                            new City() { Name = "Marseille" },
                            new City() { Name = "Nice" },
                            new City() { Name = "Toulon" },
                            new City() { Name = "Aix-en-Provence" },
                            new City() { Name = "Avignon" }
                        }
                    }
                }
                    });

                    _context.Countries.Add(new Country
                    {
                        Name = "United Kingdom",
                        States = new List<State>()
                {
                    new State()
                    {
                        Name = "England",
                        Cities = new List<City>()
                        {
                            new City() { Name = "London" },
                            new City() { Name = "Manchester" },
                            new City() { Name = "Birmingham" },
                            new City() { Name = "Liverpool" },
                            new City() { Name = "Bristol" }
                        }
                    },
                    new State()
                    {
                        Name = "Scotland",
                        Cities = new List<City>()
                        {
                            new City() { Name = "Edinburgh" },
                            new City() { Name = "Glasgow" },
                            new City() { Name = "Aberdeen" },
                            new City() { Name = "Dundee" },
                            new City() { Name = "Inverness" }
                        }
                    }
                }
                    });

                    _context.Countries.Add(new Country
                    {
                        Name = "Italy",
                        States = new List<State>()
                {
                    new State()
                    {
                        Name = "Lombardy",
                        Cities = new List<City>()
                        {
                            new City() { Name = "Milan" },
                            new City() { Name = "Bergamo" },
                            new City() { Name = "Brescia" },
                            new City() { Name = "Como" },
                            new City() { Name = "Varese" }
                        }
                    },
                    new State()
                    {
                        Name = "Lazio",
                        Cities = new List<City>()
                        {
                            new City() { Name = "Rome" },
                            new City() { Name = "Latina" },
                            new City() { Name = "Frosinone" },
                            new City() { Name = "Viterbo" },
                            new City() { Name = "Rieti" }
                        }
                    }
                }
                    });

                    _context.Countries.Add(new Country
                    {
                        Name = "Canada",
                        States = new List<State>()
                {
                    new State()
                    {
                        Name = "Ontario",
                        Cities = new List<City>()
                        {
                            new City() { Name = "Toronto" },
                            new City() { Name = "Ottawa" },
                            new City() { Name = "Mississauga" },
                            new City() { Name = "Hamilton" },
                            new City() { Name = "London" }
                        }
                    },
                    new State()
                    {
                        Name = "Quebec",
                        Cities = new List<City>()
                        {
                            new City() { Name = "Montreal" },
                            new City() { Name = "Quebec City" },
                            new City() { Name = "Laval" },
                            new City() { Name = "Gatineau" },
                            new City() { Name = "Longueuil" }
                        }
                    }
                }
                    });

                    _context.Countries.Add(new Country
                    {
                        Name = "Brazil",
                        States = new List<State>()
                {
                    new State()
                    {
                        Name = "São Paulo",
                        Cities = new List<City>()
                        {
                            new City() { Name = "São Paulo" },
                            new City() { Name = "Guarulhos" },
                            new City() { Name = "Campinas" },
                            new City() { Name = "São Bernardo do Campo" },
                            new City() { Name = "Santo André" }
                        }
                    },
                    new State()
                    {
                        Name = "Rio de Janeiro",
                        Cities = new List<City>()
                        {
                            new City() { Name = "Rio de Janeiro" },
                            new City() { Name = "São Gonçalo" },
                            new City() { Name = "Duque de Caxias" },
                            new City() { Name = "Nova Iguaçu" },
                            new City() { Name = "Niterói" }
                        }
                    }
                }
                    });

                    _context.Countries.Add(new Country
                    {
                        Name = "China",
                        States = new List<State>()
                {
                    new State()
                    {
                        Name = "Beijing",
                        Cities = new List<City>()
                        {
                            new City() { Name = "Dongcheng" },
                            new City() { Name = "Xicheng" },
                            new City() { Name = "Chaoyang" },
                            new City() { Name = "Haidian" },
                            new City() { Name = "Fengtai" }
                        }
                    },
                    new State()
                    {
                        Name = "Shanghai",
                        Cities = new List<City>()
                        {
                            new City() { Name = "Huangpu" },
                            new City() { Name = "Xuhui" },
                            new City() { Name = "Changning" },
                            new City() { Name = "Jing'an" },
                            new City() { Name = "Putuo" }
                        }
                    }
                }
                    });

                    _context.Countries.Add(new Country
                    {
                        Name = "India",
                        States = new List<State>()
                {
                    new State()
                    {
                        Name = "Maharashtra",
                        Cities = new List<City>()
                        {
                            new City() { Name = "Mumbai" },
                            new City() { Name = "Pune" },
                            new City() { Name = "Nagpur" },
                            new City() { Name = "Nashik" },
                            new City() { Name = "Thane" }
                        }
                    },
                    new State()
                    {
                        Name = "Uttar Pradesh",
                        Cities = new List<City>()
                        {
                            new City() { Name = "Lucknow" },
                            new City() { Name = "Kanpur" },
                            new City() { Name = "Agra" },
                            new City() { Name = "Varanasi" },
                            new City() { Name = "Allahabad" }
                        }
                    }
                }
                    });

                    _context.Countries.Add(new Country
                    {
                        Name = "Australia",
                        States = new List<State>()
                {
                        new State()
                    {
                        Name = "New South Wales",
                        Cities = new List<City>()
                        {
                            new City() { Name = "Sydney" },
                            new City() { Name = "Newcastle" },
                            new City() { Name = "Central Coast" },
                            new City() { Name = "Wollongong" },
                            new City() { Name = "Maitland" }
                        }
                    },
                    new State()
                    {
                        Name = "Victoria",
                        Cities = new List<City>()
                        {
                            new City() { Name = "Melbourne" },
                            new City() { Name = "Geelong" },
                            new City() { Name = "Ballarat" },
                            new City() { Name = "Bendigo" },
                            new City() { Name = "Shepparton" }
                        }
                    }
                }
                    });

                    _context.Countries.Add(new Country
                    {
                        Name = "Russia",
                        States = new List<State>()
                {
                    new State()
                    {
                        Name = "Moscow Oblast",
                        Cities = new List<City>()
                        {
                            new City() { Name = "Moscow" },
                            new City() { Name = "Krasnogorsk" },                            
                            new City() { Name = "Mytishchi" },
                            new City() { Name = "Khimki" }
                        }
                    },
                    new State()
                    {
                        Name = "Saint Petersburg",
                        Cities = new List<City>()
                        {
                            new City() { Name = "Saint Petersburg" },
                            new City() { Name = "Kolpino" },
                            new City() { Name = "Pushkin" },
                            new City() { Name = "Peterhof" },
                            new City() { Name = "Kronstadt" }
                        }
                    }
                }
                    });

                    _context.Countries.Add(new Country
                    {
                        Name = "South Africa",
                        States = new List<State>()
                {
                    new State()
                    {
                        Name = "Gauteng",
                        Cities = new List<City>()
                        {
                            new City() { Name = "Johannesburg" },
                            new City() { Name = "Pretoria" },
                            new City() { Name = "Ekurhuleni" },
                            new City() { Name = "Soweto" },
                            new City() { Name = "Vereeniging" }
                        }
                    },
                    new State()
                    {
                        Name = "Western Cape",
                        Cities = new List<City>()
                        {
                            new City() { Name = "Cape Town" },
                            new City() { Name = "Stellenbosch" },
                            new City() { Name = "Paarl" },
                            new City() { Name = "George" },
                            new City() { Name = "Mossel Bay" }
                        }
                    }
                }
                    });

                    // Save changes to database
                    await _context.SaveChangesAsync();
                }
            }

        }
    }