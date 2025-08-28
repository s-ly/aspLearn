var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/test", () => "Test - OK");
app.MapGet("/obj2", () => 42);
app.MapGet("/obj", () => new Patient("Tony"));
app.MapGet("/obj3", () => new Person("Nika")); // не отображается (нет свойств)
app.MapGet("/obj4", () => new Human("Obyvan"));
app.MapGet("/obj5", () => new PatientStruct("Grom"));
app.MapGet("/obj6", () => new List<string> { "q", "w", "e" }); // возвращает массив

app.Run();

// неизменяемый
// immutable DTO (объектов передачи данных)
// как класс, только конструктор и свойства создаются автоматом.
public record Patient(string name);

// неподходет, так-как нет свойств
public class Person {
    public string name;
    public Person(string n) {
        name = n;
    }
}

// работает, но пришлось писать конструктор и свойство вручную
public class Human {
    public string Name { get; set; }
    public Human(string name) {
        Name = name;
    }
}

// структура тоже работает (но изменяемая и есть особенности, копирование при передачи).
public struct PatientStruct {
    public string Name { get; set; }
    public PatientStruct(string name) {
        Name = name;
    }
}
