## Making console app

### Check the version

```
dotnet --info
```

### Create solution file

```
dotnet new sln
```

### Create a library project

```
dotnet new classlib -n library
```

### Create console project

```
dotnet new console -n console-app
```

### Add projects to solution

```
dotnet sln add library
dotnet sln add console-app
```

### Add lib project to console project

```
cd console-app
dotnet add reference ../library
```

### Run project

```
dotnet run -p console-app\
```

### Managing nuget using nuget package extension

Created by jmrog

Testing using donet core 3.1.9

Add the following packages

- Microsoft.Extensions.Hosting
- Serilog.Extensions.Hosting (v. 3.1.0)
- Serilog.Settings.Configuration (v. 3.1.0)
- Serilog.Sinks.Console
