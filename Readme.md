# Lski.Data.Profiler

Ever want to 'just' see the queries and parameters that are being sent to the database? This project allow you to do that, by just wrapping new connections when they are created and using them like normal.

## Usage

Just wrap your new connection with a profiler. It create a string containing both the query and any parameters along with their values and then run the action passed in. In the example below any query will be output to the console.

```cs
var connection = new SqlConnection("your-connection-string");
// replace with this
var connection = new ProfiledDbConnection(new SqlConnection("your-connection-string"), new SimpleProfiler(Console.WriteLine));
```

#### Disclaimer 

This library is heavily based on the database profiler from the open source (MIT) [MiniProfiler](https://github.com/MiniProfiler/dotnet) project by Sam Saffron from Stack Overflow. In fact the first version is approximately 90% code extracted from that project.  The purpose of this project differs from MiniProfiler though and I will be adding/changing that code over time to provide additional features.