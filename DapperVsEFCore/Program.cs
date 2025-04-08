using DapperVsEFCore.Entities;
using DapperVsEFCore.Persistence;
using DapperVsEFCore.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IStudentRepository, StudentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region EFCORE API
var studentEFGroup = app.MapGroup("/EfCore/students");

studentEFGroup.MapGet("", async (IStudentRepository studentRepository) =>
{
    var result = await studentRepository.EFGetAllAsync();
    return TypedResults.Ok(result);
});

studentEFGroup.MapGet("/{studentId}", async Task<Results<Ok<Student>, BadRequest<string>>> (int studentId, IStudentRepository studentRepository) =>
{
    try
    {
        var result = await studentRepository.EFReadByIdAsync(studentId);

        return TypedResults.Ok(result);
    }
    catch (Exception ex)
    {
        return TypedResults.BadRequest(ex.Message);
    }
});

studentEFGroup.MapPost("", async (Student student, IStudentRepository studentRepository) =>
{
    await studentRepository.EFCreateAsync(student);

    return TypedResults.Ok();
});

studentEFGroup.MapPut("", async Task<Results<Ok, BadRequest<string>>> (Student student, IStudentRepository studentRepository) =>
{
    try
    {
        await studentRepository.EFUpdateAsync(student);

        return TypedResults.Ok();
    }
    catch (Exception ex)
    {
        return TypedResults.BadRequest(ex.Message);
    }
});

studentEFGroup.MapDelete("/{studentId}", async Task<Results<Ok, BadRequest<string>>> (int studentId, IStudentRepository studentRepository) =>
{
    try
    {
        await studentRepository.EFDeleteAsync(studentId);

        return TypedResults.Ok();
    }
    catch (Exception ex)
    {
        return TypedResults.BadRequest(ex.Message);
    }
});

#endregion

#region Dapper API

var studentDapperGroup = app.MapGroup("/dapper/students");

studentDapperGroup.MapGet("", async (IStudentRepository studentRepository) =>
{
    var result = await studentRepository.DapperGetAllAsync();
    return TypedResults.Ok(result);
});

studentDapperGroup.MapGet("/{studentId}", async Task<Results<Ok<Student>, BadRequest<string>>> (int studentId, IStudentRepository studentRepository) =>
{
    try
    {
        var result = await studentRepository.DapperReadByIdAsync(studentId);

        return TypedResults.Ok(result);
    }
    catch (Exception ex)
    {
        return TypedResults.BadRequest(ex.Message);
    }
});

studentDapperGroup.MapPost("", async (Student student, IStudentRepository studentRepository) =>
{
    await studentRepository.DapperCreateAsync(student);

    return TypedResults.Ok();
});


studentDapperGroup.MapPut("", async Task<Results<Ok, BadRequest<string>>> (Student student, IStudentRepository studentRepository) =>
{
    try
    {
        await studentRepository.DapperUpdateAsync(student);

        return TypedResults.Ok();
    }
    catch (Exception ex)
    {
        return TypedResults.BadRequest(ex.Message);
    }
});

studentDapperGroup.MapDelete("/{studentId}", async Task<Results<Ok, BadRequest<string>>> (int studentId, IStudentRepository studentRepository) =>
{
    try
    {
        await studentRepository.DapperDeleteAsync(studentId);

        return TypedResults.Ok();
    }
    catch (Exception ex)
    {
        return TypedResults.BadRequest(ex.Message);
    }
});
#endregion


app.UseHttpsRedirection();

app.Run();
