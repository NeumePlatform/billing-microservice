global using service_billing.Models;
global using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using service_billing.services.TransactionService;
using service_billing.Data;
using service_billing.services.MessageProducer;
using MassTransit;
using MassTransit.AspNetCoreIntegration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using service_billing;
using TransactionModel;

var builder = WebApplication.CreateBuilder(args);

//var bus = Bus.Factory.CreateUsingRabbitMq(config =>
//{
//    config.Host("amqp://guest:guest@localhost:5672");

//    config.ReceiveEndpoint("subTransactions", c =>
//    {
//        c.Handler<TransactionModel.Transaction>(ctx =>
//        {
//            return Console.Out.WriteLineAsync(ctx.Message.id.ToString());
//        });
//    });
//});

//bus.Start();

//bus.Publish(new TransactionModel.Transaction { id = 1, customerID = "2", });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddDbContext<DataContext>();
builder.Services.AddMassTransit(config =>
{
    //config.UsingRabbitMq((ctx, cfg) =>
    //{
    //    cfg.Host("amqp://guest:guest@localhost:5672");


    //});

    config.UsingAzureServiceBus((ctx, cfg) =>
    {
        cfg.Host("Endpoint=sb://neumeplatform.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=pf2GckyPcbONdiBWK80Vw1OqCDMsvgk/5+ASbKLV//w=");
    });
});

builder.Services.AddScoped<IMessageProducer, MessageProducer>();


var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = domain;
        options.Audience = builder.Configuration["Auth0:Audience"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = ClaimTypes.NameIdentifier
        };
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("write:transaction", policy => policy.Requirements.Add(new HasScopeRequirement("write:transaction", domain)));
});

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000/")
                .AllowAnyMethod().AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(MyAllowSpecificOrigins);

app.MapControllers();

app.Run();

