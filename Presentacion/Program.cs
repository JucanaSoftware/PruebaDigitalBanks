var builder = WebApplication.CreateBuilder(args);

// Agregar servicios a la colección antes de Build()
builder.Services.AddRazorPages();

builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("https://localhost:7291/"); // URL de tu API
});

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.MapGet("/", context =>
{
    context.Response.Redirect("/Usuario");
    return Task.CompletedTask;
});


app.Run();
