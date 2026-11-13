using Microsoft.EntityFrameworkCore;
using TodoApi;

var builder = WebApplication.CreateBuilder(args);

//var connectionString = builder.Configuration.GetConnectionString("todos") ?? "Data Source=todos.db";

// DbContext regisztrálása SQLite használatával
//builder.Services.AddDbContext<TodoDb>(options => options.UseSqlite(connectionString));

builder.Services.AddDbContext<TodoDb>();

//builder.Services.AddSqlite<TodoDb>(connectionString);

var app = builder.Build();

app.MapGet("/todos", async (TodoDb db) => 
    await db.TodoItems
        .Include(t => t.Member)
        .Include(t => t.Place)
        .Select(todo => new
        {
            todo.Id,
            todo.Name,
            todo.IsCompleted,
            Member = todo.Member == null ? null : todo.Member.Name,
            Place = todo.Place == null ? null : todo.Place.Name
        })
        .ToListAsync());

app.MapGet("/todos/{id}", async (TodoDb db, int id) =>
{
    var todo = await db.TodoItems
        .Include(t => t.Member)
        .Include(t => t.Place)
        .FirstOrDefaultAsync(t => t.Id == id);

    if (todo is null) return Results.NotFound();

    return Results.Ok(new
    {
        todo.Id,
        todo.Name,
        todo.IsCompleted,
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

app.MapPost("/todos", async (TodoDb db, TodoItem todo) =>
{
    await db.TodoItems.AddAsync(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/todos/{todo.Id}", todo);
});

app.MapPut("/todos/{id}", async (TodoDb db, TodoItem updateTodo, int id) =>
{
    var todo = await db.TodoItems.FindAsync(id);

    if (todo is null) return Results.NotFound(); 
    todo.Name = updateTodo.Name ?? todo.Name;
    todo.IsCompleted = updateTodo.IsCompleted;
    if (updateTodo.Member is not null) todo.Member = await db.Members.FindAsync( updateTodo.Member.Id );
    if (updateTodo.Place is not null) todo.Place = await db.Places.FindAsync(updateTodo.Place.Id);

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/todos/{id}", async (TodoDb db, int id) =>
{
    var todo = await db.TodoItems.FindAsync(id);

    if (todo is null) return Results.NotFound();

    db.TodoItems.Remove(todo);

    await db.SaveChangesAsync();

    return Results.Ok();
});

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

app.MapGet("/members/{id}", async (TodoDb db, int id) =>
{
    var member = await db.Members
        .Include(m => m.TodoItems)
        .FirstOrDefaultAsync(m => m.Id == id);

    if (member is null) return Results.NotFound();

    return Results.Ok(new
    {
        member.Id,
        member.Name,
        TodoItems = member.TodoItems == null ? null : member.TodoItems
            .Select(todo => new
            {
                todo.Id,
                todo.Name
            }).ToList()
    });
});

app.MapPost("/members", async (TodoDb db, Member member) =>
{
    await db.Members.AddAsync(member);
    await db.SaveChangesAsync();
    return Results.Created($"/members/{member.Id}", member);
});

app.MapPut("/members/{id}", async (TodoDb db, Member updateMember, int id) =>
{
    var member = await db.Members.FindAsync(id);

    if (member is null) return Results.NotFound();

    member.Name = updateMember.Name ?? member.Name;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/members/{id}", async (TodoDb db, int id) =>
{
    var member = await db.Members.FindAsync(id);

    if (member is null) return Results.NotFound();

    db.Members.Remove(member);

    await db.SaveChangesAsync();

    return Results.Ok();
});

app.MapGet("/places", async (TodoDb db) =>
    await db.Places
    .Include(p => p.TodoItems)
    .Select(place => new {
        place.Id,
        place.Name,
        TodoItemsName = place.TodoItems == null ? null : place.TodoItems
            .Select(todo => todo.Name).ToList()
    })
    .ToListAsync()
);

app.MapGet("/places/{id}", async (TodoDb db, int id) =>
{
    var place = await db.Places.FindAsync(id);

    if (place is null) return Results.NotFound();

    return Results.Ok();
});

app.MapGet("/places/{id}/todos/member", async (TodoDb db, int id) =>
{
    var place = await db.Places
        .Include(p => p.TodoItems)
            .ThenInclude(t => t.Member)
        .FirstOrDefaultAsync(p => p.Id == id);

    if (place is null) return Results.NotFound();

    return Results.Ok(new
    {
        place.Id,
        place.Name,
        TodoItems = place.TodoItems == null ? null : place.TodoItems
            .Select(todo => new
            {
                todo.Id,
                todo.Name,
                Member = todo.Member == null ? null : new
                {
                    todo.Member.Id,
                    todo.Member.Name
                }
            }).ToList()
    });
});

app.MapPost("/places", async (TodoDb db, Place place) =>
{
    await db.Places.AddAsync(place);
    await db.SaveChangesAsync();
    return Results.Created($"/places/{place.Id}", place);
});

app.MapPut("/places/{id}", async (TodoDb db, Place UpdatePlace, int id) =>
{
    var place = await db.Places.FindAsync(id);

    if (place is null) return Results.NotFound();

    place.Name = UpdatePlace.Name;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/places/{id}", async (TodoDb db, int id) =>
{
    var place = await db.Places.FindAsync(id);

    if (place is null) return Results.NotFound();

    db.Places.Remove(place);

    await db.SaveChangesAsync();

    return Results.Ok();
});

app.Run();