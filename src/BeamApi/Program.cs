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
builder.Services.AddScoped<DiscountsService>();
builder.Services.AddScoped<LeaveService>();
builder.Services.AddScoped<InvoicesService>();
builder.Services.AddScoped<TicketStateService>();
builder.Services.AddScoped<EmployeeProfilesService>();
builder.Services.AddScoped<ProjectsService>();
builder.Services.AddScoped<ResourcesService>();
builder.Services.AddScoped<DocumentsService>();
builder.Services.AddScoped<ProductPricesService>();
builder.Services.AddScoped<CustomersService>();
builder.Services.AddScoped<PasswordResetService>();
builder.Services.AddScoped<PaymentCallbacksService>();


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