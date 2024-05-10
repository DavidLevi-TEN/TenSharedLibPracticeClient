using Microsoft.Extensions.DependencyInjection;
using Ten.Grpc.CorporateConfiguration.Api;
using Ten.Grpc.CorporateConfiguration.Dto;
using Ten.Shared.Audit.Grpc;
using Ten.Shared.Hosting.Builder;
using Ten.Shared.Logging;
using TenSharedLibraryPractice.Protos;

AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

var builder = TenAppBuilder.CreateBuilder("TenSharedLibPracticeClient", "TSLPC", args);

builder.AddTenLogging();

builder.AddTenAuditGrpc();

builder.Services.AddGrpcClient<Greeter.GreeterClient>((provider, grpcClientFactoryOptions) =>
{
    grpcClientFactoryOptions.Address = new Uri("http://localhost:8888");
    grpcClientFactoryOptions.AddTenAuditLogging(provider);
});

var app = builder.Build();

var request = new HelloRequest();

_ = await app.Services.GetRequiredService<Greeter.GreeterClient>().SayHelloAsync(request);
