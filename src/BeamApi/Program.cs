using BeamApi.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<AccountsService>();
builder.Services.AddScoped<SuppliersService>();
builder.Services.AddScoped<OrdersService>();
builder.Services.AddScoped<ExpensesService>();
builder.Services.AddScoped<OrderWorkflowService>();
builder.Services.AddScoped<RefundService>();
builder.Services.AddScoped<ContactService>();
builder.Services.AddScoped<OrderCancellationService>();
builder.Services.AddScoped<CaseService>();
builder.Services.AddScoped<UserDeactivationService>();
builder.Services.AddScoped<TicketClosureService>();
builder.Services.AddScoped<AuditService>();
builder.Services.AddScoped<SearchService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
