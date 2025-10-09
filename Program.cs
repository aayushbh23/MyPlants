using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using PlantsCatalog.Models;
using PlantsCatalog.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("dbConfig.json", optional: false, reloadOnChange: true);

var provider = builder.Configuration["Database:Provider"]?.Trim() ?? "SqlServer";
builder.Services.AddDbContext<PlantsCatalogDBContext>(options =>
{
    if (provider.Equals("Sqlite", StringComparison.OrdinalIgnoreCase))
        options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
    else{
        var username = builder.Configuration["DbCredentials:Username"];
        var password = builder.Configuration["DbCredentials:Password"];
        var baseConn = builder.Configuration.GetConnectionString("SqlServer");

        if (string.IsNullOrWhiteSpace(baseConn))
            throw new InvalidOperationException("ConnectionStrings:SqlServer is missing in appsettings.json.");

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            throw new InvalidOperationException("DbCredentials:Username or Password missing in dbConfig.json.");

        var finalConn = baseConn
            .Replace("{USERNAME}", username, StringComparison.Ordinal)
            .Replace("{PASSWORD}", password, StringComparison.Ordinal);

        options.UseSqlServer(finalConn);
    }
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpsRedirection(o => o.HttpsPort = 5001);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ICartService, SessionCartService>();
builder.Services.AddScoped<IPlantService, EfPlantService>();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseExceptionHandler("/Error/500");
app.UseStatusCodePagesWithReExecute("/Error/{0}");
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PlantsCatalogDBContext>();

    if (provider.Equals("Sqlite", StringComparison.OrdinalIgnoreCase))
    {
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }
    else
    {
        for (int i = 0; i < 5; i++)
        {
            try {
                db.Database.Migrate(); break;
            }catch (Microsoft.Data.SqlClient.SqlException) when (i < 4){
                await Task.Delay(TimeSpan.FromSeconds(2));
            }
        }
    }

    if (!db.Plants.Any())
    {
        db.Plants.AddRange(

            new Plant { Name = "Monstera Deliciosa",
                        Category = "Indoor",
                        Price = 13.99M,
                        Description = "Iconic split leaves. Easy indoor plant.",
                        ImageFile = "monstera.png"
                    },

            new Plant { Name = "Snake Plant (Sansevieria)",
                        Category = "Succulent",
                        Price = 19.99M,
                        Description = "Hardy, low-light tolerant.",
                        ImageFile = "snake.png"
                    },

            new Plant { Name = "Fiddle Leaf Fig",
                        Category = "Succulent",
                        Price = 39.50M,
                        Description = "Statement plant with violin-shaped leaves.",
                        ImageFile = "fiddleleaf.png"
                    },

            new Plant { Name = "ZZ Plant (Zamioculcas)",
                        Category = "Outdoor",
                        Price = 18.50M,
                        Description = "Glossy leaves, drought tolerant.",
                        ImageFile = "zzplant.png"
                    },

            new Plant { Name = "Pothos Golden",
                        Category = "Indoor",
                        Price = 29.99M,
                        Description = "Fast-growing trailing plant.",
                        ImageFile = "pothos.png"
                    },

            new Plant { Name = "Peace Lily",
                        Category = "Indoor",
                        Price = 22.49M,
                        Description = "Elegant white blooms, air-purifying.",
                        ImageFile = "peacelily.png"
                    },

            new Plant { Name = "Spider Plant",
                        Category = "Indoor",
                        Price = 13,
                        Description = "Easy-care, produces baby spiderettes.",
                        ImageFile = "spider.png"
                    },

            new Plant { Name = "Rubber Plant",
                        Category = "Outdoor",
                        Price = 28,
                        Description = "Thick glossy leaves, bright light.",
                        ImageFile = "rubber.png"
                    },

            new Plant { Name = "Aloe Vera",
                        Category = "Succulent",
                        Price = 16,
                        Description = "Succulent with soothing gel.",
                        ImageFile = "aloe.png"
                    },

            new Plant { Name = "Bird of Paradise",
                        Category = "Outdoor",
                        Price = 45,
                        Description = "Tropical look, large leaves.",
                        ImageFile = "birdofparadise.png"
                    },

            new Plant { Name = "Calathea Orbifolia",
                        Category = "Succulent",
                        Price = 35,
                        Description = "Striking patterns, prefers humidity.",
                        ImageFile = "calathea.png"
                    },

            new Plant { Name = "Chinese Money Plant",
                        Category = "Indoor",
                        Price = 19,
                        Description = "Round leaves, compact growth.",
                        ImageFile = "moneyplant.png"
                    },

            new Plant { Name = "Boston Fern",
                        Category = "Outdoor",
                        Price = 31,
                        Description = "Soft green fronds, ideal for hanging baskets.",
                        ImageFile = "bostonfern.png"
                    },

            new Plant { Name = "Lavender",
                        Category = "Flowering",
                        Price = 105,
                        Description = "Fragrant purple flowers, attracts pollinators.",
                        ImageFile = "lavender.png"
                    },

            new Plant { Name = "Cactus",
                        Category = "Succulent",
                        Price = 12,
                        Description = "Set of small cacti in assorted shapes and sizes.",
                        ImageFile = "cactus.png"
                    },

            new Plant { Name = "Rose Bush",
                        Category = "Flowering",
                        Price = 115,
                        Description = "Classic red roses that bloom through summer.",
                        ImageFile = "rosebush.png"
                    },

            new Plant { Name = "Basil Herb",
                        Category = "Herb",
                        Price = 8,
                        Description = "Fresh basil leaves perfect for cooking.",
                        ImageFile = "basilherb.png"
                    },

            new Plant { Name = "Mint Herb",
                        Category = "Herb",
                        Price = 5,
                        Description = "Refreshing aroma, great for tea and cocktails.",
                        ImageFile = "mintherb.png"
                    }

             );
        db.SaveChanges();
    }
}

var defaultCulture = new CultureInfo("en-AU");
CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

app.Run();
