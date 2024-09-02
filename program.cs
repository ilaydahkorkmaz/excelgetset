

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<ServiceClient>(provider =>
{
    var binding = new BasicHttpBinding();
    binding.TextEncoding = Encoding.UTF8;
    var endpoint = new EndpointAddress("http://hastane_ip:1010/Service.svc");
    return new ServiceClient(binding, endpoint);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
