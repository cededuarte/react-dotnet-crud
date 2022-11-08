using aspnetserver.Data;
using Microsoft.OpenApi.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //CORS POLICY ALLOW REACT APP TO INTERACT WITH SERVER

        builder.Services.AddCors(options =>
        {
        options.AddPolicy("CORSPolicy",
            builder =>
            {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:3000", "https://appname.azurestaticapps.net");
            });
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(swaggerGenOptions =>
        {
            swaggerGenOptions.SwaggerDoc("v1", info: new OpenApiInfo { Title = "React .NET CRUD", Version = "v1" });
        

        });

        var app = builder.Build();
        app.UseSwagger();
        app.UseSwaggerUI(swaggerUIOptions =>
        {
            swaggerUIOptions.DocumentTitle = "React .NET CRUD";
            swaggerUIOptions.SwaggerEndpoint("swagger/v1/swagger.json", "React .NET CRUD");
            swaggerUIOptions.RoutePrefix = string.Empty;
        });
        app.UseHttpsRedirection();

        //TELLING ASP.NET WE WANT TO USE CORS/REQUEST PIPELINE
        app.UseCors("CORSPolicy");



        //CREATE API ENDPOINT
        app.MapGet("/get-all-posts", async () => await PostRepository.GetPostsAsync())
            .WithTags("Users Endpoints");

        app.MapGet("get-post by id/{postId}", async (int postId) =>
        {
            Post postToReturn = await PostRepository.GetPostByIdAsync(postId);

            if (postToReturn != null)
            {
                return Results.Ok(postToReturn);
            }
            else
            {
                return Results.BadRequest();
            }
        }).WithTags("Users Endpoints");

        app.MapPost("/create-post", async (Post postToCreate) =>
        {
            bool createSuccessful = await PostRepository.CreatePostAsync(postToCreate);

            if (createSuccessful)
            {
                return Results.Ok("Created Successfully");
            }
            else
            {
                return Results.BadRequest();
            }
        }).WithTags("Users Endpoints");


        app.MapPut("/update-post", async (Post postToUpdate) =>
        {
            bool updateSuccessful = await PostRepository.UpdatePostAsync(postToUpdate);

            if (updateSuccessful)
            {
                return Results.Ok("Updated Successfully");
            }
            else
            {
                return Results.BadRequest();
            }
        }).WithTags("Users Endpoints");

        app.MapDelete("/delete-post-by-id/{postId}", async (int postId) =>
        {
            bool deleteSuccessful = await PostRepository.DeletePostAsync(postId);

            if (deleteSuccessful)
            {
                return Results.Ok("Deleted Successfully");
            }
            else
            {
                return Results.BadRequest();
            }
        }).WithTags("Users Endpoints");


        app.Run();
    }
}