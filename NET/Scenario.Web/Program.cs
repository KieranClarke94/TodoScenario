using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddControllers();

builder.Services.AddHealthChecks();

builder.Services.AddMvcCore()
	  .ConfigureApiBehaviorOptions((x) =>
	  {
		  x.InvalidModelStateResponseFactory = (context) =>
		  {
			  // Copied from https://github.com/aspnet/Mvc/blob/8d66f104f7f2ca42ee8b21f75b0e2b3e1abe2e00/src/Microsoft.AspNetCore.Mvc.Core/DependencyInjection/ApiBehaviorOptionsSetup.cs#L134
			  // NOTE: TraceId omitted (undesirable, also uses internal classes so cannot include from original source)
			  var problemDetails = new ValidationProblemDetails(context.ModelState)
			  {
				  Status = StatusCodes.Status400BadRequest,
			  };

			  var result = new BadRequestObjectResult(problemDetails);

			  result.ContentTypes.Add("application/problem+json");
			  result.ContentTypes.Add("application/problem+xml");

			  return result;
		  };

	  })
	  .AddJsonOptions((x) => { 
		  x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
	  });

var app = builder.Build();

// global cors policy
app.UseCors(x => x
	.AllowAnyMethod()
	.AllowAnyHeader()
	.SetIsOriginAllowed(origin => true) // allow any origin
	.AllowCredentials()); // allow credentials

app.MapHealthChecks("/health");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
