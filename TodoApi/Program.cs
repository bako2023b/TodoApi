
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TodoApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoDb>();

var app = builder.Build();

app.MapGet("/todos", async (TodoDb db) =>
    await db.TodoItems
        .Include(t => t.Member)
        .Include(t => t.Place)
        .Select(todo => new
        {
            todo.Id,
            todo.Name,
            todo.IsComplete,
            Member = todo.Member == null ? null : todo.Member.Name,
            Place = todo.Place == null ? null : todo.Place.Name,
        })
        .ToListAsync());

app.MapGet("/members", async (TodoDb db) =>
    await db.Members
        .Include(m => m.TodoItems)
        .Select(member => new
        {
            member.Id,
            member.Name,
            TodoItemsName = member.TodoItems == null ? null : member.TodoItems
            .Select(todo => todo.Name).ToList()
        })
        .ToListAsync());

app.MapGet("/Places", async (TodoDb db) =>
    await db.Places
        .Include(p => p.TodoItems)
        .Select(place => new
        {
            place.Id,
            place.Name,
            TodoItemsName = place.TodoItems == null ? null : place.TodoItems
            .Select (todo => todo.Name).ToList()
        })
        .ToListAsync());
/*
app.MapPost("/todos", async (TodoDb db, TodoItem todo) =>
{
    todo.Member = todo.Member == null ? null : await db.Members.FindAsync(todo.Member.Id);
    todo.Place = todo.Place == null ? null : await db.Places.FindAsync(todo.Place.Id);

    await db.TodoItems.AddAsync(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/todos/{todo.Id}", new
    {
        todo.Id,
        todo.Name,
        todo.IsComplete,
        Member = todo.Member == null ? null : new
        {
            todo.Member.Id,
            todo.Member.Name
        },
        Place = todo.Place == null ? null : new
        {
            todo.Place.Id,
            todo.Place.Name
        }
    });
});
*/

app.MapPost("/todos", async (TodoDb db, TodoSaveDto dto) =>
{
    var todo = new TodoItem
    {
        Name = dto.Name,
        IsComplete = dto.IsComplete,
        MemberId = dto.Member?.Id,
        PlaceId = dto.Place?.Id
    };

    db.TodoItems.Add(todo);
    await db.SaveChangesAsync();

    // Ha vissza akarod adni a részletes választ a kliensnek:
    var createdTodo = await db.TodoItems
        .Include(t => t.Member)
        .Include(t => t.Place)
        .FirstAsync(t => t.Id == todo.Id);

    return Results.Created($"/todos/{createdTodo.Id}", new
    {
        createdTodo.Id,
        createdTodo.Name,
        createdTodo.IsComplete,
        Member = createdTodo.Member == null ? null : new { createdTodo.Member.Id, createdTodo.Member.Name },
        Place = createdTodo.Place == null ? null : new { createdTodo.Place.Id, createdTodo.Place.Name }
    });
});

app.MapPost("/members", async (TodoDb db, Member member) =>
{
    await db.Members.AddAsync(member);
    await db.SaveChangesAsync();
    return Results.Created($"/members/{member.Id}", member);
});

app.MapPost("/places", async (TodoDb db, Place place) =>
{
    await db.Places.AddAsync(place);
    await db.SaveChangesAsync();
    return Results.Created($"/places/{place.Id}", place);
});

app.MapPut("/todos/{id}", async (TodoDb db, TodoItem updateTodo, int id) =>
{
    var todo = await db.TodoItems.FindAsync(id);

    if (todo == null) return Results.NotFound();

    todo.Name = updateTodo.Name ?? todo.Name;
    todo.IsComplete = updateTodo.IsComplete;
    if (updateTodo.Member is not null) todo.Member = await db.Members.FindAsync(updateTodo.Member.Id);
    if (updateTodo.Place is not null) todo.Place = await db.Places.FindAsync(updateTodo.Place.Id);

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/todos/{id}", async (TodoDb db, int id) =>
{
    var todo = await db.TodoItems.FindAsync(id);
    if(todo is null)
    {
        return Results.NotFound();
    }
    db.TodoItems.Remove(todo);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.MapDelete("/members/{id}", async (TodoDb db, int id) =>
{
    var member = await db.Members.FindAsync(id);
    if (member is null)
    {
        return Results.NotFound();
    }
    db.Members.Remove(member);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.MapDelete("/places/{id}", async (TodoDb db, int id) =>
{
    var place = await db.Places.FindAsync(id);
    if (place is null)
    {
        return Results.NotFound();
    }
    db.Places.Remove(place);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.Run();
