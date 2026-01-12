namespace AutenticacaoDotNet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication("MeuCookieAuthenticacao")
    .AddCookie("MeuCookieAuthenticacao", options =>
    {
        options.Cookie.Name = "MeuCookieAuthenticacao";
        options.ExpireTimeSpan = TimeSpan.FromSeconds(200);
        options.LoginPath = "/Account/Login";
    });

            builder.Services.AddAuthorization(options =>
            {
                //options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));
                //options.AddPolicy("MustBelongToHRDepartment", policy => policy.RequireClaim("Department", "HR"));
                //options.AddPolicy("HRManagerOnly", policy => policy
                //    .RequireClaim("Department", "HR")
                //    .RequireClaim("Manager")
                //    .Requirements.Add(new HRManagerProbationRequirement(3)));
                options.AddPolicy("RH", policy => policy.RequireRole("RH"));
                options.AddPolicy("Admin1", policy => policy.RequireRole("Admin1"));
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
