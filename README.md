# Your "Clean Architecture" is still layered!

Often developers, when talking about architecture, actually think about their project structure. Hexagonal, Clean, Ports & Adapters, Onion... all of these are referring on how to organize your project. You either "inherit" it from your team or try some that makes more sense to you on new project or when refactoring. Thing is that project organization is important when you have a monolith or some coarse grained services that contain several functionalities that are somewhat related, but you want to keep them separate on code level and not to jump into making a (micro)service for each.

You usually end up with a solution that has several projects:

- API - just for controllers and to expose business logic
- Domain/Core - with business logic
- Infrastructure - interface implementation
- Persistence? - some folks like to keep DB stuff separated

So what is wrong with this structure? Listen to this talk by Simon Brown and continue reading to grab some code samples.

[![Modular Monoliths • Simon Brown](https://img.youtube.com/vi/5OjqD-ow8GE/0.jpg)](https://www.youtube.com/watch?v=5OjqD-ow8GE)

## Your code doesn't match your diagram

Let's start by having an API that has two functionalities:

- Weather Forecast
- Calculator

How would you draw a component diagram? Something like this, right?

![Diagram](https://dev-to-uploads.s3.amazonaws.com/i/2co4nbwuw8iq1fn4c6vo.png)

How would you structure your code? I guess something like this:

- API (project)
- Domain (project)
  - WeatherForecast (folder)
  - Calculator (folder)
- Infrastructure
  - WeatherForecast (folder)
  - Calculator (folder)

There is an obvious mismatch, right? You might say - that doesn't matter, we have a nice folder structure so we keep things separated and well organized.

OK, but what is stopping something from `WeatherForecast` folder to call something else from `Calculator` folder? Internal conventions and rules? Pull requests? Yes, all of that might help, but it would be more obvious if in that pull request you would see that unwanted relation trying to creeping in.

## Then how to structure?

A suggestion is that it matches you component diagram, like this:

- API (project)
- WeatherForecast (project)
  - ...
- Calculator (project)
  - ...

I deliberately put three dots, since it is up to developer of the specific functionality how to organize code inside of it. Should you have Domain/Infrastructure/whatever folder structure in each project? Or you will have some convention that if functionality is simple and you have just few classes, you do not even want to have any sub-folder? Whatever your teams' convention is, that is fine.

Some obvious advantages are:

- Solution structure matches diagram, so it is obvious where the code is.
- All code related to one functionality is in one project, making it much harder to make some dangerous dependencies between different functionalities.

## Encapsulate your component

We all know what is encapsulation, but we often think of it just in the terms of class encapsulation. Simon Brown suggested we do the encapsulation on component level too. If you think of your REST API, it is encapsulated as a consumer can only call exposed methods in a way you specified. Why not do the same for your component?

You might say you already do that by having interfaces that are then injected, so a caller can't access the implementation, but that is more often not true. Usually we have that API/Core/Implementation project structure and all our classes are `public`. We need them to be `public` as DI setup is usually in API project and they need to be visible.

With the new project-per-component structure, we do not have that problem any more. Implementation classes can be `private` or `internal` and therefore hidden from other components. Yes, you would have your DI configuration inside the component!

## Sample solution

Grab a look at the sample solution on GutHub {% github nikolic-bojan/modularity %}

Structure of my project is:

- API (project)
- Components (solution folder to group components)
  - WeatherForecast (project)
  - Calculator (project)

Things that are exposed from component are:

- Interfaces
- DTOs needed for interfaces
- Dependency Injection configuration

In API's `Startup.cs` you just add calls to extension methods in order to configure DI like this:

```csharp
public void ConfigureServices(IServiceCollection services)
{
	services.AddControllers();

	services.AddComponentWeatherForecast();
	services.AddComponentCalculator();
}
```

Here is a sample of one extension method:

```csharp
public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddComponentWeatherForecast(this IServiceCollection services)
	{
		return services.AddScoped<IWeatherForecastService, WeatherForecastService>();
	}
}
```

## Final words

I hope you like these encapsulated components. They are really making hard for team members to make "shortcuts". If components need to talk to each other, that is totally fine - only thing you can access on another component are interfaces and DTOs. If you want to make a full-blown service out of your component, that would be super-easy as everything is in one place.

Would like to hear your comments and questions.

Thanks for reading!
